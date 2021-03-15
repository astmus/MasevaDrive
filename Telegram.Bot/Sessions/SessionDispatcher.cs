using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Interaction;
using Telegram.Bot.Types;

namespace Telegram.Bot.Sessions
{
	/// <summary>
	/// 
	/// </summary>
	public class SessionDispatcher : BaseSessionDispatcher
	{
		/// <summary>
		/// 
		/// </summary>
		public SessionDispatcher(IEnumerable<RegisteredUser> users) : base()
		{
			this.Users = users;
		}

		public IEnumerable<RegisteredUser> Users { get; set; }

		public void DispatchSession(IInteractionContext context)
		{
			var session = this[context.User.Id];
			if (session == null)
			{
				var regInfo = Users.FirstOrDefault(regUser => regUser.Id == context.User.Id);
				if (regInfo != null) //user registered but session not oppened yet
					session = StartSession(context.User.ToRegisteredUser(regInfo));
				else
				{
					session = new Session();
					session.User = context.User.ToRegisteredUser("astmus@biruza.com", (int)context.Message.Chat.Id);
					CurrentSessions.Add(session);
					//start registration procedure place it in RouteInteraction with check if session is null
				}
			}
			session.MessagesHystory.Add(context.Message);
			context.Session = session;
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
