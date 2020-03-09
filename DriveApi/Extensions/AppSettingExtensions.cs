using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Configuration;

namespace DriveApi.Extensions
{
	public static class AppSettingExtensions
	{
		static AppSettingExtensions()
		{
			
		}

		public static string RootPath(this NameValueCollection settings)
		{
			return settings["RootPath"];
		}

		public static string BaseAddress(this NameValueCollection settings)
		{
			return settings["BaseAddress"];
		}

		public static string PathToThumbnails(this NameValueCollection settings)
		{
			return settings["PathToThumbnails"];
		}
	}

}
