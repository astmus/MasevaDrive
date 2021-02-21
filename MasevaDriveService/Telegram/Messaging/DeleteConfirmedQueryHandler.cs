using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MasevaDriveService
{
	[CallbackQueryHandler(RequestKind.ConfirmDelete, typeof(DeleteConfirmedQueryHandler))]
	internal class DeleteConfirmedQueryHandler : QueryHandler
	{
		string fileName;
		public override string AnswerMessage()
		{
			return "File " + fileName + " deleted";
		}

		public override Task Handle()
		{
			var error = StorageItemsProvider.Instance.MoveToTrash(Data.FileHash, ref fileName);
			if (error == null)
				return Owner.DeleteMessageAsync(ChatID, MessageID, default);
			else
				return RaiseError(error);
		}
	}
}
