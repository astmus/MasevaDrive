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

		public Stream GetItem(string name)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
			var requieredItem = storageItems[name];
			if (requieredItem.IsFolder)
			return FolderContent(requieredItem);
				else
			return File.Open(requieredItem.Path, FileMode.Open);
		}

		private Stream FolderContent(StorageItem folder)
		{
			var content = storageItems.Where(item => item.Value.ParentHash == folder.Hash).Select(c => c.Value).ToList();
			string result = new ImageTable(content).TransformText();
			byte[] resultBytes = Encoding.UTF8.GetBytes(result);
			WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
			return new MemoryStream(resultBytes);
		}

		public Stream Root()
		{
			var root = ConfigurationManager.AppSettings["RootFolder"];
			var content = storageItems.Where(item => item.Value.ParentPath == root).Select(c => c.Value).ToList();
			string result = new ImageTable(content).TransformText();
			byte[] resultBytes = Encoding.UTF8.GetBytes(result);
			WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
			return new MemoryStream(resultBytes);
		}
	}
}
