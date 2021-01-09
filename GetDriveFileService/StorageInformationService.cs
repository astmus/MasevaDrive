using FrameworkData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDriveFileService
{
	public class StorageInformationService : IStorageDataInfo
	{
		public StorageInformationService()
		{
			int i = 0;
		}

		public string GetItem(string fileNameHash)
		{
			var requieredItem = StorageItemsProvider.Instance[fileNameHash];
			return requieredItem?.FullPath ?? "";
		}

		public string GetContent(string fileNameHash)
		{
			var requieredItem = StorageItemsProvider.Instance[fileNameHash];
			if (requieredItem == null)
				return "item not found " + fileNameHash;
			if (requieredItem.IsFile)
				return "item has not children";
			var result = StorageItemsProvider.Instance.GetContentOfFolder(fileNameHash);
			return string.Join(Environment.NewLine, result);			
		}

		public StorageItemInfo GetStorageItemByHash(string folderHash)
		{
			var requieredItem = StorageItemsProvider.Instance[folderHash];
			return requieredItem ?? null;
		}

		public List<StorageItemInfo> GetConentOf(string folderHash)
		{
			return StorageItemsProvider.Instance.storageItems.Values.Where(v=>v.ParentHash == folderHash).ToList();
		}
	}
}
