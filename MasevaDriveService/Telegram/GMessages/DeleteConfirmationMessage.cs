using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	[TelegramMessageHandler("2", typeof(DeleteConfirmationMessage))]
	internal class DeleteConfirmationMessage : GMessage
	{
		public override Task Replay()
		{
			var error = StorageItemsProvider.Instance.MoveToTrash(Data.FileHash);
			if (error == null)
				return Owner.DeleteMessageAsync(ChatID, MessageID, default);
			else
				return Owner.AnswerCallbackQueryAsync(query.Id, error, true);
		}
	}
}
