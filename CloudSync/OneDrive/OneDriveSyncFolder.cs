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
using CloudSync.OneDrive;
using System.Threading;
using CloudSync.Extensions;

namespace CloudSync
{
	public class OneDriveSyncFolder : OneDriveItem
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		[JsonProperty]
		private string deltaLink;
		[JsonProperty]
		private string nextLink;
		[JsonIgnore]
		Queue<OneDriveSyncItem> itemsForSync = new Queue<OneDriveSyncItem>();
		[JsonIgnore]
		public bool IsActive
		{
			get { return _isActive; }
			set
			{
				_isActive = value;
				OnActiveChanged(value);
			}
		}
		[JsonProperty("IsActive")]
		private bool _isActive = true;
		public string PathToSync { get; set; }
		public event Action<IProgressable> NewWorkerReady;
		private DispatcherTimer syncTimer = new DispatcherTimer();
		private OneDriveClient _owner;
		private bool firstSyncCompleted { get; set; } = false;
		private OneDriveClient Owner
		{
			get
			{
				return _owner ?? (_owner = Settings.Instance.Accounts[OwnerId]);
			}
		}
		public OneDriveSyncFolder()
		{
			initTimer();
		}
		public OneDriveSyncFolder(OneDriveItem folder, string pathToSync) : base()
		{
			this.Id = folder.Id;
			this.Name = folder.Name;
			this.Size = folder.Size;
			this.PathToSync = pathToSync;
			this.OwnerId = folder.OwnerId;
			initTimer();
		}

		public async void Sync(string link = null)
		{
			if (!IsActive) return;
			string deltaRequest = link ?? String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/delta?select=id,name,size,folder,file,deleted,parentReference,createdBy", Id);
			string result = await Owner.GetHttpContent(deltaRequest);
			var jresult = JObject.Parse(result);
			deltaLink = jresult["@odata.deltaLink"]?.ToString();
			nextLink = jresult["@odata.nextLink"]?.ToString();

			var allItems = jresult["value"].ToObject<List<OneDriveSyncItem>>();
			if (allItems.Count == 0)
			{
				StartSyncTimer();
				return;
			}

			logger.Trace("Sync function for {0} items; Deleted = {1}; Files = {2}; Folders = {3}; for folder {4}", allItems.Count, allItems.Where(w => w.Deleted != null).Count(), allItems.Where(w => w.File != null).Count(), allItems.Where(w => w.Folder != null).Count(),this.Name);
			allItems = allItems.Where(w => w.Deleted == null).ToList();
			foreach (var item in allItems.Where(w => w.File != null))
				itemsForSync.Enqueue(item);
			var folderItems = allItems.Where(w => w.Id != this.Id && w.Folder != null);
			foreach (var folder in folderItems)
				Directory.CreateDirectory(Path.Combine(PathToSync, folder.ReferencePath, folder.Name));

			StartCreateWorkers();
		}

		public void StartCreateWorkers()
		{
			bool atLeastOneWorkerStarted = false;
			DownloadFileWorker worker = null;
			for  (int i = 0; i < 3 && (worker = MakeNextWorker()) != null; i++)
			{				
				atLeastOneWorkerStarted = true;
				NewWorkerReady?.Invoke(worker);
			}			

			if (!atLeastOneWorkerStarted)
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

		private void OnWorkerCompleted(IProgressable sender, ProgressableEventArgs args)
		{
			logger.Debug("Worker completed {0} with success = {1}", sender, args.Successfull);
			logger.Info("{0} completed with success = {1}", sender.TaskName, args.Successfull);
			if (sender is DownloadFileWorker && args.Successfull && (sender as DownloadFileWorker).DeleteOldestFileOnSuccess)
			{
				var work = (sender as DownloadFileWorker);
				string folderId = work.SyncItem.ParentId;
				//logger.Trace("Delete old file started name:{0} folderId {1}", work.SyncItem.Name, folderId);
				var task = RemoveOldestFiles(folderId).ContinueWith(action =>
				{
					bool result = action.Result;					
				});
			}
			else
				if (args.Error != null)
					logger.Warn("Worker failed with error {0} for items {1}", args.Error.Message, sender.TaskName);

			if (!IsActive) return;
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
				
		private SemaphoreSlim sema = new SemaphoreSlim(1);
		private Dictionary<string, Queue<OneDriveItem>> pools = new Dictionary<string, Queue<OneDriveItem>>();
		private async Task<OneDriveItem> GetItemsForDeletePool(string folderId)
		{
			await sema.WaitAsync();
			Queue<OneDriveItem> itemsForDeletePool = null;
			try
			{
				if (pools.ContainsKey(folderId) == false)
					pools.Add(folderId, new Queue<OneDriveItem>());
				itemsForDeletePool = pools[folderId];
				if (itemsForDeletePool.Count == 0)
				{					
					var result = await Owner.GetListOfItemsInFolder(folderId);
					pools[folderId] = result;
					return result.Count > 0 ? pools[folderId].Dequeue() : null;
				}
				else				
					return itemsForDeletePool.Dequeue();				
			}
			finally
			{
				sema.Release();
			}			
		}

		private async Task<bool> RemoveOldestFiles(string folderId)
		{
			OneDriveItem itemForDelete = await GetItemsForDeletePool(folderId);
			//logger.Debug("Items for delete = {0}", itemForDelete);
			logger.Debug("Delete item = {0}", itemForDelete);
			return await Owner.DeleteItem(itemForDelete);
		}

		private void StartSyncTimer()
		{
			if (!syncTimer.IsEnabled)
			{
				logger.Trace("Sync timer staeted fo folder {0}",this.Name);
				syncTimer.Start();
				firstSyncCompleted = true;
			}
		}

		private void CheckUpdatesOnTheServer(object sender, EventArgs e)
		{
			if (!IsActive || deltaLink == null) return;
			Sync(deltaLink);
		}

		private DownloadFileWorker MakeNextWorker()
		{
			while (itemsForSync.Count != 0)
			{
				OneDriveSyncItem syncItem = itemsForSync.Dequeue();
				string destFileName = Path.Combine(PathToSync, syncItem.ReferencePath, syncItem.Name);
				FileInfo info = new FileInfo(destFileName);				
				if (info.Exists && info.GetSHA1Hash() == syncItem.SHA1Hash)
				{
					logger.Trace("File already exist. Name = {0}",info.FullName);
					continue;
				}
				DownloadFileWorker worker = new DownloadFileWorker(syncItem, destFileName, Owner);
				worker.DeleteOldestFileOnSuccess = firstSyncCompleted && (!info.Exists);
				worker.TaskName = String.Format("{0} ({1})", syncItem.Name, syncItem.FormattedSize);
				worker.Completed += OnWorkerCompleted;
				logger.Debug("New worker ready for file {0} save to {1}", worker.SyncItem.Name, worker.Destination);
				return worker;
			}
			return null;
		}
	}
}
