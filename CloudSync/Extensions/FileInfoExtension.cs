using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CloudSync.Extensions
{
	static class FileInfoExtension
	{
		public static String GetSHA1Hash(this FileInfo info)
		{
			SHA1 sha = new SHA1CryptoServiceProvider();
			var fileStream = info.Open(FileMode.Open);
			var sha1hash = sha.ComputeHash(fileStream);
			fileStream.Close();

			StringBuilder sBuilder = new StringBuilder();			
			for (int i = 0; i < sha1hash.Length; i++)
				sBuilder.Append(sha1hash[i].ToString("x2"));

			return sBuilder.ToString().ToUpper();
		}
	}
}
