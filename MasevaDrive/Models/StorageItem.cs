﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Configuration;
using MasevaDrive.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MasevaDrive
{
	public class StorageItem
	{
		
		private static string apiFilesURL = "http://192.168.0.103/MasevaDrive/Files/View{0}/{1}?name={2}";
		[NotMapped]
		public string ParentPath { get; set; }
		public string ParentID { get; set; }
		public string ParentName { get; set; }
		[NotMapped]
		public string Path { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public StorageFile File { get { return _file ?? StorageFile.Create(FileSysInfo); } set { _file = value; } }
		public StorageDirectory Directory { get { return _directory ?? StorageDirectory.Create(DirectorySysInfo); } set { _directory = value; } }
		public string ViewLink { get { return string.Format(apiFilesURL, Directory != null ? "" : (File.IsPicture ? "Image" : "Video"), Id, Name); } }
		[NotMapped]
		public FileInfo FileSysInfo { get; set; } = null;
		[NotMapped]
		public DirectoryInfo DirectorySysInfo { get; set; } = null;

		private StorageDirectory _directory = null;
		private StorageFile _file = null;	
		
		public class StorageFile
		{
			public long Size { get; set; }
			public DateTime CreationTime { get; set; }
			public double Duration { get; set; }
			public bool IsPicture { get; set; }
			public StorageFile()
			{
			
			}
			public StorageFile(FileInfo info)
			{
				Size = info.Length;
				CreationTime = info.CreationTime;				
			}

			public static StorageFile Create(FileInfo info)
			{
				return info == null ? null : new StorageFile(info);
			}
		}

		public class StorageDirectory
		{
			public long Size { get; set; }
			public DateTime CreationTime { get; set; }
			public StorageDirectory()
			{
				
			}
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