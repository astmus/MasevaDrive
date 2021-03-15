using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Interaction;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramService.Jarvise.Interfaces;

namespace TelegramService.Jarvise
{
	class DefaultReplier : IReplier, IDisposable
	{
		private bool disposedValue;

		public async Task ReplyAsync(IHandleResult replyInfo, IInteractionContext context, CancellationToken cancelToken)
		{
			Message response = context.Message;
			var info = /*replyInfo.UseThisState ?? */replyInfo;
			//await context.Bot.DeleteMessageAsync(response.Chat.Id, response.MessageId, cancelToken);		
			
			
			var items = new List<string>();
			if (info.Options != null)
				items.AddRange(info.Options);
			if (info.Controls != null)
				items.AddRange(info.Controls);

			ReplyMarkupBase board = replyInfo.ResetKeyboard != false ? new ReplyKeyboardRemove() : GetKeyboard(items, info.HideKeyboard);
			
			//{
			//	OneTimeKeyboard = false;
			//	ResizeKeyboard = false;
			//}; 		
			
			response = await context.BotConnection.SendTextMessageAsync(context.Update.Message.Chat.Id, info.TextLabel ?? context.Message.Text, board, cancelToken);			

			context.Session.MessagesHystory.Add(response);
		}

		protected ReplyKeyboardMarkup GetKeyboard(IEnumerable<string> keys, bool hide)
		{
			var rkm = new ReplyKeyboardMarkup();
			var rows = new List<KeyboardButton[]>();
			var cols = new List<KeyboardButton>();
			foreach (var t in keys)
			{
				cols.Add(new KeyboardButton(t));
				rows.Add(cols.ToArray());
				cols = new List<KeyboardButton>();
			}
			rkm.Keyboard = rows.ToArray();
			rkm.OneTimeKeyboard = hide;			
			return rkm;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		void IDisposable.Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
