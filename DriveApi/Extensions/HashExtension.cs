using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DriveApi.Extensions
{
	static class HashExtension
	{
		private static MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();
		public static string ToHash(this string value)
		{
			var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(value));
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
				sb.Append(hashBytes[i].ToString("X2"));
			return sb.ToString();
		}
	}
}
