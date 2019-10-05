using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloudSync
{
    public class Settings
    {
        private static readonly Lazy<Settings> _instance = new Lazy<Settings>(() => new Settings());
        private Settings()
        {
            FoldersForSync = new List<OneDriveSyncFolder>();
			Accounts = new CloudAccountManager();
			Load();
        }

        public List<OneDriveSyncFolder> FoldersForSync { get; set; }
		public CloudAccountManager Accounts { get; set; }
        public static Settings Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public void Save()
        {
			Properties.Settings.Default.Clouds = JsonConvert.SerializeObject(Accounts);
			Properties.Settings.Default.Folders = JsonConvert.SerializeObject(FoldersForSync);
            Properties.Settings.Default.Save();
        }

        public void Load()
        {			
			if (!String.IsNullOrEmpty(Properties.Settings.Default.Clouds))
				Accounts = JsonConvert.DeserializeObject<CloudAccountManager>(Properties.Settings.Default.Clouds);
			if (!String.IsNullOrEmpty(Properties.Settings.Default.Folders))
                FoldersForSync = JsonConvert.DeserializeObject<List<OneDriveSyncFolder>>(Properties.Settings.Default.Folders);
        }
    }
}
