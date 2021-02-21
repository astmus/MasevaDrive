using FrameworkData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	[SvcErrorHandlerBehaviour]
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

		public void SendCustomTestMessage(string customMessage)
		{
			TelegramClient.Instance.SendNotifyAboutError(customMessage);
		}

		public void SendStorageMessage(MasevaMessage message)
		{
			var storageItem = message.InnerItem;
			switch (message.Action)
			{
				case StorageAction.NewFileObtained:
					StorageItemsProvider.Instance.AddNew(storageItem);
					TelegramClient.Instance.SendNotifyFileLoadSuccess(storageItem);
					break;
				case StorageAction.OneDriveError:
					TelegramClient.Instance.SendNotifyAboutError(storageItem.Description);
					break;
				case StorageAction.FileSyncronizationProblem:
					TelegramClient.Instance.SendNotifyAboutSyncError(storageItem.Owner, storageItem.Description);
					break;
				default:
					break;
			}
		}
	}
}
