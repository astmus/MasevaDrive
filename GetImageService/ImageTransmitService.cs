using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace GetImageService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	class ImageTransmitService : GetImageServiceContract
	{
		Dictionary<string, StorageItem> storageItems = new Dictionary<string, StorageItem>();
		public ImageTransmitService()
		{
			DirectoryInfo d = new DirectoryInfo(ConfigurationManager.AppSettings["RootFolder"]);			
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

		public Stream GetImage(string name)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            return File.Open(storageItems[name].Path, FileMode.Open);
        }

		public Stream Connect()
		{
			string result = new ImageTable(new List<string>() {"1","2","3"}).TransformText();
			byte[] resultBytes = Encoding.UTF8.GetBytes(result);
			WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
			return new MemoryStream(resultBytes);
		}
	}
}
