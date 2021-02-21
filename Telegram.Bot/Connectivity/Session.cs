using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public class Session
	{
		/// <summary>
		/// 
		/// </summary>
		public RegisteredUser User { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Session()
		{

		}
		/// <summary>
		/// 
		/// </summary>
		public List<Message> MessagesHystory = new List<Message>();
	}
}
