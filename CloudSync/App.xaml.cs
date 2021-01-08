using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Net;
using CloudSync.Telegram;
using System.IO.Pipes;
using System.IO;

namespace CloudSync
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();		

		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			AppSettings.Instance.Save();
			Log.Fatal(e.Exception, "Unhandled exception: {0}", e.Exception);
			LogManager.Flush();			
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Log.Info("App is start");
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			TelegramService.RequestDeleteFile += OnRequestDeleteFileFromStorage;
			TelegramService.StartService();						
		}

		private async void OnRequestDeleteFileFromStorage(string hashOfPath)
		{
			NamedPipeClientStream client = new NamedPipeClientStream("StorageFileInfoPipe");
			client.Connect();
			StreamReader reader = new StreamReader(client);
			StreamWriter writer = new StreamWriter(client);
			writer.AutoFlush = true;			
			await writer.WriteLineAsync(hashOfPath);
			var pathToFile = await reader.ReadLineAsync();
			try
			{
				File.Delete(pathToFile);
			}
			catch (System.Exception ex)
			{
				TelegramService.SendNotifyAboutDeleteFileError(pathToFile + " cannot delete" + ex.ToString());
			}			
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{			
			TelegramService.StopService();			
			LogManager.Flush();			
		}		
	}
}
