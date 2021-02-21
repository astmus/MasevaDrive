using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MasevaDriveService
{
	[CallbackQueryHandler(RequestKind.Delete, typeof(DeleteFileQueryHandler))]
	internal class DeleteFileQueryHandler : QueryHandler
	{
		public override Task Handle()
		{
			return Owner.EditMessageReplyMarkupAsync(ChatID, MessageID, DeleteKeyboard, default);
			/*
			 if (StorageItemsProvider.Instance[hash] != null)
				await Bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);
			
			if (result == MessageConfirmLevel.NeedConfirm)
				await Bot.EditMessageReplyMarkupAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId, MakeDeleteKeyboard(hash,MessageConfirmLevel.WaitDecision), default(CancellationToken));
			else
				await Bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);*/
			/*if (RequestDeleteFile != null)
				RequestDeleteFile(e.CallbackQuery.Data);
			*/			
		}

		/*public override void Send()
		{
			Owner.SendTextMessageAsync(ChatID, "File was deleted");
		}*/

		private static RequestKeyboard DeleteKeyboard = new RequestKeyboard((RequestKind.ConfirmDelete, "confirm delete"), (RequestKind.Cancel, "cancel"));
		
			/*return new InlineKeyboardMarkup(new InlineKeyboardButton[]
			{
				InlineKeyboardButton.WithCallbackData("yes, delete", (RequestData)RequestKind.ConfirmDelete),// FileHash = Data.FileHash)),
				InlineKeyboardButton.WithCallbackData("cancel", (RequestData)RequestKind.Cancel)// Data.FileHash))
			});*/
	}
}
