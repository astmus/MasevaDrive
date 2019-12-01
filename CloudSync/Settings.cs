using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CloudSync
{
    public class Settings
    {
        private static readonly Lazy<Settings> _instance = new Lazy<Settings>(() => new Settings());
        private Settings()
        {
			Load();	
		}

		public ObservableCollection<OneDriveAccount> Accounts { get; set; }
        public static Settings Instance
        {
            get { return _instance.Value; }
        }

        public void Save()
        {
			Properties.Settings.Default.Accounts = JsonConvert.SerializeObject(Accounts);			
            Properties.Settings.Default.Save();
        }

        public void Load()
        {
			if (!String.IsNullOrEmpty(Properties.Settings.Default.Accounts))
				Accounts = JsonConvert.DeserializeObject<ObservableCollection<OneDriveAccount>>(Properties.Settings.Default.Accounts);
			else
				Accounts = new ObservableCollection<OneDriveAccount>();		
		}
    }
}
