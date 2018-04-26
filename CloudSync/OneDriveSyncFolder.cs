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
			foreach (var item in allItems.Where(w => w.File != null))
				itemsForSync.Push(item);
			var folderItems = allItems.Where(w => w.Id != this.Id && w.Folder != null);
			foreach (var folder in folderItems)
				Directory.CreateDirectory(Path.Combine(PathToSync, folder.ReferencePath, folder.Name));

			if (itemsForSync.Count > 0)
				StartCreateWorkers();
			else
				StartSyncTimer();
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

			if (atLeastOneWorkerStarted == false)
				StartSyncTimer();
		}

		private void initTimer()
		{
			syncTimer.Tick += CheckUpdatesOnTheServer;
			syncTimer.Interval = TimeSpan.FromMinutes(1);
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

		private void OnWorkerCompleted(object sender)
		{
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

		private void StartSyncTimer()
		{
			if (!syncTimer.IsEnabled)
				syncTimer.Start();
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
				DownloadFileWorker worker = new DownloadFileWorker(syncItem.Link, destFileName, Owner);
				worker.TaskName = String.Format("{0} ({1})", syncItem.Name, syncItem.FormattedSize);
				worker.Completed += OnWorkerCompleted;
				return worker;
			}
			return null;
		}
	}
}
