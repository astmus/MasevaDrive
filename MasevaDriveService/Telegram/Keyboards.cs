using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MasevaDriveService
{
	public static class Keyboards 
	{
		public static InlineKeyboardMarkup CommonFileActionsKeyboard(string hash)
		{
			return new InlineKeyboardMarkup(new InlineKeyboardButton[]
			{
				InlineKeyboardButton.WithCallbackData("удалить", CallbackData.Build(MesssageAction.Delete,hash))
			});
		}

	}
}
