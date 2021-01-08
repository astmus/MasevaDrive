using FrameworkData;
using FrameworkData.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveApi.Storage
{
	public interface IStorageItemsProvide
	{
		IEnumerable<StorageItemInfo> GetRoot();
		StorageItemInfo GetById(string id);
		StorageItemInfo GetFileById(string id);
		IEnumerable<StorageItemInfo> GetChildrenByParentId(string id);
		bool HasId(string id);
	}
	public class StorageItemsProvider : IStorageItemsProvide
	{
		Dictionary<string, StorageItemInfo> items;
		FileSystemWatcher observer = new FileSystemWatcher(SolutionSettings.Default.RootOfMediaFolder);		
		
		public StorageItemsProvider()
		{
			DirectoryInfo d = new DirectoryInfo(SolutionSettings.Default.RootOfMediaFolder);			
			items = new Dictionary<string, StorageItemInfo>();
			var folders = d.GetDirectories("*", SearchOption.AllDirectories);
			foreach (var f in folders)
			{
				StorageItemInfo item = new StorageItemInfo(f);
				items.Add(item.Hash, item);
			};

			var files = d.GetFiles("*", SearchOption.AllDirectories);
			foreach (var f in files)
			{
				StorageItemInfo item = new StorageItemInfo(f);
				items.Add(item.Hash, item);
			};
		}

		public StorageItemInfo GetFileById(string id)
		{
			return items.FirstOrDefault(i => i.Value.IsFile && i.Value.Hash == id).Value;
		}

		public StorageItemInfo GetById(string id)
		{
			return items.ContainsKey(id) ? items[id] : null;
		}

		public IEnumerable<StorageItemInfo> GetChildrenByParentId(string id)
		{
			return items.Where(i => i.Value.ParentHash == id).Select(i => i.Value).AsEnumerable();
		}

		public IEnumerable<StorageItemInfo> GetRoot()
		{
			string pathToRoot = SolutionSettings.Default.RootOfMediaFolder;
			return items.Where(i => i.Value.ParentHash == pathToRoot).Select(i=>i.Value).AsEnumerable();
		}

		public bool HasId(string id)
		{
			return items.ContainsKey(id);
		}
	}
}
