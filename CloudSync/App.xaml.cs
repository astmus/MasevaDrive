﻿using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

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
			Log.Fatal(e.Exception, "Unhandled exception: {0}", e.Exception);
			LogManager.Flush();
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			System.Diagnostics.Trace.WriteLine("App is start");
		}
	}
}
