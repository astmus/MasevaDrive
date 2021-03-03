using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public interface ICentralDispatcher<T> where T : InteractionContext
	{
		/// <summary>
		/// 
		/// </summary>
		SessionDispatcher SessionDispatcher { get; set; }
		/// <summary>
		/// 
		/// </summary>
		IInteractionRouter<T> InteractionRouter { get; set; }
	}
}
