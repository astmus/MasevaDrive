using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Telegram.Bot.Connectivity
{
	interface ISessionDispatcher
	{
		ConcurrentBag<Session> CurrentSessions { get; }
		Session StartSession(RegisteredUser user);
		void ResetSession();
		public InteractionContext DispatchInteractionToSession(Update interaction);

	}
}
