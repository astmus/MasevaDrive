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
using System.Diagnostics;
using System.Globalization;
using MediaInfo;
using Medallion.Shell;
using System.Threading.Tasks;

namespace DriveApi.Storage
{
	[DataContract]
	public class StorageItem
	{
		private static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
		public static string RootPath = ConfigurationManager.AppSettings.RootPath();
		private static string RootAliace = "Files";
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
		public StorageFile File { get { return _file ?? (_file = StorageFile.Create(this)); } set { _file = value; } }
		[DataMember]
		public StorageDirectory Directory { get { return _directory ?? (_directory = StorageDirectory.Create(DirectorySysInfo)); } set { _directory = value; } }
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
			private MediaInfoWrapper exifInfo;
			public Lazy<MemoryStream> encodedStream;
			private StorageItem parent;
			[DataMember]
			public long Size { get {return parent.FileSysInfo.Length; } set { } }
			[DataMember]
			public DateTime CreationTime { get { return parent.FileSysInfo.CreationTime; } set { } }
			[DataMember]
			public double Duration { get; set; }


			[DataMember]
			public bool IsPicture
			{
				get { return ImageExtensions.Contains(System.IO.Path.GetExtension(parent.Name).ToLowerInvariant()); }
				set { }
			}

			public string FullPath
			{
				get { return parent.FileSysInfo.FullName; }
			}

			public StorageFile(StorageItem parent)
			{
				this.parent = parent;
				encodedStream = new Lazy<MemoryStream>(InitEncodingStream,false);
			}
			private MemoryStream InitEncodingStream()
			{
				string arg = string.Format("-i \"{0}\" -vcodec libx264 -movflags frag_keyframe+empty_moov+faststart -metadata duration=\"177\" -vb 1024k -f mp4 pipe:1", parent.FileSysInfo.FullName);
				var resultStream = new MemoryStream();
				var cmd = Command.Run(@"ffmpeg.exe", null, options => options.StartInfo((i) =>
				{
					i.Arguments = arg;
					i.UseShellExecute = false;
					i.CreateNoWindow = true;
					i.RedirectStandardError = true;
					i.RedirectStandardOutput = true;
					i.RedirectStandardInput = false;
				}));
				var encodingTask = cmd.StandardOutput.PipeToAsync(resultStream, leaveStreamOpen: true);
				encodingTask.Wait();
				return resultStream;
			}

			public static StorageFile Create(StorageItem parent)
			{
				return parent.FileSysInfo == null ? null : new StorageFile(parent);
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