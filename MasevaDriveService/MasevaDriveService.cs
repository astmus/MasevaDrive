using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Timers;
using System.ServiceModel;

namespace MasevaDriveService
{
	public partial class MasevaDriveService : ServiceBase
	{
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
		Timer startTimer;
		private ServiceHost pipeLineHost;		
		public MasevaDriveService()
		{
			InitializeComponent();
			
			Logger = new EventLog();
			if (!EventLog.SourceExists("MasevaDriveSource"))
				EventLog.CreateEventSource("MasevaDriveSource", "MasevaDriveLog");

			Logger.Source = "MasevaDriveSource";
			Logger.Log = "MasevaDriveLog";
			TelegramClient.Instance.ErrorOcccured = (string error) => { Logger.WriteEntry("ERROR: telegram error => " + error, EventLogEntryType.Error); };
		}

		protected override void OnStart(string[] args)
		{
			ServiceStatus serviceStatus = new ServiceStatus();
			serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
			serviceStatus.dwWaitHint = 10000;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);

			StartTelegramPart();
			InitStorageProvider();
			StartPipeLinePart();
			startTimer = new Timer(30000);
			startTimer.Elapsed += StartTimer_Elapsed;
			startTimer.Start();		
			
			serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
			SetServiceStatus(this.ServiceHandle, ref serviceStatus);
		}

		protected override void OnStop()
		{
			//ServiceStatus serviceStatus = new ServiceStatus();
			//serviceStatus.dwCurrentState = ServiceState.SERVICE_STOP_PENDING;
			//serviceStatus.dwWaitHint = 100000;
			//SetServiceStatus(this.ServiceHandle, ref serviceStatus);
			StopTelegramPart();
			StopPipeLinePart();
			Logger.WriteEntry("Service Stopped");
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

		private void StartTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			startTimer.Stop();			
		}

		private void StartPipeLinePart()
		{
			try
			{
				pipeLineHost = new ServiceHost(typeof(StorageDataDriveService), new Uri[] { new Uri("net.pipe://localhost") });
				pipeLineHost.AddServiceEndpoint(typeof(IStorageDataDriveService), new NetNamedPipeBinding(), "StorageItemsInfoPipe");
				pipeLineHost.Open();
			}
			catch (Exception error)
			{
				Logger.WriteEntry("ERROR: Start pipeline part => " + error.Message, EventLogEntryType.Error);
			}
		}


		private void InitStorageProvider()
		{
			try
			{
#if DEBUG
				Stopwatch initMeter = new Stopwatch();
				initMeter.Start();
				StorageItemsProvider.Instance.Initialize();
				initMeter.Stop();
				Logger.WriteEntry("Storage provider iniitalize duration => " + initMeter.Elapsed.ToString());
#else
				StorageItemsProvider.Instance.Initialize();
#endif
			}
			catch (Exception error)
			{
				Logger.WriteEntry("ERROR: Storage provider part => " + error.Message,EventLogEntryType.Error);
			}			
		}

		private void StartTelegramPart()
		{
			try
			{
				TelegramClient.Instance.RaiseError("Error");
			}
			catch (Exception error)
			{
				Logger.WriteEntry("ERROR: Start telegram part => " + error.Message, EventLogEntryType.Error);
			}			
		}

		private void StopPipeLinePart()
		{
			try
			{
				pipeLineHost?.Close();
			}
			catch (Exception error)
			{
				Logger.WriteEntry("ERROR: Stop pipeline part => " + error.Message, EventLogEntryType.Error);
			}			
		}

		private void StopTelegramPart()
		{
			try
			{
				TelegramClient.Instance.RaiseError("Error");
			}
			catch (Exception error)
			{
				Logger.WriteEntry("ERROR: Stop telegram part => " + error.Message, EventLogEntryType.Error);
			}			
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
