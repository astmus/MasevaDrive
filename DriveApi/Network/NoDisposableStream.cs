using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DriveApi.Network
{
	public class NoDisposableStream : MemoryStream
	{
		public override void Close()
		{
			int i = 0;
		}
	}
}
