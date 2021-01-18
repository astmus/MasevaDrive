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
	[TelegramMessageHandler("1", typeof(DeleteFileMessage))]
	internal class DeleteFileMessage : GMessage
	{
		public override Task Replay()
		{
			return Owner.EditMessageReplyMarkupAsync(ChatID, MessageID, ConfirmLeyboard(), default);

			/*
			 
			 if (StorageItemsProvider.Instance[hash] != null)
				await Bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);
			
			if (result == MessageConfirmLevel.NeedConfirm)
				await Bot.EditMessageReplyMarkupAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId, MakeDeleteKeyboard(hash,MessageConfirmLevel.WaitDecision), default(CancellationToken));
			else
				await Bot.DeleteMessageAsync(e.CallbackQuery.Message.Chat.Id, e.CallbackQuery.Message.MessageId);*/
			/*if (RequestDeleteFile != null)
				RequestDeleteFile(e.CallbackQuery.Data);*/
			
		}

		private InlineKeyboardMarkup ConfirmLeyboard()
		{
			return new InlineKeyboardMarkup(new InlineKeyboardButton[]
			{
				InlineKeyboardButton.WithCallbackData("да, удалить", CallbackData.Build(MesssageAction.ConfirmDelete,Data.FileHash)),
				InlineKeyboardButton.WithCallbackData("отмена", CallbackData.Build(MesssageAction.Cancel,Data.FileHash))
			});
		}
	}
}
