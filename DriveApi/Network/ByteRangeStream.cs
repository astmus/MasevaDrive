using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using static DriveApi.Storage.StorageItem;
using Medallion.Shell;

namespace DriveApi.Network
{
	class ByteRangeStream : MemoryStream
	{
		private MemoryStream outputStream;
		

		public ByteRangeStream(StorageFile file)
		{
			outputStream = file.encodedStream.Value;
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

		~ByteRangeStream()
		{
			int i = 0;
		}

		public override long Position
		{
			get
			{
				return outputStream.Position;
			}

			set
			{
				outputStream.Position = value;
			}
		}

		public override long Length
		{
			get
			{
				return outputStream.Length;
			}
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return outputStream.Seek(offset, origin);
		}

		public override int Read(byte[] array, int offset, int count)
		{
			return outputStream.Read(array, offset, count);
		}

		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return outputStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		public override int ReadByte()
		{
			return outputStream.ReadByte();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			base.Write(buffer, offset, count);
		}

		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.WriteAsync(buffer, offset, count, cancellationToken);
		}

		public override void WriteByte(byte value)
		{
			base.WriteByte(value);
		}		
	}
}
