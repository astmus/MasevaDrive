using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using static MasevaDriveService.RequestData;
using LineButton = Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton;
namespace MasevaDriveService
{
	public class RequestButton : RequestFormatting, IKeyboardButton
	{		
		[JsonIgnore]
		public string Text { get; set; }
		public static RequestButton NewLine => null;

		public RequestButton(RequestKind action, string title, string[] parameters)
		{
			Action = action;
			Text = title;
			Parameters = parameters;			
		}

		public RequestButton()
		{
			FileHash = TelegramContext.Current?.Data.FileHash;
		}

		public RequestButton(RequestKind action, string title) :this()
		{
			Action = action;
			Text = title;			
		}

		public static explicit operator LineButton(RequestButton button)
		{
			var callbackRequestfromat = JsonConvert.SerializeObject(button);
			return LineButton.WithCallbackData(button.Text, callbackRequestfromat);
		}

		public static implicit operator RequestButton((RequestKind kind, string title) data) => new RequestButton(data.kind, data.title);		
	}
}
