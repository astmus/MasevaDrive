using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkData
{
	public class PipeAccessPoints
	{
		public static ChannelFactory<IStorageInfoProvider> StorageInformationChannel()
		{
			return new ChannelFactory<IStorageInfoProvider>(new NetNamedPipeBinding() { MaxReceivedMessageSize = 64*1024 }, new EndpointAddress("net.pipe://localhost/StorageItemsInfoPipe"));
		}

		public static ChannelFactory<ITelegramInteractProvider> TelegramInterractChannel()
		{
			return new ChannelFactory<ITelegramInteractProvider>(new NetNamedPipeBinding() { MaxReceivedMessageSize = 64 * 1024 }, new EndpointAddress("net.pipe://localhost/TelegramInteractPipe"));
		}
	}
}
