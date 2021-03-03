using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public class DefaultInteractionRouter : IInteractionRouter<InteractionContext>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public IInteractionHandler<InteractionContext> RouteInteraction(InteractionContext context)
		{
			return new InteractionHandler() { Context = context };
		}
	}
}
