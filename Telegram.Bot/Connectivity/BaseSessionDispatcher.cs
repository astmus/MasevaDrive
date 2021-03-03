using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot.Types;

namespace Telegram.Bot.Connectivity
{   /// <summary>
	/// 
	/// </summary>
	public abstract class BaseSessionDispatcher : ISessionDispatcher
	{/// <summary>
	 /// 
	 /// </summary>
		public ConcurrentBag<Session> CurrentSessions { get; protected set; }
		/// <summary>
		/// 
		/// </summary>
		public BaseSessionDispatcher()
		{
			CurrentSessions = new ConcurrentBag<Session>();
		}
		/// <summary>
		/// 
		/// </summary>
		public abstract Session StartSession(RegisteredUser user);
		/// <summary>
		/// 
		/// </summary>
		public abstract void ResetSession();
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public Session this[RegisteredUser user] => CurrentSessions.FirstOrDefault(s => s.User.Email == user.Email);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public Session this[string email] => CurrentSessions.FirstOrDefault(s => s.User.Email == email);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public Session this[int userId] => CurrentSessions.FirstOrDefault(s => s.User.Id == userId);

	}
}
