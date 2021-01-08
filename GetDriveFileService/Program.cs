using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GetDriveFileService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static ServiceHost host;
		static void Main()
		{
			/*ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{*/

			/*Uri baseAddress = new Uri(@"http://192.168.0.103:9090/");
			using (var service = new ServiceHost(typeof(FilesTransmitService)))
			{			
				/*var bind = new WebHttpBinding();
				bind.ReaderQuotas.MaxArrayLength = 2147483647;
				bind.ReaderQuotas.MaxStringContentLength = 2147483647;
				bind.TransferMode = TransferMode.Streamed;
				bind.MaxReceivedMessageSize = 2147483647;
				bind.Security.Mode = WebHttpSecurityMode.TransportCredentialOnly;
				service.AddServiceEndpoint(typeof(GetFileServiceContract), bind, "").Behaviors.Add(new WebHttpBehavior());*/
			//service.Open();
			//Console.ReadKey();
			//}
			/*};
			ServiceBase.Run(ServicesToRun);*/
			AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			var binding = new NetNamedPipeBinding
			{
				OpenTimeout = TimeSpan.FromSeconds(50),
				SendTimeout = TimeSpan.FromSeconds(50),
				CloseTimeout = TimeSpan.FromSeconds(50),
				MaxConnections = 5,
				//MaxBufferSize = int.MaxValue,
				//MaxReceivedMessageSize = 1024*128,
				//MaxBufferPoolSize = int.MaxValue,
				//TransactionFlow = false,
				//TransactionProtocol = TransactionProtocol.WSAtomicTransaction11,
				TransferMode = TransferMode.Buffered,
				//HostNameComparisonMode = HostNameComparisonMode.WeakWildcard
			};
			if (host != null)
				host.Close();
			using (host = new ServiceHost(typeof(StorageInformationService),new Uri[]{new Uri("net.pipe://localhost")}))
			{

				host.AddServiceEndpoint(typeof(IStorageDataInfo), new NetNamedPipeBinding(), "StorageItemsInfoPipe");

				host.Open();				

				Console.WriteLine("Service is available. " +
				  "Press <ENTER> to exit.");
				Console.ReadLine();

				host.Close();
			}
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			host?.Close();
		}

		private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			host?.Close();
		}
	}
}
