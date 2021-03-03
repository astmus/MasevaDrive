using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Connectivity;
using Telegram.Bot.Storage.InteractionHandlers;

namespace Telegram.Bot.Storage
{
	///
	public class StorageInteractionsRouter : IInteractionRouter<StorageInteractionContext>
	{
		///
		public IInteractionHandler<StorageInteractionContext> RouteInteraction(StorageInteractionContext context)
		{
			return new MainCommandsHandler(context);
		}
	}
}
