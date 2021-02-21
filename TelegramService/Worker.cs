using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Extensions;
using TDLibCore;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramService
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		TelegramBotClient client;
		public Worker(ILogger<Worker> logger)
		{
			_logger = logger;
		}
		public QueuedUpdateReceiver Receiver { get; private set; }
		public override Task StartAsync(CancellationToken cancellationToken)
		{			
			client = new TelegramBotClient(Environment.GetEnvironmentVariable("MasevaBotKey", EnvironmentVariableTarget.User));
			//Receiver = new QueuedUpdateReceiver(client);
			//Receiver.StartReceiving(null, null, cancellationToken);
			return base.StartAsync(cancellationToken);
		}
		public static ReplyKeyboardMarkup GetKeyboard(List<string> keys)
		{
			var rkm = new ReplyKeyboardMarkup();
			var rows = new List<KeyboardButton[]>();
			var cols = new List<KeyboardButton>();
			foreach (var t in keys)
			{
				cols.Add(new KeyboardButton(t));
				rows.Add(cols.ToArray());
				cols = new List<KeyboardButton>();
			}
			rkm.Keyboard = rows.ToArray();
			return rkm;
		}
		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
			int i = 1;
			while(i < 1)
			{
				await Task.Delay(5000);
				var result = await client.SendTextMessageAsync(506545376, i.ToString(), ParseMode.Default, false, false, 0, GetKeyboard(new List<string>() { "1","2","3"}), cancellationToken);
			}
			/*await foreach (var update in Receiver.YieldUpdatesAsync())
			{
				_logger.LogInformation(update.ToString());
				if (cancellationToken.IsCancellationRequested)
					break;
			}*/
		}

		public override Task StopAsync(CancellationToken cancellationToken)
		{			
			return base.StopAsync(cancellationToken);
		}
	}
}
