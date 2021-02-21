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
		public static InlineKeyboardMarkup MainMenu() => new RequestKeyboard(
									(RequestKind.LastPhotos, "last photos"),
									RequestButton.NewLine, (RequestKind.LastVideos, "last videos"),
									RequestButton.NewLine , (RequestKind.LastFiles, "last files")
				);

		public static InlineKeyboardMarkup Navigation() => new RequestKeyboard(
									(RequestKind.Prev, "<"), (RequestKind.Prev, ">"), RequestButton.NewLine,
									(RequestKind.Back, "back")
				);

		public static InlineKeyboardMarkup CommonFileActions() => new RequestKeyboard(
				(RequestKind.Delete, "delete")
			);
		

		/*
		 public static RequestKeyboard CommonFileActions = new RequestKeyboard()
		{
			new RequestKeyboardRow{
				RequestKind.Delete,new RequestButton()
			},
			new RequestKeyboardRow{
				new RequestButton(),new RequestButton()
			},
		};
		 */
	}

}
