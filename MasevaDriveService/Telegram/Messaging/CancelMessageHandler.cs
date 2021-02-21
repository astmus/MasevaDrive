using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace MasevaDriveService.Telegram.GMessages
{
	[CallbackQueryHandler(RequestKind.Cancel, typeof(CancelMessageHandler))]
	class CancelMessageHandler : QueryHandler
	{		
		public override Task Handle()
		{		
			return Owner.EditMessageReplyMarkupAsync(ChatID, MessageID, Keyboards.CommonFileActions(), default);
		}
	}
}
