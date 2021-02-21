using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveDispatcher
{
	class StorageServiceHelper
	{
        private static readonly string serviceName = "Maseva Drive Service";
        public static bool IsInstalled()
        {
            var sc = ServiceController.GetServices()
                .FirstOrDefault(service => service.ServiceName == serviceName);

            return (sc != null);
        }

        public static void StopService(Action<int, string> completedCallBack)
        {
            var sc = ServiceController.GetServices()
                .FirstOrDefault(service => service.ServiceName == serviceName);

            if (sc == null)
                return;

            if (sc.Status == ServiceControllerStatus.Running)
            {
                try
                {
                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped);
                    completedCallBack?.Invoke(0, null);
                }
                catch (Exception stopError)
                {
                    completedCallBack?.Invoke(1, stopError.Message);
                }
            }
        }

        public static void StartService(Action<int, string> completedCallBack)
        {
            var sc = ServiceController.GetServices()
                .FirstOrDefault(service => service.ServiceName == serviceName);

            if (sc == null)
                return;

            sc.ServiceName = serviceName;
            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                try
                {
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                    completedCallBack?.Invoke(0, null);
                }
                catch (Exception startError)
                {
                    completedCallBack?.Invoke(1, startError.Message);
                }
            }
        }

        public static ServiceControllerStatus CurrentStatus()
        {
            var sc = ServiceController.GetServices().FirstOrDefault(service => service.ServiceName == serviceName);

            if (sc == null)
                return ServiceControllerStatus.Stopped;
            else
                return sc.Status;
        }
    }
}
