using FrameworkData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	public class StorageDataDriveService : IStorageDataDriveService
	{
		public StorageDataDriveService()
		{
			//int i = 0;
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
			return StorageItemsProvider.Instance.Items.Values.Where(v=>v.ParentHash == folderHash).ToList();
		}

		public void SetOwnerForItem(string fileNameHash, string ownerId)
		{
			var requieredItem = StorageItemsProvider.Instance[fileNameHash];
			requieredItem.Owner = ownerId;
		}

		public void AddNewByPath(string fullPath, string ownerId)
		{
			if (File.Exists(fullPath))
				StorageItemsProvider.Instance.NewItem(fullPath);
		}

		public void SendNotifyFileLoadSuccess(string email, string fileName, string formattedSize, string pathToLoadedFile)
		{
			TelegramClient.Instance.SendNotifyFileLoadSuccess(email, fileName, formattedSize, pathToLoadedFile);
		}

		public void SendNotifyAboutDeleteFileError(string errorMessage)
		{
			TelegramClient.Instance.SendNotifyAboutDeleteFileError(errorMessage);
		}

		public void SendNotifyAboutSyncError(string email, string errorMessage)
		{
			TelegramClient.Instance.SendNotifyAboutSyncError(email, errorMessage);
		}
	}
}
