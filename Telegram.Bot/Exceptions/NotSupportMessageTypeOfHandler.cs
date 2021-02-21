using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Exceptions
{
	/// <summary>
	/// 
	/// </summary>
	public class NotSupportMessageTypeOfHandler : Exception
	{
		/// <summary>
		/// 
		/// </summary>
		public NotSupportMessageTypeOfHandler(MessageType type) : base("This handler does not support messages of type " + type.ToString())
		{

		}
	}
}
