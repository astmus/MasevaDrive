using System;
using System.Collections.Generic;
using Telegram.Bot.Sessions;
#if NET472
using System.Runtime.InteropServices.WindowsRuntime;
#endif

namespace Telegram.Bot
{
	/// <summary>
	/// Configurations for the bot
	/// </summary>
	public interface IBotOptions
	{
		/// <summary>
		/// 
		/// </summary>
		string Username { get; }

		/// <summary>
		/// Optional if client not needed. Telegram API token
		/// </summary>
		string ApiToken { get; }
		/// <summary>
		/// 
		/// </summary>
		string WebhookPath { get; }
		/// <summary>
		/// 
		/// </summary>
		TimeSpan Timeout { get; }
		/// <summary>
		/// 
		/// </summary>
	}
}
