using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Sessions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Collections.Concurrent;
using TelegramService.Jarvise.Commands;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace TelegramService
{
	public enum RootBotCommands
	{
		Start,
		Menu,
		HideMenu,
		Reset,
		Cancel,
		Back
		//"/start", "/menu", "/reset", "/отмена", "/назад" 
	}

	public class RootBotMenuOptions : BotMenuOptions<RootBot.Commands>
	{
	}

	public class RootBot : BackgroundService
	{
		private readonly ILogger<RootBot> _logger;
		private readonly TelegramBot telegramBot;
		private QueuedUpdateReceiver updatesProvider;
		private ConcurrentDictionary<User, Action<Update>> updateRouts = new ConcurrentDictionary<User, Action<Update>>();
		public IEnumerable<RegisteredUser> RegisteredUsersSource
		{
			get
			{
				return new RegisteredUser[] { new RegisteredUser() { Email = "astmus@live.com", ChatId = 506545376, Id = 506545376 }, new RegisteredUser() { Email = "olgas88@live.com", ChatId = 355747145 } };
			}
		}
		public RootBot(ILogger<RootBot> logger)
		{
			_logger = logger;
		}

		public IServiceProvider Services { get; }
		public RootBot(IServiceProvider services, ILogger<RootBot> logger)
		{
			Services = services;
			_logger = logger;
			telegramBot = new TelegramBot(RootBotOptions.Default);
			updatesProvider = new QueuedUpdateReceiver(telegramBot);// inherit this object from hjoasted service; telegramBor run as simple service and pass it as param to this
		}

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			try
			{
				var result = await telegramBot.GetMeAsync();
				var menuItems = Services.GetService<RootBotMenuOptions>().BuildInstances();
				await telegramBot.SetMyCommandsAsync(menuItems.Values, cancellationToken);
				_logger.LogInformation(result.ToString());
				var updates = new UpdateType[] { UpdateType.Message, UpdateType.InlineQuery, UpdateType.ChosenInlineResult, UpdateType.CallbackQuery, UpdateType.EditedMessage, UpdateType.ChannelPost, UpdateType.EditedChannelPost };
				updatesProvider.StartReceiving(updates, HandleError, cancellationToken);				
			}
			catch (Exception e)
			{
				_logger.LogInformation(e.ToString());
			}

			await base.StartAsync(cancellationToken);
		}

		private async Task HandleError(Exception error, CancellationToken cancelToken)
		{
			_logger.LogError(error.Message);
			await Task.CompletedTask;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await foreach (Update update in updatesProvider.YieldUpdatesAsync())
			{
				_logger.LogInformation(update.ToString());
				if (update.IsBotCommand())
				{ } //run local command handler
				else
					if (updateRouts.ContainsKey(update.Message.From))
				{
					var Session = updateRouts[update.Message.From];
					Session(update);
				}
				else
				{
					Services.
				}
				//updateRouts.Co

			}
		}
		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			updatesProvider.StopReceiving();
			await base.StopAsync(cancellationToken);
		}

		public class Commands : MenuCommand
		{
		}

		public class StartCommand : Commands
		{			
			public StartCommand(){ Command = "/start"; Description = "Start registration";	}
		}

		public class DisplayMenuCommand : Commands
		{
			public DisplayMenuCommand() { Command = "/menu"; Description = "Display menu"; }
		}
		
		public class ResetCommand : Commands
		{
			public ResetCommand() { Command = "/reset"; Description = "Reset any things"; }
		}
		public class CancelCommand : Commands
		{
			public CancelCommand() { Command = "/cancel"; Description = "Cancel"; }
		}
		public class BackCommand : Commands
		{
			public BackCommand() { Command = "/back"; Description = "Back"; }
		}
	}

	public static class UpdateExt
	{
		public static bool IsBotCommand(this Update update)
		{
			return update.Message?.Entities?.FirstOrDefault()?.Type == MessageEntityType.BotCommand;
		}
	}
}
