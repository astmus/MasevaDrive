using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public class DefaultInteractionRouter : IInteractionRouter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public IInteractionHandler RouteInteraction(InteractionContext context)
		{
			return new InteractionHandler() { Context = context };
		}
	}
}
