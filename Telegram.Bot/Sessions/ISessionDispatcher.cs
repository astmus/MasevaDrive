using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Telegram.Bot.Sessions
{
	interface ISessionDispatcher
	{
		ConcurrentBag<Session> CurrentSessions { get; }
		Session StartSession(RegisteredUser user);
		void ResetSession();
	}
}
