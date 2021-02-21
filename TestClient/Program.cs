using FrameworkData;
using FrameworkData.Settings;
using MasevaDriveService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TestClient.Program;

namespace TestClient
{

	[Description("1111")]
	class Program : ServiceHost
	{		
		static ChannelFactory<IStorageDataDriveService> pipeConnection = null;

		static void Main(string[] args)
		{
			/*ChannelFactory<IMessagebleService> httpFactory =
			  new ChannelFactory<IMessagebleService>(
				new BasicHttpBinding(),
				new EndpointAddress(
				  "http://localhost:8000/Reverse"));*/
			Program serviceHost = new Program();
			serviceHost.init();

		}

		public Program() :base(typeof(StorageDataDriveService), new Uri[] { new Uri("net.pipe://localhost") })
		{			
			AddServiceEndpoint(typeof(IStorageDataDriveService), new NetNamedPipeBinding(), "StorageItemsInfoPipe");
			Open();
			Opened += PipeLineHost_Opened;
			Faulted += PipeLineHost_Faulted;
		}

		private void init()
		{
			StartTelegramPart();			
			InitStorageProvider();
			StartPipeLinePart();
			AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			Console.WriteLine("completed");
			Console.ReadKey();
			StopTelegramPart();
			StopPipeLinePart();
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			StopTelegramPart();
			StopPipeLinePart();
		}

		private void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
		//	StopTelegramPart();
			StopPipeLinePart();
		}

		private void StartPipeLinePart()
		{
			try
			{			
				
			}
			catch (Exception error)
			{
				Console.WriteLine("ERROR: Start pipeline part => " + error.Message, EventLogEntryType.Error);
			}
		}

		private void PipeLineHost_Faulted(Object sender, EventArgs e) => Console.WriteLine("Pipe is fault => " + e.ToString() );
		private void PipeLineHost_Opened(Object sender, EventArgs e) => Console.WriteLine("Pipe is open");

		private void InitStorageProvider()
		{
			try
			{
#if DEBUG
				Stopwatch initMeter = new Stopwatch();
				initMeter.Start();
				StorageItemsProvider.Instance.Initialize();
				initMeter.Stop();
				Console.WriteLine("Storage provider iniitalize duration => " + initMeter.Elapsed.ToString());
#else
				StorageItemsProvider.Instance.Initialize();
#endif
			}
			catch (Exception error)
			{
				Console.WriteLine("ERROR: Storage provider part => " + error.Message, EventLogEntryType.Error);
			}
		}

		private void StartTelegramPart()
		{
			try
			{
				TelegramClient.Instance.RaiseError("Error");
				TelegramClient.Instance.ErrorOcccured = (message) => Console.WriteLine(message);
			}
			catch (Exception error)
			{
				Console.WriteLine("ERROR: Start telegram part => " + error.Message, EventLogEntryType.Error);
			}
		}

		private void StopPipeLinePart()
		{
			try
			{
				Close();
			}
			catch (Exception error)
			{
				Console.WriteLine("ERROR: Stop pipeline part => " + error.Message, EventLogEntryType.Error);
			}
		}

		private void StopTelegramPart()
		{
			try
			{
				TelegramClient.Instance.StopService();
			}
			catch (Exception error)
			{
				Console.WriteLine("ERROR: Stop telegram part => " + error.Message, EventLogEntryType.Error);
			}
		}
	}
}
