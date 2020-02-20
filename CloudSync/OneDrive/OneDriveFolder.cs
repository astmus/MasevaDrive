using System;
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
using CloudSync.Framework;
using CloudSync.OneDrive;
using System.Net.Http;
using CloudSync.Telegram;

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
		public event Action<Worker> NewWorkerReady;
		public event PropertyChangedEventHandler PropertyChanged;
		//public HashSet<OneDriveSyncItem> ChildrenFolders { get; private set; } = new HashSet<OneDriveSyncItem>(new OneDriveSyncItem());

		private string PathToDispathFolder { get; set; }
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
			string deltaRequest = link ?? String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/delta?select=id,name,size,folder,file,deleted,parentReference,createdBy,createdDateTime", Id);
			string result = await Owner.GetHttpContent(deltaRequest);
			if (result == null)
				StartSyncTimer();
			else
			{
				var jresult = JObject.Parse(result);
				deltaLink = jresult["@odata.deltaLink"]?.ToString();
				nextLink = jresult["@odata.nextLink"]?.ToString();
				if (jresult["error"] != null)
				{
					MessageBox.Show(jresult["error"]["code"].ToString() + Environment.NewLine + jresult["error"]["message"].ToString());					
					logger.Error(jresult["error"].ToString());
					TelegramService.SendNotifyAboutSyncError("astmus@live.com", jresult["error"].ToString());
					if (jresult["error"]["code"].ToString() == "InvalidAuthenticationToken")
						ResetDeltaLink();
				}
				
				var allItems = jresult["value"]?.ToObject<List<OneDriveSyncItem>>();

				if ((allItems?.Count ?? 0) == 0)
					StartSyncTimer();
				else
				{

					logger.Trace("Sync function for {0} items; Deleted = {1}; Files = {2}; Folders = {3}; for folder {4}", allItems.Count, allItems.Where(w => w.Deleted != null).Count(), allItems.Where(w => w.File != null).Count(), allItems.Where(w => w.Folder != null).Count(), this.Name);
					var withOutDeletedItems = allItems.Where(w => w.Deleted == null);
					foreach (var file in withOutDeletedItems.Where(w => w.File != null))
						itemsForSync.Enqueue(file);
					//ChildrenFolders.UnionWith(withOutDeletedItems.Where(w => w.Id != this.Id && w.Folder != null));			
					CheckAndCreateDirectoriesForPaths();
					StartCreateWorkers();
				}
			}
		}

		private void CheckAndCreateDirectoriesForPaths()
		{
			PathToDispathFolder = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), Owner.UserData.DisplayName);
			if (Directory.Exists(PathToDispathFolder) == false)
				Directory.CreateDirectory(PathToDispathFolder);
			/*foreach (var folder in ChildrenFolders)
				if (Directory.Exists(Path.Combine(PathToSync, folder.ReferencePath, folder.Name)) == false)
					Directory.CreateDirectory(Path.Combine(PathToSync, folder.ReferencePath, folder.Name));*/
		}

		private void StartCreateWorkers()
		{
			Worker worker = null;
			for (int i = 0; i < 2 && (worker = MakeNextWorker()) != null; i++)
				NewWorkerReady?.Invoke(worker);

			if (itemsForSync.Count == 0)
				StartSyncTimer();
		}

		private void initTimer()
		{
			syncTimer.Tick += CheckUpdatesOnTheServer;
#if DEBUG 
			syncTimer.Interval = TimeSpan.FromSeconds(30);
#else
			syncTimer.Interval = TimeSpan.FromSeconds(300);
#endif
		}

		private void OnActiveChanged(bool newValue)
		{
			logger.Trace("Active of folder {0} changed changed to {1}", this.Name, newValue);
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

		private void OnWorkerCompleted(Worker worker, ProgressableEventArgs args)
		{
			logger.Info("{0} completed with success = {1}", worker.TaskName, args.Successfull);

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
					worker.Status = "Attempt";
					if (worker.NumberOfAttempts < 3)
					{
						worker.DoWorkAsync(5000);
						return;
					}
				}
				if (args.Error is HttpRequestException)
				{					
					logger.Error(args.Error, worker.TaskName);
				}
				if (args.Error is TaskCanceledException || args.Error is OperationCanceledException)
				{
					logger.Error(args.Error, worker.TaskName);
				}
				if (args.Error is DismantileWorkerException || args.Error is FileMismatchSizeException)
				{
					itemsForSync.Enqueue(worker.SyncItem);
					return;
				}
			}

			if ((Suspended || !IsActive) && worker.SyncItem.CurrentState == SyncState.RemovedFromCloud) return;
			var newWorker = MakeNextWorker(worker.SyncItem.CurrentState != SyncState.RemovedFromCloud && args.Successfull ? worker.SyncItem : null);
			if (newWorker != null)
				NewWorkerReady?.Invoke(newWorker);
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

		private void OnDeleteOneDriveItemCompleted(CloudWorker worker, ProgressableEventArgs args)
		{
			if (args.Successfull)
				logger.Info("File {0} download and sync completed successful", (worker as DeleteOneDriveFileWorker).SyncItem.Name);
			else
				logger.Trace(args.Error, "Delete file {0} from cloud failed {1}", (worker as DeleteOneDriveFileWorker).SyncItem.Name, args.Error);
		}

		private void StartSyncTimer()
		{
			if (!syncTimer.IsEnabled)
			{
				logger.Info("Sync timer staeted fo folder {0}", this.Name);
				syncTimer.Start();
			}
		}

		private void CheckUpdatesOnTheServer(object sender, EventArgs e)
		{
			if (Suspended || !IsActive) return;
			Sync(deltaLink);
		}

		private Worker MakeNextWorker(OneDriveSyncItem syncItem = null)
		{
			while (!itemsForSync.IsEmpty || syncItem != null)
			{
				if (Suspended) return null;
				while (syncItem == null && !itemsForSync.IsEmpty)
				{
					var success = itemsForSync.TryDequeue(out syncItem);
					if (success == false)
						Thread.Sleep(20);
				}
				string destinationPath = Path.Combine(PathToDispathFolder, syncItem.Name);
				FileInfo info = new FileInfo(destinationPath);
				if (syncItem.CurrentState == SyncState.New && info.Exists && (info.Length == syncItem.Size))
				{
					logger.Warn("File already exist. Name = {0}", info.FullName);
					syncItem.CurrentState = SyncState.Loaded;					
				}

				Worker nextWorker = null;
				switch (syncItem.CurrentState)
				{
					case SyncState.New:
						nextWorker = new DownloadFileWorker(syncItem, destinationPath, Owner);
						nextWorker.TaskName = String.Format("{0} ({1})", syncItem.Name, syncItem.FormattedSize);
						logger.Trace("New download worker ready for file {0} save to {1}", syncItem.Name, destinationPath);
						break;
					case SyncState.Loaded:
						nextWorker = new CopyFileWorker(syncItem, PathToDispathFolder, PathToSync);
						nextWorker.TaskName = String.Format("Copy {0} ({1})", syncItem.Name, syncItem.FormattedSize);
						logger.Trace("New copy worker ready for file {0} save to {1}", syncItem.Name, PathToSync);
						TelegramService.SendNotifyFileLoadDone(Owner.UserData.PrincipalName, syncItem);
						break;
					case SyncState.MovedToStore:
						nextWorker = new DeleteOneDriveFileWorker(syncItem, Owner);
						nextWorker.TaskName = String.Format("Request for delete {0} ({1})", syncItem.Name, syncItem.FormattedSize);
						logger.Trace("New delete worker ready for file {0}", syncItem.Name);
						break;
					default:
						break;
				}
				nextWorker.Completed += OnWorkerCompleted;
				return nextWorker;
			}
			return null;
		}
	}
}
