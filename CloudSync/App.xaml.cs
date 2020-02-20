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
			Settings.Instance.Save();
			Log.Fatal(e.Exception, "Unhandled exception: {0}", e.Exception);
			LogManager.Flush();			
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Log.Info("App is start");
			var t = Application.ResourceAssembly.GetName().Name;
			var t2 = Application.ResourceAssembly.Location;
			TelegramService.StartService();		
		}

		private void Application_Exit(object sender, ExitEventArgs e)
		{			
			TelegramService.StopService();
			LogManager.Flush();
		}


		/*
		private void SetStartup()
		{
			RegistryKey rk = Registry.CurrentUser.OpenSubKey
				("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			if (chkStartUp.Checked)
				rk.SetValue(AppName, Application.ExecutablePath);
			else
				rk.DeleteValue(AppName, false);
		}*/
	}
}
