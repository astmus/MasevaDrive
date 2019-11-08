using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync.Interfaces
{
	public interface ICloudStreamProvider
	{
		Task<Stream> GetStreamToFileAsync(string url);
	}
}
