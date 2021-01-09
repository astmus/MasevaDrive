using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkData
{
	public class PipeAccessPoint
	{
		public static ChannelFactory<IStorageDataInfo> Connect()
		{
			return new ChannelFactory<IStorageDataInfo>(new NetNamedPipeBinding() { MaxReceivedMessageSize = 64*1024 }, new EndpointAddress("net.pipe://localhost/StorageItemsInfoPipe"));
		}
	}
}
