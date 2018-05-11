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
	[JsonObject]
	public class OneDriveItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public uint Size { get; set; }
		public JObject Folder { get; set; }
		public JObject File { get; set; }
		public JObject Deleted { get; set; }
		public string OwnerId { get; set; }
		[JsonProperty("createdBy")]
		private JObject createdBy
		{
			set
			{
				OwnerId = value["user"]["id"].ToString();
			}
		}
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

		public override string ToString()
		{
			return String.Format("Name = {0}; Size = {1}; Owner = {2}",Name,Size,OwnerId);
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
        
		[JsonIgnore]
		public string ReferencePath
        {
            get
            {
                string fullPath = ParentReference["path"].Value<String>();
                return fullPath.Replace("/drive/root:/","").Replace("/","\\");
                //return fullPath.Substring(fullPath.IndexOf("/") + 1);
            }
        }
		[JsonIgnore]
		public string ParentId
		{
			get { return ParentReference["id"].Value<String>(); }
		}
		[JsonIgnore]
		public string SHA1Hash
		{
			get { return File["hashes"]["sha1Hash"]?.ToString(); }
		}
    }
}
