using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkData
{
	public class StorageServicePipeAccessPoint
	{
		public static ChannelFactory<IStorageDataDriveService> GetConnection()
		{
			return new ChannelFactory<IStorageDataDriveService>(new NetNamedPipeBinding() { MaxReceivedMessageSize = 64*1024 }, new EndpointAddress("net.pipe://localhost/StorageItemsInfoPipe"));
		}
	}
}
