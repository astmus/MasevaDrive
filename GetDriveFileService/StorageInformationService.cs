﻿using FrameworkData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDriveFileService
{
	public class StorageInformationService : IStorageDataDriveService
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

		public void SetOwnerForItem(string fileNameHash, string ownerId)
		{
			throw new NotImplementedException();
		}

		public void AddNewByPath(string fullPath, string ownerId)
		{
			throw new NotImplementedException();
		}

		public void SendNotifyFileLoadSuccess(string email, string fileName, string formattedSize, string pathToLoadedFile)
		{
			throw new NotImplementedException();
		}

		public void SendNotifyAboutDeleteFileError(string errorMessage)
		{
			throw new NotImplementedException();
		}

		public void SendNotifyAboutSyncError(string email, string errorMessage)
		{
			throw new NotImplementedException();
		}
	}
}
