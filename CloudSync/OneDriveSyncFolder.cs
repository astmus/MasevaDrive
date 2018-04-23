using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudSync.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace CloudSync
{
    public class OneDriveSyncFolder : OneDriveItem
    {
        [JsonProperty]
        private string deltaLink;
        [JsonProperty]
        private string nextLink;
        public OneDriveSyncFolder()
        {
        }

        public OneDriveSyncFolder(OneDriveItem folder, string pathToSync) : base()
        {
            this.Id = folder.Id;
            this.Name = folder.Name;
            this.Size = folder.Size;
            this.PathToSync = pathToSync;
        }

        public bool IsActive { get; set; } = true;
        public string PathToSync { get; set; }
        public event Action<IProgressable> NewWorkerReady;
        [JsonProperty]
        Stack<OneDriveSyncItem> itemsForSync = new Stack<OneDriveSyncItem>();
        public async void Sync(string link = null)
        {
            if (!IsActive) return;            
            string deltaRequest = link ?? String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/delta?select=id,name,size,folder,file,deleted,parentReference", Id);
            string result = await OneDrive.GetHttpContentWithToken(deltaRequest);
            var jresult = JObject.Parse(result);
            deltaLink = jresult["@odata.deltaLink"]?.ToString();
            nextLink = jresult["@odata.nextLink"]?.ToString();                
            var allItems = jresult["value"].ToObject<List<OneDriveSyncItem>>();
            allItems.Where(w => w.Link != null).ToList().ForEach(fe=> itemsForSync.Push(fe));
            var folderItems = allItems.Where(w => w.Id != this.Id && w.Link == null);
            foreach (var folder in folderItems)
                Directory.CreateDirectory(Path.Combine(PathToSync, Name, folder.Name));

            StartCreateWorkers();
        }

        public void StartCreateWorkers()
        {            
            for(int i = 0; i < 4 && itemsForSync.Count != 0; i++)
            {
                var worker = MakeNextWorker();
                NewWorkerReady?.Invoke(worker);
            }
        }

        private void OnWorkerCompleted(object sender)
        {
            var worker = MakeNextWorker();
            if (worker != null)
                NewWorkerReady?.Invoke(worker);
            else
                if (!string.IsNullOrEmpty(deltaLink))
                    Sync(deltaLink);
        }

        private DownloadFileWorker MakeNextWorker()
        {
            if (itemsForSync.Count != 0)
            {
                var syncItem = itemsForSync.Pop();
                DownloadFileWorker worker = new DownloadFileWorker(syncItem.Link, Path.Combine(PathToSync, syncItem.ReferencePath,syncItem.Name));
                worker.TaskName = String.Format("{0} ({1})", syncItem.Name, syncItem.FormattedSize);
                worker.Completed += OnWorkerCompleted;
                return worker;
            }
            return null;
        }
    }
}
