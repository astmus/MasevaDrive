using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public class SessionDispatcher : BaseSessionDispatcher
	{
		/// <summary>
		/// 
		/// </summary>
		public SessionDispatcher() : base()
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public override void ResetSession()
		{
			//clear hystory etc	
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public override Session StartSession(RegisteredUser user)
		{
			var result = new Session() { User = user };
			CurrentSessions.Add(result);
			return result;
		}
	}
}
