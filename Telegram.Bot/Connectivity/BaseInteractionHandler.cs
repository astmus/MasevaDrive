using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class BaseInteractionHandler<T> : IInteractionHandler<T> where T : InteractionContext
	{
		private bool disposedValue;

		/// <summary>
		/// 
		/// </summary>
		public BaseInteractionHandler(T context)
		{
			Context = context;
			AddMessageToHistory(Message);
		}
		/// <summary>
		/// 
		/// </summary>
		public T Context { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Message Message => Context.Interaction.Message;
		/// <summary>
		/// 
		/// </summary>
		public long ChatId => Context.Interaction.Message.Chat.Id;
		/// <summary>
		/// 
		/// </summary>
		public MessageType TypeOfMessage => Context.Interaction.Message.Type;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		public abstract Task HandleAsync(CancellationToken cancelToken);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="error"></param>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		public abstract Task HandleErrorAsync(Exception error, CancellationToken cancelToken);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		protected virtual void AddMessageToHistory(Message message)
		{
			Context.Session.MessagesHystory.Add(message);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		protected static ReplyKeyboardMarkup GetKeyboard(List<string> keys)
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
			return rkm;
		}
		///
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					Context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~BaseInteractionHandler()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }
		///
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
