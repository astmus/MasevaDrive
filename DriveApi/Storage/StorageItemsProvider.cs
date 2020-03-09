using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveApi.Extensions;

namespace DriveApi.Storage
{
	public interface IStorageItemsProvide
	{
		IEnumerable<StorageItem> GetRoot();
		StorageItem GetById(string id);
		IEnumerable<StorageItem> GetChildrenByParentId(string id);
		bool HasId(string id);
	}
	public class StorageItemsProvider : IStorageItemsProvide
	{
		Dictionary<string, StorageItem> items;
		FileSystemWatcher observer = new FileSystemWatcher(ConfigurationManager.AppSettings.RootPath());		
		
		public StorageItemsProvider()
		{
			DirectoryInfo d = new DirectoryInfo(ConfigurationManager.AppSettings.RootPath());			
			items = new Dictionary<string, StorageItem>();
			var folders = d.GetDirectories("*", SearchOption.AllDirectories);
			foreach (var f in folders)
			{
				StorageItem item = new StorageItem(f);
				items.Add(item.Id, item);
			};

			var files = d.GetFiles("*", SearchOption.AllDirectories);
			foreach (var f in files)
			{
				StorageItem item = new StorageItem(f);
				items.Add(item.Id, item);
			};
		}

		public StorageItem GetById(string id)
		{
			return items[id];
		}

		public IEnumerable<StorageItem> GetChildrenByParentId(string id)
		{
			return items.Where(i => i.Value.ParentID == id).Select(i => i.Value).AsEnumerable();
		}

		public IEnumerable<StorageItem> GetRoot()
		{
			string pathToRoot = ConfigurationManager.AppSettings.RootPath();
			return items.Where(i => i.Value.ParentPath == pathToRoot).Select(i=>i.Value).AsEnumerable();
		}

		public bool HasId(string id)
		{
			return items.ContainsKey(id);
		}
	}
}
