using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Storage;
using Telegram.Bot.Storage.InteractionHandlers;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Types;
using TelegramService.Storage;
using TelegramService.Jarvise.Interfaces;
using LinqToDB.Common;
using StorageProviders.SQLite;
using System.Linq.Expressions;
using Telegram.Bot.Interaction;
using TelegramService.Jarvise.Interaction;
using System.Security.Cryptography;

namespace TelegramService
{
	public class StorageWorker : BackgroundService
	{
		private readonly ILogger<StorageWorker> _logger;
		private StorageTelegramBot<InteractionContext> _storageBot;
		
		TelegramBot telegramBot;
		public StorageWorker(ILogger<StorageWorker> logger)
		{
			_logger = logger;
		}

		public IServiceProvider Services { get; }
		public StorageWorker(IServiceProvider services, ILogger<StorageWorker> logger)
		{
			Services = services;			
			_logger = logger;
			_storageBot = new StorageTelegramBot<InteractionContext>();
		}

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			var r = await _storageBot.GetMeAsync(cancellationToken);			
			_storageBot.StartReceiving(null, logException, cancellationToken);
			await base.StartAsync(cancellationToken);
		}

		private async Task logException(Exception error, CancellationToken cancelToken)
		{
			_logger.LogError(error.Message);
			await Task.CompletedTask;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			var mms = from m in _storageBot.Updates select m;
			await foreach (Update updateContext in _storageBot.Updates)
			{
				using (var scope = Services.CreateScope())
				{
					Console.WriteLine(updateContext.RawCommand());
					//_storageBot.SessionDispatcher.DispatchSession(updateContext);
					//updateContext.BotConnection = _storageBot; // may be should refactored?

					using (SQLiteStorage dbProvider = scope.ServiceProvider.GetRequiredService<SQLiteStorage>())
					{
						//updateContext.SQLite = dbProvider;						
						/*using (var handler = _storageBot.MakeHandler(updateContext))
						{
							HandleResult result;
							try
							{
								result = await handler.HandleAsync(updateContext, cancellationToken);
							}
							catch (Exception error)
							{
								result = await handler.HandleErrorAsync(error, cancellationToken);
							}
							if (result != null)
							{
								var replier = scope.ServiceProvider.GetRequiredService<IReplier>();
								Console.WriteLine("replier code => " + replier.GetHashCode().ToString());
								await replier.ReplyAsync(result, updateContext, cancellationToken);
								Console.WriteLine(updateContext.Message.Text + " => replied");
							}
							else
								Console.WriteLine($"There is no handler for command {updateContext.Message.Text}");
						}*/
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
