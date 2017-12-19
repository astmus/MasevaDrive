using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GetImageService
{
    public partial class HostWinServiсe : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public HostWinServiсe()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the CalculatorService type and 
            // provide the base address.
            try
            {                
                string baseAddress = "http://localhost:46243";
                serviceHost = new ServiceHost(typeof(ImageTransmitService), new Uri(baseAddress));
                serviceHost.AddServiceEndpoint(typeof(GetImageServiceContract), new WebHttpBinding(), "").Behaviors.Add(new WebHttpBehavior());

                serviceHost.Open();
            }
            catch (System.Exception ex)
            {
                File.WriteAllText(@"D:\log.txt", ex.Message);
            }
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
