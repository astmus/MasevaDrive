using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MasevaGiveOutPoint
{
	public class GiveOutPoint
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureKestrel(options =>
					{
						options.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(3);
						options.Limits.MaxConcurrentUpgradedConnections = 130;					
						options.Limits.MaxRequestLineSize = 16 * 1024;
						options.Limits.Http2.MaxFrameSize = 16*1024;
						options.Limits.Http2.MaxRequestHeaderFieldSize = 2*8192;
					});
					webBuilder.UseStartup<Startup>();
				});
	}
}

