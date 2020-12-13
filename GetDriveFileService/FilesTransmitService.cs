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
		FileSystemWatcher newFilesWatcher;
		public FilesTransmitService()
		{
			DirectoryInfo d = new DirectoryInfo(@"Z:\Images&Video\");			
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
			newFilesWatcher = new FileSystemWatcher(@"Z:\Images&Video\");
			newFilesWatcher.IncludeSubdirectories = true;
			newFilesWatcher.InternalBufferSize = 65000;
			newFilesWatcher.Created += NewFilesWatcher_Created;
			newFilesWatcher.Deleted += NewFilesWatcher_Deleted;
			newFilesWatcher.EnableRaisingEvents = true;
		}

		private void NewFilesWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			storageItems.Remove(e.FullPath.ToHash());
		}

		private void NewFilesWatcher_Created(object sender, FileSystemEventArgs e)
		{
			StorageItem item = new StorageItem(new FileInfo(e.FullPath));
			storageItems.Add(item.Hash, item);
		}

		public Stream GetItem(string name)
        {
			var requieredItem = storageItems[name];

			WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
			WebOperationContext.Current.OutgoingResponse.Headers.Add(string.Format("Content-Disposition: attachment; filename=\"{0}\"", requieredItem.ItemName));
			WebOperationContext.Current.OutgoingResponse.ContentLength = requieredItem.Size;

			return File.Open(requieredItem.Path, FileMode.Open);
		}
	}
}
