using FrameworkData;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

[ServiceContract(Name = "Maseva Drive")]
public interface IStorageDataDriveService
{
	[OperationContract]
	string GetItem(string fileNameHash);

	[OperationContract]
	string GetContent(string fileNameHash);

	[OperationContract]
	StorageItemInfo GetStorageItemByHash(string fileNameHash);

	[OperationContract]
	List<StorageItemInfo> GetConentOf(string fileNameHash);

	[OperationContract]
	void SetOwnerForItem(string fileNameHash, string ownerId);

	[OperationContract]
	void AddNewByPath(string fullPath, string ownerId);

	[OperationContract]
	void SendNotifyFileLoadSuccess(string email, string fileName, string formattedSize, string pathToLoadedFile);

	[OperationContract]
	void SendNotifyAboutDeleteFileError(string errorMessage);

	[OperationContract]
	void SendNotifyAboutSyncError(string email, string errorMessage);
	
}
