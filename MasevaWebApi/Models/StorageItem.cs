using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MasevaWebApi.Models
{
	public class StorageItem
	{
		public string ParentPath { get; set; }
		public string ParentHash { get; set; }
		public string Path { get; set; }
		public string Hash { get; set; }
		public bool IsDirecory { get; set; } = false;
		public StorageItem(FileInfo fileInfo)
		{
			Path = fileInfo.FullName;
			Hash = fileInfo.FullName.ToHash();
			ParentPath = fileInfo.DirectoryName;
			ParentHash = fileInfo.DirectoryName.ToHash();
		}
		public StorageItem(DirectoryInfo directoryInfo)
		{
			Path = directoryInfo.FullName;
			Hash = directoryInfo.FullName.ToHash();
			ParentPath = directoryInfo.Parent.FullName;
			ParentHash = directoryInfo.Parent.FullName.ToHash();
			IsDirecory = true;
		}
	}

	static class HashExtension
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
}