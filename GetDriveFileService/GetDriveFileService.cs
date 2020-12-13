using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GetDriveFileService
{
	public partial class GetDriveFileService : ServiceBase
	{
		public ServiceHost serviceHost = null;
		public GetDriveFileService()
		{
			InitializeComponent();
		}		

		protected override void OnStart(string[] args)
		{
			if (serviceHost != null)
			{
				serviceHost.Close();
			}

			try
			{
				StorageItemsProvider.Instance.StartPipeServer();
				Uri baseAddress = new Uri(@"http://192.168.0.103:9090/");
				serviceHost = new ServiceHost(typeof(FilesTransmitService), baseAddress);
				serviceHost.AddServiceEndpoint(typeof(GetFileServiceContract), new WebHttpBinding(), "").Behaviors.Add(new WebHttpBehavior());
			}
			catch (System.Exception ex)
			{
				throw new Exception("My data");
			}
			
			serviceHost.Open();
		}

		protected override void OnStop()
		{
			if (serviceHost != null)
			{
				serviceHost.Close();
				serviceHost = null;
			}
			StorageItemsProvider.Instance.StopPipeService();
		}
	}
}
