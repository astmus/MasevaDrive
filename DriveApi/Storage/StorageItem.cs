using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Configuration;
using DriveApi.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DriveApi.Storage
{
	[DataContract]
	public class StorageItem
	{
		private static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
		public static string RootPath = ConfigurationManager.AppSettings.RootPath();
		private static string RootAliace = "Файлы";
		[NotMapped]
		public string ParentPath { get; set; }
		[DataMember]
		public string ParentID { get; set; }
		[DataMember]
		public string ParentName { get; set; }
		[NotMapped]
		public string Path { get; set; }
		[DataMember]
		public string Id { get; set; }
		[DataMember]
		public string Name { get; set; }		
		[DataMember]
		public StorageFile File { get { return _file ?? StorageFile.Create(FileSysInfo); } set { _file = value; } }
		[DataMember]
		public StorageDirectory Directory { get { return _directory ?? StorageDirectory.Create(DirectorySysInfo); } set { _directory = value; } }
		[NotMapped]
		public FileInfo FileSysInfo { get; set; } = null;
		[NotMapped]
		public DirectoryInfo DirectorySysInfo { get; set; } = null;

		private StorageDirectory _directory = null;
		private StorageFile _file = null;

		public StorageItem(FileInfo fileInfo)
		{
			Path = fileInfo.FullName;			
			Id = Path.ToHash();
			ParentPath = fileInfo.DirectoryName;			
			ParentID = ParentPath.ToHash();
			FileSysInfo = fileInfo;			
			Name = System.IO.Path.GetFileName(Path);			
			ParentName = ParentPath == ConfigurationManager.AppSettings.RootPath() ? RootAliace : System.IO.Path.GetFileName(ParentPath);
		}

		public StorageItem(DirectoryInfo directoryInfo)
		{			
			Path = directoryInfo.FullName;
			Id = Path.ToHash();
			ParentPath = directoryInfo.Parent.FullName;
			ParentID = ParentPath.ToHash();
			DirectorySysInfo = directoryInfo;
			Name = System.IO.Path.GetFileName(Path);			
			ParentName = ParentPath == ConfigurationManager.AppSettings.RootPath() ? RootAliace : System.IO.Path.GetFileName(ParentPath);
		}

		[DataContract]
		public class StorageFile
		{
			[DataMember]
			public long Size { get; set; }
			[DataMember]
			public DateTime CreationTime { get; set; }
			[DataMember]
			public double Duration { get; set; }
			public StorageFile(FileInfo info)
			{
				Size = info.Length;
				CreationTime = info.CreationTime;
				if (!ImageExtensions.Contains(System.IO.Path.GetExtension(info.Name).ToLower()))
				{
					Duration = TimeSpan.FromMinutes(13).TotalSeconds;
				}
			}

			public static StorageFile Create(FileInfo info)
			{
				return info == null ? null : new StorageFile(info);
			}
		}

		[DataContract]
		public class StorageDirectory
		{
			[DataMember]
			public long Size { get; set; }
			[DataMember]
			public DateTime CreationTime { get; set; }
			public StorageDirectory(DirectoryInfo info)
			{
				Size = 0;
				CreationTime = info.CreationTime;
			}

			public static StorageDirectory Create(DirectoryInfo info)
			{
				return info == null ? null : new StorageDirectory(info);
			}
		}
	}

	
}