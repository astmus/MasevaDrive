using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkData
{
	[ServiceContract(Name ="TelegramInterractonService")]
	public interface ITelegrammInterractor
	{
		[OperationContract]
		void SendToOwnerWithActions(StorageItemInfo storageItem);
	}
}
