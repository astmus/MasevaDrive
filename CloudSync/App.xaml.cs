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
using System.Windows.Threading;
using static CloudSync.OneDriveClient;
using CloudSync.Models;

namespace CloudSync
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		internal static readonly Lazy<OccuranceNotificationHandler> NotificationSender = new Lazy<OccuranceNotificationHandler>(() => new OccuranceNotificationHandler());
		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			AppSettings.Instance.Save();
			Log.Fatal(e.Exception, "Unhandled exception: {0}", e.Exception);
			LogManager.Flush();			
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Log.Info("App is start");
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
			ServicePointManager.DefaultConnectionLimit = 16;
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{						
			LogManager.Flush();			
		}		
	}
}
