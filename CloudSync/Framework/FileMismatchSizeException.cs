using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync.Framework
{
	public class FileMismatchSizeException : Exception
	{
		public long ItemSyncSize { get; private set; }
		public long LoadedFileSize { get; private set; }
		public string PathToLoadedFile { get; private set; }
		public FileMismatchSizeException(string fileName, long itemSyncSize, long loadedFileSize, string pathToLoadedFile) : base(string.Format("{0} file mismatch size, path to loaded {1}",fileName, pathToLoadedFile))
		{
			ItemSyncSize = itemSyncSize;
			LoadedFileSize = loadedFileSize;
		}
	}
}
