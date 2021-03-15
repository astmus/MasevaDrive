using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Sessions;

namespace TelegramService
{
	/// <summary>
	/// 
	/// </summary>
	public class RootBotOptions : IBotOptions
	{
		/// <summary>
		/// 
		/// </summary>
		public string Username => "AliseBot";
		/// <summary>
		/// 
		/// </summary>
		public string ApiToken => Environment.GetEnvironmentVariable("AliseBot", EnvironmentVariableTarget.User);
		/// <summary>
		/// 
		/// </summary>
		public string WebhookPath => null;
		/// <summary>
		/// 
		/// </summary>
		public TimeSpan Timeout => TimeSpan.FromMinutes(2);
		/// <summary>
		/// 
		/// </summary>
		
		public static RootBotOptions Default => new RootBotOptions();
	}
}
