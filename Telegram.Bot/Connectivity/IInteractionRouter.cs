using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public interface IInteractionRouter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public IInteractionHandler RouteInteraction(InteractionContext context);
	}
}
