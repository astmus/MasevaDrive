using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


	public static class HashExtension
	{
		private static MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();
		public static string ToHash(this string path)
		{
			var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(path));
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
				sb.Append(hashBytes[i].ToString("X2"));
			return sb.ToString();
		}
	}

public static class LongExt
{
	public static long AsKB(this long value)
	{
		return (long)Math.Round(value / 1024.0);
	}

	public static long AsMB(this long value)
	{
		return (long)Math.Round(value.AsKB() / 1024.0);
	}

	public static long AsGB(this long value)
	{
		return (long)Math.Round(value.AsMB() / 1024.0);
	}
}
