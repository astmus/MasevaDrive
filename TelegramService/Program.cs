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
using StorageProviders.SQLite;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Configuration;
using LinqToDB;
using TelegramService.Jarvise.Interfaces;
using TelegramService.Jarvise;
using LinqToDB.DataProvider.SQLite;
using LinqToDB.SchemaProvider;
using StorageProviders.SQLite.Extensions;
using System.Data.SQLite;
using TelegramService.Jarvise.Commands;
using TelegramService.Jarvise.Session;

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
			}).ConfigureAppConfiguration(b =>
			{
				b.AddEnvironmentVariables().Build();
				
			}).ConfigureServices((hostContext, services) =>
				{
				services.AddHostedService<RootBot>(/*(IServiceProvider provider)=>
				{
					
				}*/).AddScoped<UserSessionService>(((IServiceProvider provider, UserSessionService options)
					=> (IServiceProvider proovider, options)=> { } ).AddLinqToDb((IServiceProvider provider, LinqToDbConnectionOptionsBuilder options) =>
					{
						/*var b = new System.Data.SQLite.SQLiteConnectionStringBuilder(hostContext.Configuration.GetConnectionString("DbConnectionString"));
						b.CacheSize = 65536;
						b.JournalMode = System.Data.SQLite.SQLiteJournalModeEnum.Wal;
						b.ForeignKeys = true;
						b.RecursiveTriggers = true;
						b.Enlist = true;
						b.PageSize = 4096;						
						b.ReadOnly = true;
						b.Pooling = true;

						options.UseSQLite(b.ToString()).UseDefaultLogging(provider);		*/
						options.UseConnectionFactory(
							SQLiteTools.GetDataProvider(ProviderName.SQLiteClassic),
							() =>
							{
								var b = new System.Data.SQLite.SQLiteConnectionStringBuilder(hostContext.Configuration.GetConnectionString("DbConnectionString"));
								b.CacheSize = 65536;
								b.JournalMode = System.Data.SQLite.SQLiteJournalModeEnum.Wal;
								b.ForeignKeys = true;
								b.RecursiveTriggers = true;
								b.Enlist = true;
								b.PageSize = 4096;
								b.ReadOnly = true;
								b.Pooling = true;
								return new SQLiteConnection(b.ToString());
							}).UseDefaultLogging(provider);
					});
					services.AddScoped<SQLiteStorage>();
					services.AddScoped<IReplier, DefaultReplier>();
					services.AddTransient((IServiceProvider collection) =>
					{
						var res = new RootBotMenuOptions();
						res.UseMenuItem<RootBot.StartCommand>();
						return res;
					});
					//services.Configure<RootBot>(conf => 
				});
	}

}
