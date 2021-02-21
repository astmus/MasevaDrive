using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Connectivity;
using Telegram.Bot.Storage.InteractionHandlers;

namespace Telegram.Bot.Storage
{
	class StorageInteractionsRouter : IInteractionRouter
	{
		public IInteractionHandler RouteInteraction(InteractionContext context)
		{
			return new MainCommandsHandler(context);
		}
	}
}
