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
			var items = d.EnumerateFileSystemInfos("*", SearchOption.AllDirectories).OrderBy(item=>item.CreationTime).AsEnumerable();
			foreach (var itemi in items)
			{

				if ("*.jpg|*.jpeg|*.bmp|*.gif|*.png|*.mov|*.mp4|*.3gp".IndexOf(itemi.Extension.ToLower()) == -1 && itemi is FileInfo)
					continue;

				StorageItemInfo item = new StorageItemInfo(itemi);
				Items.Add(item.Hash, item);				
			};		
		}

		public void AddNew(StorageItemInfo item)
		{
			if (this[item.Hash] == null)
				this.Items.Add(item.Hash, item);
		}

		public void NewItem(string fullPath, string ownerId = "family")
		{
			StorageItemInfo item = new StorageItemInfo(new FileInfo(fullPath));
			Items.Add(item.Hash, item);
		}

		internal string MoveToTrash(string hash, ref string fileName)
		{
			var file = this[hash];
			if (file == null)
				return "Данный файл не найден. Возможно уже удален.";			
			try
			{
				File.Move(file.FullPath, SolutionSettings.Default.RecycleFolderPath + file.Name);
				fileName = file.Name;
				return null;
			}
			catch (Exception error)
			{
				return "Ошибка удаления: " + error;
			}
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
