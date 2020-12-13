using GetDriveFileService.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace GetDriveFileService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	class FilesTransmitService : GetFileServiceContract
	{
		public FilesTransmitService()
		{
			
		}		

		public Stream GetItem(string name)
        {
			var requieredItem = StorageItemsProvider.Instance[name];
			if (requieredItem == null)
			{
				WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
				WebOperationContext.Current.OutgoingResponse.StatusDescription = "File does not exists";
				return null;
			}
			
			WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
			WebOperationContext.Current.OutgoingResponse.Headers.Add(string.Format("Content-Disposition: attachment; filename=\"{0}\"", requieredItem.ItemName));
			WebOperationContext.Current.OutgoingResponse.ContentLength = requieredItem.Size;
			
			return requieredItem.FileSysInfo.Open(FileMode.Open, FileAccess.Read);
		}
	}
}
