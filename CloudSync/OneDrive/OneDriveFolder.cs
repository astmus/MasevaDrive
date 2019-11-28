﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSync.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Threading;
using System.Threading;
using CloudSync.Extensions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System.Diagnostics;
using CloudSync.Framework.Exceptions;
using System.Collections.Concurrent;
using System.Windows;



namespace CloudSync
{
	public class OneDriveFolder : OneDriveItem, INotifyPropertyChanged
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		[JsonProperty]
		private string deltaLink;
		[JsonProperty]
		private string nextLink;
		[JsonProperty]
		ConcurrentQueue<OneDriveSyncItem> itemsForSync { get; set; } = new ConcurrentQueue<OneDriveSyncItem>();		
		[JsonIgnore]
		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				if (_isActive == value) return;
				_isActive = value;
				NotifyPropertyChanged();
			}
		}
		[JsonProperty("IsActive")]
		private bool _isActive = false;

		[JsonIgnore]
		public bool Suspended
		{
			get { return _suspended; }
			set
			{
				if (_suspended == value) return;
				_suspended = value;
				if (PathToSync != null)
					OnActiveChanged(!value);
			}
		}
		private bool _suspended = true;

		[JsonProperty("PathToSync")]
		private string _pathToSync { get; set; }
		[JsonIgnore]
		public string PathToSync
		{
			get { return _pathToSync; }
			set
			{
				if (_pathToSync == value) return;
				_pathToSync = value;
				NotifyPropertyChanged();
			}
		}

		[JsonIgnore]
		public bool HasWorkerReadySubscribers { get { return NewWorkerReady != null; } }
		public event Action<CloudWorker> NewWorkerReady;
		public event PropertyChangedEventHandler PropertyChanged;
		
		public HashSet<OneDriveSyncItem> ChildrenFolders { get; private set; } = new HashSet<OneDriveSyncItem>(new OneDriveSyncItem());
		private DispatcherTimer syncTimer = new DispatcherTimer();
		private OneDriveClient _owner;		
		private OneDriveClient Owner
		{
			get
			{
				return _owner ?? (_owner = Settings.Instance.Accounts.FirstOrDefault((a) => { return a.Client.UserData.Id == OwnerId; })?.Client);
			}
		}
		public OneDriveFolder()
		{
			initTimer();			
		}		

		protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void StartSync()
		{
			if (!Suspended)
				return; //that mean synchronization is already started
			CheckAndCreateDirectoriesForPaths();
			Suspended = false;
		}

		public void ResetDeltaLink()
		{
			deltaLink = null;
		}

		private async void Sync(string link = null)
		{
			if (Suspended || !IsActive) return;
			string deltaRequest = link ?? String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/delta?select=id,name,size,folder,file,deleted,parentReference,createdBy", Id);
			string result = await Owner.GetHttpContent(deltaRequest);			
			var jresult = JObject.Parse(result);
			deltaLink = jresult["@odata.deltaLink"]?.ToString();
			nextLink = jresult["@odata.nextLink"]?.ToString();
			if (jresult["error"] != null)
				MessageBox.Show(jresult["error"]["code"].ToString() + Environment.NewLine + jresult["error"]["message"].ToString());

			var allItems = jresult["value"]?.ToObject<List<OneDriveSyncItem>>();

			if ((allItems?.Count ?? 0) == 0)
			{
				StartSyncTimer();
				return;
			}

			logger.Trace("Sync function for {0} items; Deleted = {1}; Files = {2}; Folders = {3}; for folder {4}", allItems.Count, allItems.Where(w => w.Deleted != null).Count(), allItems.Where(w => w.File != null).Count(), allItems.Where(w => w.Folder != null).Count(),this.Name);
			var withOutDeletedItems = allItems.Where(w => w.Deleted == null);
			foreach (var file in withOutDeletedItems.Where(w => w.File != null))
				itemsForSync.Enqueue(file);			
			ChildrenFolders.UnionWith(withOutDeletedItems.Where(w => w.Id != this.Id && w.Folder != null));			
			CheckAndCreateDirectoriesForPaths();			
			StartCreateWorkers();
		}

		private void CheckAndCreateDirectoriesForPaths()
		{
			foreach (var folder in ChildrenFolders)
				if (Directory.Exists(Path.Combine(PathToSync, folder.ReferencePath, folder.Name)) == false)
					Directory.CreateDirectory(Path.Combine(PathToSync, folder.ReferencePath, folder.Name));
		}

		private void StartCreateWorkers()
		{
			DownloadFileWorker worker = null;
			for  (int i = 0; i < 10 && (worker = MakeNextWorker()) != null; i++)
				NewWorkerReady?.Invoke(worker);

			if (itemsForSync.Count == 0)
				StartSyncTimer();
		}

		private void initTimer()
		{
			syncTimer.Tick += CheckUpdatesOnTheServer;
			syncTimer.Interval = TimeSpan.FromSeconds(60);
		}

		private void OnActiveChanged(bool newValue)
		{
			logger.Trace("Active of folder {0} changed changed to {1}", this.Name,newValue);
			if (newValue)
			{
				if (itemsForSync.Count > 0)
					StartCreateWorkers();
				else
					Sync(nextLink ?? deltaLink);
			}
			else
				syncTimer.Stop();
		}

		private void OnWorkerCompleted(CloudWorker sender, ProgressableEventArgs args)
		{			
			logger.Info("{0} completed with success = {1}", sender.TaskName, args.Successfull);
			
			if (args.Successfull == false)
			{
				if (args.Error is WebException)
				{
					WebException ex = args.Error as WebException;
					var response = ex.Response as HttpWebResponse;
					if (response != null)
					{
						switch ((int)response.StatusCode)
						{
							//case: HttpStatusCode.Unauthorized
						}
					}
					if (sender.NumberOfAttempts < 3)
					{
						sender.DoWorkAsync((int)TimeSpan.FromSeconds(5).TotalMilliseconds);
						return;
					}
				}
				if (args.Error is TaskCanceledException || args.Error is OperationCanceledException)
				{
					logger.Error(args.Error, sender.TaskName);				
				}
				if (args.Error is DismantileWorkerException)
				{
					itemsForSync.Enqueue((sender as DownloadFileWorker).SyncItem);
					return;
				}
			}
				

			if (Suspended || !IsActive) return;
			var worker = MakeNextWorker();
			if (worker != null)
				NewWorkerReady?.Invoke(worker);
			else
			{
				if (!string.IsNullOrEmpty(nextLink))
				{
					string linkForLoad = nextLink;
					nextLink = null;
					Sync(linkForLoad);
				}
				else StartSyncTimer();
			}
		}
		
		private void StartSyncTimer()
		{
			if (!syncTimer.IsEnabled)
			{
				logger.Trace("Sync timer staeted fo folder {0}",this.Name);
				syncTimer.Start();
			}
		}

		private void CheckUpdatesOnTheServer(object sender, EventArgs e)
		{
			if (Suspended || !IsActive) return;
			Sync(deltaLink);
		}

		private DownloadFileWorker MakeNextWorker()
		{
			while (!itemsForSync.IsEmpty)
			{
				OneDriveSyncItem syncItem = null;
				if (Suspended) return null;
				do 
				{
					var success = itemsForSync.TryDequeue(out syncItem);
					if (success == false)
						Thread.Sleep(20);
				} while (syncItem == null && !itemsForSync.IsEmpty);
				string destinationPath = Path.Combine(PathToSync, syncItem.ReferencePath, syncItem.Name);
				FileInfo info = new FileInfo(destinationPath);				
				if (info.Exists && (info.Length == syncItem.Size) && info.GetSHA1Hash() == syncItem.SHA1Hash)
				{
					logger.Trace("File already exist. Name = {0}",info.FullName);
					continue;
				}
				DownloadFileWorker worker = new DownloadFileWorker(syncItem, destinationPath, Owner);
				worker.TaskName = String.Format("{0} ({1})", syncItem.Name, syncItem.FormattedSize);
				worker.Completed += OnWorkerCompleted;
				logger.Debug("New worker ready for file {0} save to {1}", syncItem.Name, worker.Destination);
				return worker;
			}
			return null;
		}
	}
}
