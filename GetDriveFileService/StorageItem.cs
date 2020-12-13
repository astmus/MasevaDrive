using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDriveFileService
{
	public class StorageItem
	{
		public string ParentPath { get; set; }
		public string ParentHash { get; set; }
		public string FullPath { get; set; }
		public string Hash { get; set; }
		public string ItemName { get; set; }
		public bool IsFolder { get; set; } = false;
		public long Size { get; private set; } = 0;
		public FileInfo FileSysInfo { get; set; } = null;
		public DirectoryInfo DirectorySysInfo { get; set; } = null;

		public StorageItem(FileInfo fileInfo)
		{
			ItemName = fileInfo.Name;
			FullPath = fileInfo.FullName;
			Hash = fileInfo.FullName.ToHash();
			ParentPath = fileInfo.DirectoryName;
			ParentHash = fileInfo.DirectoryName.ToHash();
			FileSysInfo = fileInfo;
			Size = fileInfo.Length;
		}
		public StorageItem(DirectoryInfo directoryInfo)
		{
			IsFolder = true;
			ItemName = directoryInfo.Name;
			FullPath = directoryInfo.FullName;
			Hash = directoryInfo.FullName.ToHash();
			ParentPath = directoryInfo.Parent.FullName;
			ParentHash = directoryInfo.Parent.FullName.ToHash();
			DirectorySysInfo = directoryInfo;
		}

		public override string ToString()
		{
			return string.Format(ItemName);
		}
	}
}
