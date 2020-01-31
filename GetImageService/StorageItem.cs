using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetImageService
{
	public class StorageItem
	{
		public string ParentPath { get; set; }
		public string ParentHash { get; set; }
		public string Path { get; set; }
		public string Hash { get; set; }
		public string ItemName { get; set; }
		public bool IsFolder { get; set; } = false;

		public StorageItem(FileInfo fileInfo)
		{
			ItemName = fileInfo.Name;
			Path = fileInfo.FullName;
			Hash = fileInfo.FullName.ToHash();
			ParentPath = fileInfo.DirectoryName;
			ParentHash = fileInfo.DirectoryName.ToHash();
		}
		public StorageItem(DirectoryInfo directoryInfo)
		{
			IsFolder = true;
			ItemName = directoryInfo.Name;
			Path = directoryInfo.FullName;
			Hash = directoryInfo.FullName.ToHash();
			ParentPath = directoryInfo.Parent.FullName;
			ParentHash = directoryInfo.Parent.FullName.ToHash();
		}

		public override string ToString()
		{
			return string.Format(ItemName);
		}
	}
}
