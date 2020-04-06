using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.IO.MemoryMappedFiles;
using Medallion.Shell;

namespace DriveApi.Network
{
	public class DuplexStream: Stream
	{
		MemoryMappedFile mappedFile { get; set; }
		public MemoryMappedViewStream readStream { get; set; }
		public MemoryMappedViewStream writeStream { get; private set; }
		public Command encodeTask;

		public DuplexStream(string name, long expectedSize) 
		{
			this.mappedFile = MemoryMappedFile.CreateNew(name, expectedSize, MemoryMappedFileAccess.ReadWrite); ;
			readStream = mappedFile.CreateViewStream();
			writeStream = mappedFile.CreateViewStream();
		}		

		~DuplexStream()
		{
			int i = 0;
			readStream.Close();
			writeStream.Close();
			mappedFile.Dispose();
		}

		public override long Position
		{
			get
			{
				return readStream.Position;
			}

			set
			{
				readStream.Position = value;
				Console.WriteLine("Position set to =" + value);
			}
		}

		public override long Length
		{
			get
			{
				Console.WriteLine("Read length = "+ readStream.Length + "Read pos = "+ readStream.Position);
				return readStream.Length;
			}
		}

		public override bool CanRead
		{
			get	{return true;}
		}

		public override bool CanSeek
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override int Read(byte[] array, int offset, int count)
		{
			int res = 0;
			while (writeStream.Position - readStream.Position < count)
				//res = Task.Delay(1000).ContinueWith(t => { return readStream.Read(array, offset, count); }).Result;
				Thread.Sleep(1000);
			return readStream.Read(array, offset, count); ;
		}

		public override void Close()
		{
			int i = 0;			
			encodeTask.Kill();
			readStream.Close();
			writeStream.Close();
			mappedFile.Dispose();
			base.Close();
		}

		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return readStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		public override int ReadByte()
		{
			return readStream.ReadByte();
		}

		public override void Flush()
		{
			Console.WriteLine("Flush");
			writeStream.Flush();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			Console.WriteLine("Seek");
			return readStream.PointerOffset;
		}

		public override void SetLength(long value)
		{
			int i = 0;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			writeStream.Write(buffer, offset, count);
		}

		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return writeStream.WriteAsync(buffer, offset, count, cancellationToken);
		}
	}
}
