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
using System.IO.Pipes;
using System.IO;
using FrameworkData;

namespace CloudSync
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		internal OccuranceNotificationHandler DefaultHandler;
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
			DefaultHandler = new OccuranceNotificationHandler(StorageServicePipeAccessPoint.GetConnection().CreateChannel());
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{			
			//TelegramService.StopService();			
			LogManager.Flush();			
		}		
	}
}
