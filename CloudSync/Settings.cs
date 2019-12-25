using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

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
		public readonly string RootFolder = @"Z:\Images&Video\";
		public static Settings Instance
        {
            get { return _instance.Value; }
        }
		
        public void Load()
        {			
			try
			{
				using (StreamReader file = File.OpenText(@"account_data.dat"))
				{
					JsonSerializer serializer = new JsonSerializer();
					Accounts = (ObservableCollection<OneDriveAccount>)serializer.Deserialize(file, typeof(ObservableCollection<OneDriveAccount>));
				}
			}
			catch (System.Exception ex)
			{
				Save();
			}
		}

		public void Save()
		{
			using (StreamWriter file = File.CreateText(@"account_data.dat"))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, Accounts);
			}
		}
	}
}
