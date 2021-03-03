using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Connectivity;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Storage.InteractionHandlers
{
	///
	public class StorageInteractionContext : InteractionContext
	{
		///
		public object StorageItemsProvider { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	public class MainCommandsHandler : BaseInteractionHandler<StorageInteractionContext>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public MainCommandsHandler(StorageInteractionContext context) : base(context)
		{

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		public override async Task HandleAsync(CancellationToken cancelToken)
		{
			if (TypeOfMessage != MessageType.Text)
				throw new NotSupportMessageTypeOfHandler(TypeOfMessage);
			Message m = null;
			switch (Command)
			{
				case "/start":
					goto case "/mainmenu";
				case "/mainmenu":
					m = await Context.Connection.SendTextMessageAsync(ChatId, "Основное меню", GetKeyboard(new List<string>() { "Просмотр папок", "Просмотр недавних", "Скрыть" }), cancelToken);
					break;
				case "/browse":
					var board = GetKeyboard(new List<string>() { "Просмотр папок", "Просмотр недавних", "Скрыть" });
					m = await Context.Connection.SendTextMessageAsync(ChatId, "сформировать запрос", ParseMode.Default, false, false, 0, new ReplyKeyboardRemove(), cancelToken);
					break;
				default:
					break;

			}
			if (m != null)
				AddMessageToHistory(m);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="error"></param>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		public override async Task HandleErrorAsync(Exception error, CancellationToken cancelToken)
		{
#if NET5_0
			await new ValueTask();
#else
			await Task.FromResult(1);
#endif
		}

		private string Command => Context.Interaction.Message.Text;
	}
}
