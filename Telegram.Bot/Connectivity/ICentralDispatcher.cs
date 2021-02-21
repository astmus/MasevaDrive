using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public interface ICentralDispatcher
	{
		/// <summary>
		/// 
		/// </summary>
		SessionDispatcher SessionDispatcher { get; set; }
		/// <summary>
		/// 
		/// </summary>
		IInteractionRouter InteractionRouter { get; set; }
	}
}
