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

namespace CloudSync
{    
    public class OneDriveSyncFolder : OneDriveItem
    {
        [JsonProperty]
        private string deltaLink;
        [JsonProperty]
        private string nextLink;
        [JsonProperty]
        Stack<OneDriveSyncItem> itemsForSync = new Stack<OneDriveSyncItem>();
        [JsonIgnore]
        public bool IsActive {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (_isActive)
                {
                    if (itemsForSync.Count > 0)
                        StartCreateWorkers();
                    else
                        Sync(nextLink ?? deltaLink);
                }
                else
                    syncTimer.Stop();
            }
        }
        [JsonProperty("IsActive")]
        private bool _isActive = true;
        public string PathToSync { get; set; }
        public event Action<IProgressable> NewWorkerReady;
        private DispatcherTimer syncTimer = new DispatcherTimer();        
        public OneDriveSyncFolder()
        {
        }
        public OneDriveSyncFolder(OneDriveItem folder, string pathToSync) : base()
        {
            this.Id = folder.Id;
            this.Name = folder.Name;
            this.Size = folder.Size;
            this.PathToSync = pathToSync;
            syncTimer.Tick += CheckUpdatesOnTheServer;
            syncTimer.Interval = TimeSpan.FromMinutes(1);            
        }      

        public async void Sync(string link = null)
        {
            if (!IsActive) return;
            string deltaRequest = link ?? String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/delta?select=id,name,size,folder,file,deleted,parentReference", Id);
            string result = await OneDrive.GetHttpContentWithToken(deltaRequest);
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
            for (int i = 0; i < 4 && itemsForSync.Count != 0; i++)
            {
                var worker = MakeNextWorker();
                NewWorkerReady?.Invoke(worker);
            }
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
            if (itemsForSync.Count != 0)
            {
                var syncItem = itemsForSync.Pop();
                DownloadFileWorker worker = new DownloadFileWorker(syncItem.Link, Path.Combine(PathToSync, syncItem.ReferencePath, syncItem.Name));
                worker.TaskName = String.Format("{0} ({1})", syncItem.Name, syncItem.FormattedSize);
                worker.Completed += OnWorkerCompleted;
                return worker;
            }
            return null;
        }
    }
}
