using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Types;

namespace Telegram.Bot.AsyncUpdate
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class AsyncUpdateTelegramBot : TelegramBot, IYieldingUpdateReceiver
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="options"></param>
		public AsyncUpdateTelegramBot(IBotOptions options) : base(options)
		{

		}
		/// <summary>
		/// 
		/// </summary>
		public abstract int PendingUpdates { get; }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public abstract IAsyncEnumerable<Update> YieldUpdatesAsync();
	}
}