using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CloudSync.Models
{
    public class OneDriveItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public uint Size { get; set; }

        [JsonIgnore]
        public string FormattedSize
        {
            get {
                string[] suf = { "B", "KB", "MB", "GB", "TB"}; //Longs run out around EB
                if (Size == 0)
                    return "0 " + suf[0];
                long bytes = Math.Abs(Size);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                return (Math.Sign(Size) * num)+ " "+ suf[place];
            }
        }
    }
    
    public class OneDriveSyncItem : OneDriveItem
    {           
        [JsonIgnore]
        public string Link
        {
            get { return String.Format("https://graph.microsoft.com/v1.0/me/drive/items/{0}/content", Id); }
        }
        [JsonProperty("parentReference")]
        public JObject ParentReference { get; set; }

        public object Folder { get; set; }

        public string ReferencePath
        {
            get
            {
                string fullPath = ParentReference["path"].Value<String>();
                return fullPath.Replace("/drive/root:/","").Replace("/","\\");
                //return fullPath.Substring(fullPath.IndexOf("/") + 1);
            }
        }
    }
}
