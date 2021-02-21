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
	void SendStorageMessage(MasevaMessage message);
	[OperationContract]
	void SendCustomTestMessage(string errorMessage);
}
