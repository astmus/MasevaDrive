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
		Dictionary<string, StorageItem> storageItems = new Dictionary<string, StorageItem>();
		FileSystemWatcher newFilesWatcher = new FileSystemWatcher(Settings.Default.RootPath);
		private static readonly Lazy<StorageItemsProvider> lazy = new Lazy<StorageItemsProvider>(() => new StorageItemsProvider());

		public static StorageItemsProvider Instance { get { return lazy.Value; } }
		
		private StorageItemsProvider()
		{
			DirectoryInfo d = new DirectoryInfo(Settings.Default.RootPath);
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
			newFilesWatcher = new FileSystemWatcher(Settings.Default.RootPath);
			newFilesWatcher.IncludeSubdirectories = true;
			newFilesWatcher.InternalBufferSize = 65000;
			newFilesWatcher.Created += NewFilesWatcher_Created;
			newFilesWatcher.Deleted += NewFilesWatcher_Deleted;
			newFilesWatcher.EnableRaisingEvents = true;
		}		

		public StorageItem this[string hash]
		{
			get
			{
				if (storageItems.ContainsKey(hash))
					return storageItems[hash];
				else
					return null;
			}
		}

		bool pipeIsServiceEnabled = false;
		public void StartPipeServer()
		{
			pipeIsServiceEnabled = true;
			Task.Factory.StartNew(async () =>
			{
				NamedPipeServerStream server = new NamedPipeServerStream("StorageFileInfoPipe");
				do
				{
					server.WaitForConnection();
					StreamReader reader = new StreamReader(server);
					StreamWriter writer = new StreamWriter(server);
					writer.AutoFlush = true;

					var filePathHash = await reader.ReadLineAsync();
					if (storageItems.ContainsKey(filePathHash))
						writer.WriteLine(storageItems[filePathHash].FullPath);
					else
						writer.WriteLine("error");
					server.Disconnect();
				} while (pipeIsServiceEnabled);
			});
		}

		public void StopPipeService()
		{
			pipeIsServiceEnabled = false;
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

		public void Dispose()
		{
			StopPipeService();
		}
	}
}
