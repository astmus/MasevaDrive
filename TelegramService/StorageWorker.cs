using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StorageProviders.NetCore.DBs.SQLite;
using Telegram.Bot;
using Telegram.Bot.Connectivity;
using Telegram.Bot.Storage;
using Telegram.Bot.Storage.InteractionHandlers;

namespace TelegramService
{
	/// <summary>
	/// 
	/// </summary>
	public class DefaultStorageBotOptions : IBotOptions
	{
		/// <summary>
		/// 
		/// </summary>
		public string Username => "Jarvise";
		/// <summary>
		/// 
		/// </summary>
		public string ApiToken => Environment.GetEnvironmentVariable("JarviseKey", EnvironmentVariableTarget.User);
		/// <summary>
		/// 
		/// </summary>
		public string WebhookPath => null;
		/// <summary>
		/// 
		/// </summary>
		public TimeSpan Timeout => TimeSpan.FromMinutes(2);
		/// <summary>
		/// 
		/// </summary>
		public IEnumerable<RegisteredUser> RegisteredUsersSource
		{
			get
			{
				return new RegisteredUser[] { new RegisteredUser() { Email = "astmus@live.com", ChatId = 506545376, Id = 506545376 }, new RegisteredUser() { Email = "olgas88@live.com", ChatId = 355747145 } };
			}
		}
	}

	public class StorageWorker : BackgroundService
	{
		private readonly ILogger<StorageWorker> _logger;

		private StorageTelegramBot _storageBot;

		public StorageWorker(ILogger<StorageWorker> logger)
		{
			_logger = logger;
		}

		public IServiceProvider Services { get; }
		public StorageWorker(IServiceProvider services, ILogger<StorageWorker> logger)
		{
			Services = services;
			_logger = logger;
		}

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			var options = new DefaultStorageBotOptions();
			_storageBot = new StorageTelegramBot(new DefaultStorageBotOptions());
			_storageBot.InteractionRouter = new StorageInteractionsRouter();

			_storageBot.StartReceiving(null, null, cancellationToken);
			await base.StartAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Storage Worker running at: {time}", DateTimeOffset.Now);

			await foreach (var update in _storageBot.YieldUpdatesAsync())
			{
				if (cancellationToken.IsCancellationRequested)
					return;
				_logger.LogInformation(update.ToString());
				using (var scope = Services.CreateScope())
				{
					SQLiteProvider dbProvider = scope.ServiceProvider.GetRequiredService<SQLiteProvider>();
					using (var handler = _storageBot.CreateInteractionHandler(update, context => { context.StorageItemsProvider = dbProvider; }))
					{
						try
						{
							await handler.HandleAsync(cancellationToken);
						}
						catch (Exception error)
						{
							await handler.HandleErrorAsync(error, cancellationToken);
						}
					}
				}
			}
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			_storageBot.StopReceiving();
			await base.StopAsync(cancellationToken);
		}
	}
}
