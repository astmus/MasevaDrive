using FrameworkData;
using GetDriveFileService.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GetDriveFileService
{
	public class StorageItemsProvider : IDisposable
	{
		public Dictionary<string, StorageItemInfo> storageItems = new Dictionary<string, StorageItemInfo>();
		FileSystemWatcher newFilesWatcher = new FileSystemWatcher(Settings.Default.RootPath);
		private static readonly Lazy<StorageItemsProvider> lazy = new Lazy<StorageItemsProvider>(() => new StorageItemsProvider());

		public static StorageItemsProvider Instance { get { return lazy.Value; } }
		
		private StorageItemsProvider()
		{
			DirectoryInfo d = new DirectoryInfo(Settings.Default.RootPath);
			var folders = d.GetDirectories("*", SearchOption.AllDirectories);
			foreach (var f in folders)
			{
				StorageItemInfo item = new StorageItemInfo(f);
				storageItems.Add(item.Hash, item);
			};

			var files = d.GetFiles("*", SearchOption.AllDirectories);
			foreach (var f in files)
			{
				StorageItemInfo item = new StorageItemInfo(f);
				storageItems.Add(item.Hash, item);
			};
			/*newFilesWatcher = new FileSystemWatcher(Settings.Default.RootPath);
			newFilesWatcher.IncludeSubdirectories = true;
			newFilesWatcher.InternalBufferSize = 65000;
			newFilesWatcher.Created += NewFilesWatcher_Created;
			newFilesWatcher.Deleted += NewFilesWatcher_Deleted;
			newFilesWatcher.EnableRaisingEvents = true;*/
		}

		public List<string> GetContentOfFolder(string hash)
		{
			return storageItems.Where(si => si.Value.ParentHash == hash).Select(s=>s.Value.FullPath).ToList();
		}

		public StorageItemInfo this[string hash]
		{
			get
			{
				if (storageItems.ContainsKey(hash))
					return storageItems[hash];
				else
					return null;
			}
		}

		private void NewFilesWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			storageItems.Remove(e.FullPath.ToHash());
		}

		private void NewFilesWatcher_Created(object sender, FileSystemEventArgs e)
		{
			StorageItemInfo item = new StorageItemInfo(new FileInfo(e.FullPath));
			storageItems.Add(item.Hash, item);
		}

		public void Dispose()
		{
		}
	}
}
