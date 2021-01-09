﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DriveDataInfoActualizer
{
	public partial class ActualizerInfoService : ServiceBase
	{
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

		public ActualizerInfoService()
		{
			InitializeComponent();
			Logger = new System.Diagnostics.EventLog();
			if (!System.Diagnostics.EventLog.SourceExists("ActializerSource"))
			{
				System.Diagnostics.EventLog.CreateEventSource(
					"ActializerSource", "ActializerNewLog");
			}
			Logger.Source = "ActializerSource";
			Logger.Log = "ActializerNewLog";
		}

		protected override void OnStart(string[] args)
		{
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
			serviceStatus.dwWaitHint = 10000;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);

			Logger.WriteEntry("ActualizerStarted");

			serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);
		}

		protected override void OnStop()
		{
			//ServiceStatus serviceStatus = new ServiceStatus();
			//serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
			//serviceStatus.dwWaitHint = 100000;
			//SetServiceStatus(this.ServiceHandle, ref serviceStatus);

			Logger.WriteEntry("ActualizerStopped");



			//// Update the service state to Stopped.
			//serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
			//SetServiceStatus(this.ServiceHandle, ref serviceStatus);
		}

		protected override void OnContinue()
		{
			Logger.WriteEntry("In OnContinue.");
		}

		protected override void OnPause()
		{
			Logger.WriteEntry("In Pause.");
		}

		public enum ServiceState
		{
			SERVICE_STOPPED = 0x00000001,
			SERVICE_START_PENDING = 0x00000002,
			SERVICE_STOP_PENDING = 0x00000003,
			SERVICE_RUNNING = 0x00000004,
			SERVICE_CONTINUE_PENDING = 0x00000005,
			SERVICE_PAUSE_PENDING = 0x00000006,
			SERVICE_PAUSED = 0x00000007,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ServiceStatus
		{
			public int dwServiceType;
			public ServiceState dwCurrentState;
			public int dwControlsAccepted;
			public int dwWin32ExitCode;
			public int dwServiceSpecificExitCode;
			public int dwCheckPoint;
			public int dwWaitHint;
		};
	}
}
