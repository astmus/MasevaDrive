using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;
using LinqToDB.DataProvider;
using LinqToDB.Extensions;
using StorageProviders.NetCore.DBs.SQLite;
using StorageProviders.SQLite;
using Microsoft.Extensions.Logging;
using System.Threading;
using LinqToDB;

namespace TelegramService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder().UseWindowsService(conf =>
			{
				conf.ServiceName = "UniPoint Storage Service";
			}).ConfigureHostConfiguration(b =>
			{
				b.AddEnvironmentVariables().Build();
			}).
			ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<StorageWorker>().AddLinqToDbContext<SQLiteProvider>((provider, options) =>
					{
						options.UseSQLite(hostContext.Configuration.GetConnectionString("DbConnectionString")).UseDefaultLogging(provider);
					});
					services.AddScoped<SQLiteProvider>();
				});
	}

	/*public class SQLiteConnection : SQLiteProvider
	{
		public SQLiteConnection(LinqToDbConnectionOptions<SQLiteConnection> options)
			: base(options)
		{

		}
	}*/
}
