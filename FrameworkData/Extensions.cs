using FrameworkData;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


public static class HashExtension
	{
		private static MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();
		public static string ToHash(this string path)
		{
#if DEBUG
		return path;
#else
		var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(path));
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
				sb.Append(hashBytes[i].ToString("X2"));
			return sb.ToString();
#endif
		}

}

public static class StorageItemInfoExtensions
{
	public static string GetFormatSize(this StorageItemInfo storageItem)
	{
		return storageItem.IsFile ? (storageItem.Size.AsMB().ToString("0.##")) + " MB" : "0";
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

	/// <summary>
	/// Present count of KB as bytes
	/// </summary>
	/// <param name="bytes"></param>
	/// <returns></returns>
	public static int KB(this int bytes)
	{
		return (int)Math.Round(bytes * 1024.0);
	}

	/// <summary>
	/// Present count of MB as bytes
	/// </summary>
	/// <param name="bytes"></param>
	/// <returns></returns>
	public static int MB(this int bytes)
	{
		return (int)Math.Round(bytes.KB() * 1024.0);
	}

}
