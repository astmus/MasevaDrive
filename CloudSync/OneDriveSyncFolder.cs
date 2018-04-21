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
        private string nextDeltaLink;
        public OneDriveSyncFolder(OneDriveItem folder, string pathToSync) : base()
        {
            this.Id = folder.Id;
            this.Name = folder.Name;
            this.Size = folder.Size;
            this.PathToSync = pathToSync;
        }
        
        public string PathToSync { get; set; }
        public event Action<IProgressable> NewWorkerReady;
        List<OneDriveSyncItem> itemsForSync = new List<OneDriveSyncItem>();
        public async void Sync()
        {
            string deltaRequest = String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/delta", Id);
            string result = await OneDrive.GetHttpContentWithToken(deltaRequest);
            var jresult = JObject.Parse(result);
            nextDeltaLink = (jresult["@odata.deltaLink"] ?? jresult["@odata.nextLink"])?.ToString();
            var allItems = jresult["value"].ToObject<List<OneDriveSyncItem>>();
            itemsForSync.AddRange(allItems.Where(w => w.Link != null));
            var folderItems = allItems.Where(w => w.Id != this.Id && w.Link == null);
            foreach (var folder in folderItems)
                Directory.CreateDirectory(Path.Combine(PathToSync, Name, folder.Name));
        }
    }
}
