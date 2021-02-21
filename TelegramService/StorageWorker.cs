using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Connectivity;
using Telegram.Bot.Storage;

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
		public string ApiToken => Environment.GetEnvironmentVariable("MasevaBotKey", EnvironmentVariableTarget.User);
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
				return new RegisteredUser[] { new RegisteredUser(){ Email="astmus@live.com", ChatId = 506545376, Id = 506545376 }, new RegisteredUser() { Email = "olgas88@live.com", ChatId = 355747145 } };
			}
		}
	}

	public class StorageWorker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;

		private StorageTelegramBot _storageBot;

		public StorageWorker(ILogger<Worker> logger)
		{
			_logger = logger;
		}
		
		public override Task StartAsync(CancellationToken cancellationToken)
		{
			var options = new DefaultStorageBotOptions();
			_storageBot = new StorageTelegramBot(new DefaultStorageBotOptions());

			_storageBot.StartReceiving(null, null, cancellationToken);
			return base.StartAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

			await foreach (var update in _storageBot.YieldUpdatesAsync())
			{
				if (cancellationToken.IsCancellationRequested) return;
				_logger.LogInformation(update.ToString());

				var handler = _storageBot.CreateInteractionHandler(update);
				try
				{
					await handler.HandleAsync(cancellationToken);
				}catch(Exception error)
				{
					await handler.HandleErrorAsync(error, cancellationToken);
				}
			}
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{
			_storageBot.StopReceiving();
			return base.StopAsync(cancellationToken);
		}
	}
}
