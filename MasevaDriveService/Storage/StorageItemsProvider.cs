using FrameworkData;
using FrameworkData.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MasevaDriveService
{
	public class StorageItemsProvider : IDisposable
	{
		public Dictionary<string, StorageItemInfo> Items = new Dictionary<string, StorageItemInfo>();		
		private static readonly Lazy<StorageItemsProvider> lazy = new Lazy<StorageItemsProvider>(() => new StorageItemsProvider());

		public static StorageItemsProvider Instance { get { return lazy.Value; } }
		
		public void Initialize()
		{

		}

		private StorageItemsProvider()
		{
			DirectoryInfo d = new DirectoryInfo(SolutionSettings.Default.RootOfMediaFolder);
			var folders = d.GetDirectories("*", SearchOption.AllDirectories);
			foreach (var f in folders)
			{
				StorageItemInfo item = new StorageItemInfo(f);
				Items.Add(item.Hash, item);
			};

			var files = d.GetFiles("*.*", SearchOption.AllDirectories);
			foreach (var f in files)
			{
				if ("*.jpg|*.jpeg|*.bmp|*.gif|*.png|*.mov|*.mp4|*.3gp".IndexOf(f.Extension) == -1)
					continue;

				StorageItemInfo item = new StorageItemInfo(f);
				Items.Add(item.Hash, item);
			};			
		}

		public void NewItem(string fullPath, string ownerId = "family")
		{
			StorageItemInfo item = new StorageItemInfo(new FileInfo(fullPath));
			Items.Add(item.Hash, item);
		}

		public List<string> GetContentOfFolder(string hash)
		{
			return Items.Where(si => si.Value.ParentHash == hash).Select(s=>s.Value.FullPath).ToList();
		}

		public StorageItemInfo this[string hash]
		{
			get
			{
				if (Items.ContainsKey(hash))
					return Items[hash];
				else
					return null;
			}
		}

		public void Dispose()
		{
		}
	}
}
