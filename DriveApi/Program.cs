using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting.Engine;
using System.Configuration;
using DriveApi.Extensions;
using DriveApi.Storage;

namespace DriveApi
{
	class Program
	{
		static void Main(string[] args)
		{
			InitializeAppSettings();			
			// Start OWIN host 						
			using (WebApp.Start<Startup>(url: ConfigurationManager.AppSettings.BaseAddress()))
			{
				// Create HttpClient and make a request to api/values 
				HttpClient client = new HttpClient();

				var response = client.GetAsync(ConfigurationManager.AppSettings.BaseAddress()).Result;

				Console.WriteLine(response);
				Console.WriteLine(response.Content.ReadAsStringAsync().Result);
				Console.ReadLine();
			}
		}

		private static void InitializeAppSettings()
		{
			var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (configFile.AppSettings.Settings["RootPath"] == null)
				configFile.AppSettings.Settings.Add("RootPath", @"z:\Images&Video");
			if (configFile.AppSettings.Settings["BaseAddress"] == null)
				configFile.AppSettings.Settings.Add("BaseAddress", "http://192.168.0.103:9090/");
			if (configFile.AppSettings.Settings["PathToThumbnails"] == null)
				configFile.AppSettings.Settings.Add("PathToThumbnails", @"z:\Temp\Thumbnails");

			configFile.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
		}
	}
}
