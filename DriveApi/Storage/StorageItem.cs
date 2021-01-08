using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using Medallion.Shell;
using System.Threading.Tasks;
using DriveApi.Network;
using System.IO.MemoryMappedFiles;
using System.Net.Http.Headers;

namespace DriveApi.Storage
{
	/*[DataContract]
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
		[NotMapped]
		public List<string> EncodeLogs { get; set; } = new List<string>();

		private StorageDirectory _directory = null;
		private StorageFile _file = null;
		public Command CurrentEncodeProcess;

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
			private Lazy<MediaFileInfo> mediaInfo;
			public Lazy<Stream> encodedStream;
			public bool EncodeCompleted { get; set; }
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

			public MediaFileInfo MediaInfo
			{
				get { return mediaInfo.Value; }
			}

			public StorageFile(StorageItem parent)
			{
				this.parent = parent;
				encodedStream = new Lazy<Stream>(InitEncodingStream,true);
				mediaInfo = new Lazy<MediaFileInfo>(parent.FileSysInfo.GetMediaInfo, true);
			}
			
			List<string> lines = new List<string>();
			TimeSpan offset = TimeSpan.Zero;
			//ByteRangeStream partialStream = new ByteRangeStream();
			public Stream InitEncodingStream()
			{
				/*string arg1 = string.Format("-i \"{0}\" -c:v libx264 -strict -2 -passlogfile log.log -b:v 1M -maxrate 1M -bufsize 2M -pass 1 -f mp4", parent.FileSysInfo.FullName);
				var cmd2 = Command.Run(@"ffmpeg.exe", null, options => options.StartInfo((i) =>
				{
					i.Arguments = arg1;
					i.UseShellExecute = false;
					i.CreateNoWindow = true;
					i.RedirectStandardError = true;
					i.RedirectStandardOutput = false;
					i.RedirectStandardInput = false;
				}));
				cmd2.StandardError.PipeToAsync(Console.Out);*/
				/*

				var info = parent.FileSysInfo.GetMediaInfo();
				double targetVideoBitRate = info.Streams[0].BitRate * .5D;
				double targetAudioBitRate = info.Streams[1].BitRate * .5D;				
				targetAudioBitRate = Math.Max(32000, Math.Min(targetAudioBitRate, 512000));
				double totalDurationSeconds = info.Format.Duration;
				long size = info.Format.Size;
				var leftBits = (info.Format.BitRate * totalDurationSeconds) - ((info.Streams[0].BitRate + info.Streams[1].BitRate) * totalDurationSeconds);
				//string arg = string.Format("-i \"{0}\" -c:v libx264 -strict -2 -passlogfile log.log -bufsize 2M -pass 2 -b 1M -metadata duration=\"{2}\" -c:a copy -f mp4 -movflags frag_keyframe+empty_moov pipe:1", parent.FileSysInfo.FullName, targetBitrate, TimeSpan.FromMilliseconds(totalDuration).ToString());
				//string arg = string.Format("-re -i \"{0}\" -vcodec libvpx -quality realtime -b 1024k -bufsize 1M -metadata duration=\"{2}\" -c:a libopus -f webm pipe:1", parent.FileSysInfo.FullName, targetBitrate, TimeSpan.FromMilliseconds(totalDuration).ToString());
				//-c:v libvpx -minrate 8M -maxrate 8M -b:v 8M -bufsize 1k -c:a libopus -b:a 96k -f webm pipe:1
				//var mmf = MemoryMappedFile.CreateNew(parent.Name, totalDuration * (targetBitrate/8)+ (totalDuration * ((int)mediaInfo.Value.AudioStreams[0].Bitrate / 8)));
				//string arg = string.Format("-v quiet -stats -ss {3} -i \"{0}\" -vcodec libvpx -quality realtime -b 1024k -bufsize 1k -metadata duration=\"{2}\" -c:a libopus -f webm -fs 640k pipe:1", parent.FileSysInfo.FullName, targetBitrate, TimeSpan.FromMilliseconds(totalDuration).ToString(), offset);
				double expectedSize = ((targetVideoBitRate + targetAudioBitRate) * totalDurationSeconds) / 8D;

				string arg = string.Format("-hide_banner -i \"{0}\" -vcodec libvpx -quality realtime -b:v {3} -maxrate {5} -minrate {5} -bufsize 1k -metadata duration=\"{1}\" -ac 2 -c:a libopus -b:a {4} -fs {2} -f webm pipe:1", parent.FileSysInfo.FullName,(int)totalDurationSeconds, (int)expectedSize, (int)targetVideoBitRate, (int)targetAudioBitRate, info.Format.BitRate * 0.5D);
				Console.WriteLine(arg);

				*/
				//var mmf = 
				//var result = new DuplexStream(parent.Name, (long)expectedSize);
				//ByteRangeStream partialStream = new ByteRangeStream(expectedSize);
				//MemoryStream result = new MemoryStream();
				//partialStream.EncodeCompleted = false;
				//var cmd = Command.Run(@"ffmpeg.exe", null, options => options.StartInfo((i) =>
				//{
					//i.Arguments = arg;
					/*i.UseShellExecute = false;
					i.CreateNoWindow = true;
					i.RedirectStandardError = true;
					i.RedirectStandardOutput = true;
					i.RedirectStandardInput = false;					*/
				//}));
				//result.encodeTask = cmd;				
				/*var encodingTask = cmd.StandardOutput.PipeToAsync(result.writeStream, leaveStreamOpen: true);
				cmd.RedirectStandardErrorTo(Console.Out);				
				encodingTask.Wait(1500);*/
				//cmd.RedirectStandardErrorTo(lines);
				//var err = cmd.GetOutputAndErrorLines();
				
				//var encodingTask = cmd.StandardOutput.PipeToAsync(resultStream, leaveStreamOpen: true);
				//encodingTask.ContinueWith((t) => {
				//	resultStream.Encoded = true;
				//});
				//return mmf.CreateViewStream();
				/*lines = lines.Where(l=>l.IndexOf("speed") > 0).ToList();
				List<Dictionary<string, string>> totalStats = new List<Dictionary<string, string>>();
				lines.ForEach((data) =>
				{
					while (data.IndexOf("= ") >= 0)
						data = data.Replace("= ", "=");
					encodeStat = data.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToDictionary(key => key.Split('=')[0], val => val.Split('=')[1]);
					totalStats.Add(encodeStat);
				});*/

				//offset = offset.Add(TimeSpan.Parse(encodeStat?["time"] ?? "00:00:00.0"));
				/*
				return null;
			}

			public static StorageFile Create(StorageItem parent)
			{
				return parent.FileSysInfo == null ? null : new StorageFile(parent);
			}
		}*/

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