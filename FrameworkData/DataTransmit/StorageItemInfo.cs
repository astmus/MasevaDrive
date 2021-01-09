using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkData
{
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
		public DateTime DateTimeLastAccess { get; set; }
		[DataMember]
		public string Owner { get; set; }

		[IgnoreDataMember]
		public string ParentHash { get; set; }

		public override string ToString()
		{
			return string.Format("{0} IsFile = {1} Hash = {2} CR = {3}, LA = {4}, OR = {5}", Name, IsFile, Hash, DateTimeCreation, DateTimeLastAccess, DateTimeOriginal);
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

		public StorageItemInfo(FileSystemInfo storageItemInfo)
		{
			FileInfo file = storageItemInfo as FileInfo;
			IsFile = file != null;
			if (IsFile)
			{
				Size = file.Length;
				ParentHash = file.DirectoryName.ToHash();
				DateTimeCreation = file.CreationTime;
				DateTimeLastAccess = file.LastAccessTime;				
			}
			else
			{
				DirectoryInfo dir = storageItemInfo as DirectoryInfo;
				ParentHash = dir.Parent.Name.ToHash();
			}
			Name = storageItemInfo.Name;
			FullPath = storageItemInfo.FullName;
			Hash = storageItemInfo.FullName.ToHash();
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

