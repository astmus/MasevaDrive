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
using CloudSync.OneDrive;
using System.Threading;

namespace CloudSync
{
	public class OneDriveSyncFolder : OneDriveItem
	{
		[JsonProperty]
		private string deltaLink;
		[JsonProperty]
		private string nextLink;
		[JsonIgnore]
		Stack<OneDriveSyncItem> itemsForSync = new Stack<OneDriveSyncItem>();
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
		private bool shouldDeleteOldestFile = false;
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
			allItems = allItems.Where(w => w.Deleted == null).ToList();
			foreach (var item in allItems.Where(w => w.File != null))
				itemsForSync.Push(item);
			var folderItems = allItems.Where(w => w.Id != this.Id && w.Folder != null);
			foreach (var folder in folderItems)
				Directory.CreateDirectory(Path.Combine(PathToSync, folder.ReferencePath, folder.Name));

			StartCreateWorkers();
		}

		public void StartCreateWorkers()
		{
			bool atLeastOneWorkerStarted = false;
			int i = 0;
			for (var worker = MakeNextWorker(); worker != null && i < 4; worker = MakeNextWorker(), i++)
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
			syncTimer.Interval = TimeSpan.FromSeconds(15);
		}

		private void OnActiveChanged(bool newValue)
		{
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
			if (sender is DownloadFileWorker && shouldDeleteOldestFile && args.Successfull)
			{
				string folderId = (sender as DownloadFileWorker).SyncItem.ParentId;
				var task = RemoveOldestFiles(folderId).ContinueWith(action =>
				{
					bool result = action.Result;
				});
			}
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

		private System.Object lockThis = new System.Object();
		private SemaphoreSlim sema = new SemaphoreSlim(1);
		private Queue<OneDriveItem> _itemsForDeletePool = new Queue<OneDriveItem>();
		private async Task<Queue<OneDriveItem>> GetItemsForDeletePool(string folderId)
		{
			string getItemsUrl = String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/children?orderby=lastModifiedDateTime", folderId);

			await sema.WaitAsync();
			try
			{
				if (_itemsForDeletePool.Count == 0)
				{
					string result = await Owner.GetHttpContent(getItemsUrl);
					var jres = JObject.Parse(result);
					_itemsForDeletePool = jres["value"].ToObject<Queue<OneDriveItem>>();
					return _itemsForDeletePool;
				}
			}
			finally
			{
				sema.Release();
			}
			return _itemsForDeletePool;
		}

		private async Task<bool> RemoveOldestFiles(string folderId)
		{
			//string getItemsUrl = String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/children?orderby=lastModifiedDateTime",folderId);
			//string result = await GetItemsForDeletePool(folderId);
			//var jres = JObject.Parse(result);
			//var allItems = jres["value"].ToObject<List<OneDriveItem>>();
			//allItems.				
			var allItems = await GetItemsForDeletePool(folderId);
			OneDriveItem itemForDelete = allItems.Dequeue();
			return await Owner.DeleteItem(itemForDelete.Id);
		}

		private void StartSyncTimer()
		{
			if (!syncTimer.IsEnabled)
			{
				syncTimer.Start();
				shouldDeleteOldestFile = true;
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
				OneDriveSyncItem syncItem = itemsForSync.Pop();
				string destFileName = Path.Combine(PathToSync, syncItem.ReferencePath, syncItem.Name);
				FileInfo info = new FileInfo(destFileName);
				if (info.Exists)
					continue;
				DownloadFileWorker worker = new DownloadFileWorker(syncItem, destFileName, Owner);
				worker.TaskName = String.Format("{0} ({1})", syncItem.Name, syncItem.FormattedSize);
				worker.Completed += OnWorkerCompleted;
				return worker;
			}
			return null;
		}
	}
}
