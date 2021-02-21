using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FrameworkData
{
	public enum StorageAction : ushort
	{
		NewFileObtained,
		OneDriveError,
		FileSyncronizationProblem
	}

	[DataContract]
	public class MasevaMessage
	{
		[DataMember]
		public string PayloadData { get; set; }
		[DataMember]
		public StorageAction Action { get; set; }
		public static MasevaMessage NewFileObtained(string owner, string pathToFile)
		{
			var data = new StorageItemInfo(pathToFile) { Owner = owner };
			return new MasevaMessage() { Action = StorageAction.NewFileObtained, PayloadData = data.ToJson() }; 
		}
		public static MasevaMessage OneDriveError(string owner, string errorMessage)
		{
			var data = new StorageItemInfo() { Owner = owner, Description = errorMessage, IsFile = false };
			return new MasevaMessage() { Action = StorageAction.OneDriveError, PayloadData = data.ToJson() };
		}
		public static MasevaMessage FileSyncronizationProblem(string owner, string errorMessage)
		{
			var data = new StorageItemInfo() { Owner = owner, Description = errorMessage, IsFile = false };
			return new MasevaMessage() { Action = StorageAction.FileSyncronizationProblem, PayloadData = data.ToJson() };
		}

		public StorageItemInfo InnerItem => JsonConvert.DeserializeObject<StorageItemInfo>(PayloadData) ?? null;
	}

	[DataContract]
	public class StorageItemInfo
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string Hash { get; set; }
		[DataMember]
		public string FullPath { get; set; }
		[DataMember]
		public bool IsFile { get; set; }
		[DataMember]
		public long Size { get; set; }
		[DataMember]
		public DateTime DateTimeOriginal { get; set; }
		[DataMember]
		public DateTime DateTimeCreation { get; set; }
		[DataMember]
		public string Owner { get; set; }
		[DataMember]
		public string Description { get; set; }
		[IgnoreDataMember]
		public string ParentHash { get; set; }

		public override string ToString()
		{
			return string.Format("{0} IsFile = {1} Hash = {2} CR = {3}, OR = {4}", Name, IsFile, Hash, DateTimeCreation, DateTimeOriginal);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[IgnoreDataMember]
		protected FileSystemInfo systemInfo { get; set; }

		public StorageItemInfo()
		{

		}

		public StorageItemInfo(string fullPath):this(new FileInfo(fullPath))
		{
		}

		public string ToJson() => JsonConvert.SerializeObject(this);

		public StorageItemInfo(FileSystemInfo storageItemInfo)
		{
			if (storageItemInfo is FileInfo file)
			{
				IsFile = true;
				Size = file.Length;
				ParentHash = file.DirectoryName.ToHash();
				DateTimeCreation = file.CreationTime;
				DateTimeOriginal = file.LastAccessTime;				
			}
			else
			{
				IsFile = false;
				DirectoryInfo dir = storageItemInfo as DirectoryInfo;
				ParentHash = dir.Parent.Name.ToHash();
			}
			Name = storageItemInfo.Name;
			FullPath = storageItemInfo.FullName;
#if DEBUG
			Hash = Path.GetFileNameWithoutExtension(storageItemInfo.FullName);
#else
			Hash = storageItemInfo.FullName.ToHash();
#endif

			systemInfo = storageItemInfo;
		}

		public FileStream GetStream()
		{
			if (IsFile && File.Exists(FullPath))
				return File.OpenRead(FullPath);
			else
				return null;
		}
	}
}

