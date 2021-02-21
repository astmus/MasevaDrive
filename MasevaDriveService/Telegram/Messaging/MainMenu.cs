using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MasevaDriveService
{
	public static class TelegramBotClientExt
	{
		public static Task SendMainMenu(this TelegramBotClient client, long chatId)
		{
			return client.SendTextMessageAsync(chatId, "Main menu",ParseMode.Default,false,false,0, Keyboards.MainMenu(), default);
		}
	}

	[CallbackQueryHandler(RequestKind.MainMenu,typeof(MainMenuTelegram))]
	class MainMenuTelegram : QueryHandler
	{
		public override Task Handle()
		{
			return Owner.SendTextMessageAsync(ChatID, "Main menu", ParseMode.Default, true, false, 0, Keyboards.MainMenu(), default);
		}		
	}

	//[CallbackQueryHandler(RequestKind.MainMenuPhotos, typeof(NavigationQueryHandler))]
	//[CallbackQueryHandler(RequestKind.MainMenuVideos, typeof(NavigationQueryHandler))]
	[CallbackQueryHandler(RequestKind.MainMenuFiles, typeof(NavigationQueryHandler))]
	class NavigationQueryHandler : QueryHandler
	{
		public override Task Handle()
		{			
			return Owner.EditMessageReplyMarkupAsync(ChatID, MessageID, Keyboards.Navigation(), default);
		}
	}

	
}
