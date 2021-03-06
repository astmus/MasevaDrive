﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace CloudSync
{
    internal class AppSettings
    {
        private static readonly Lazy<AppSettings> _instance = new Lazy<AppSettings>(() => new AppSettings());
        private AppSettings()
		{
			Load();
		}
		
		public ObservableCollection<OneDriveAccount> Accounts { get; set; }

		public static AppSettings Instance
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
					Accounts = (ObservableCollection<OneDriveAccount>)serializer.Deserialize(file, typeof(ObservableCollection<OneDriveAccount>)) ?? new ObservableCollection<OneDriveAccount>();
				}
			}
			catch (System.Exception ex)
			{
				//Save();				
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
