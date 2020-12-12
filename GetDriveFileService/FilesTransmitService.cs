using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace GetDriveFileService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	class FilesTransmitService : GetFileServiceContract
	{
		Dictionary<string, StorageItem> storageItems = new Dictionary<string, StorageItem>();
		public FilesTransmitService()
		{
			DirectoryInfo d = new DirectoryInfo(@"z:\Images&Video\");
			var folders = d.GetDirectories("*", SearchOption.AllDirectories);
			foreach (var f in folders)
			{
				StorageItem item = new StorageItem(f);
				storageItems.Add(item.Hash, item);
			};

			var files = d.GetFiles("*", SearchOption.AllDirectories);
			foreach (var f in files)
			{
				StorageItem item = new StorageItem(f);
				storageItems.Add(item.Hash, item);
			};	
		}

		public Stream GetItem(string name)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
			var requieredItem = storageItems[name];			
			return File.Open(requieredItem.Path, FileMode.Open);
		}
	}
}
