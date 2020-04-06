using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using static DriveApi.Storage.StorageItem;
using Medallion.Shell;
using System.Net.Http.Headers;

namespace DriveApi.Network
{
	class ByteRangeStream : MemoryStream
	{
		public bool EncodeCompleted = false;
		public long supposeLength;
		public ByteRangeStream(long supposeLength) : base((int)supposeLength)
		{
			this.supposeLength = supposeLength;
			//outputStream = file.encodedStream.Value;
			//this.file = file;
			//string arg = string.Format("-i \"{0}\" -c:v libvpx -minrate 1M -maxrate 1M -b:v 1M -bufsize 1k -c:a libopus -b:a 96k -metadata duration=\"177\" -f webm pipe:1", file.FullPath);
			/*string arg = string.Format("-i \"{0}\" -vcodec libx264 -movflags frag_keyframe+empty_moov+faststart -vb 1024k -f mp4 pipe:1", file.FullPath);

			var cmd = Command.Run(@"ffmpeg.exe", null, options => options.StartInfo((i) =>
				{
					i.Arguments = arg;
					i.UseShellExecute = false;
					i.CreateNoWindow = true;
					i.RedirectStandardError = true;
					i.RedirectStandardOutput = true;
					i.RedirectStandardInput = false;
				}));
			var encodingTask = cmd.StandardOutput.PipeToAsync(outputStream, leaveStreamOpen: true);
			encodingTask.Wait();
			var results = cmd.StandardError.ReadToEnd();*/
		}

		public override void Close()
		{
			base.Close();
		}

		public override long Length
		{
			get
			{
				return /*EncodeCompleted ? base.Length :*/ supposeLength;
			}
		}
	}
}
