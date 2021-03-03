using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;
using System.Linq;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class BaseCentralDispatcher<T> : ICentralDispatcher<T> where T : InteractionContext, new()
	{
		/// <summary>
		/// 
		/// </summary>
		protected List<RegisteredUser> RegisteredUsers { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public SessionDispatcher SessionDispatcher { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public abstract IInteractionRouter<T> InteractionRouter { get; set; }
		/// <summary>
		/// 
		/// </summary>
		protected IBot Connection { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="usersInfoProvider"></param>
		public BaseCentralDispatcher(IEnumerable<RegisteredUser> usersInfoProvider)
		{

			RegisteredUsers = new List<RegisteredUser>(usersInfoProvider);
			SessionDispatcher = new SessionDispatcher();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="interaction"></param>
		/// <returns></returns>
		public IInteractionHandler<T> CreateInteractionHandler(Update interaction)
		{
			int senderUserId = interaction.GetOwner().Id;
			var session = SessionDispatcher[senderUserId];
			if (session == null)
			{
				var regInfo = RegisteredUsers.FirstOrDefault(registered => registered.Id == senderUserId);
				if (regInfo != null) //user registered but session not oppened yet
					session = SessionDispatcher.StartSession(interaction.GetOwner().ToRegisteredUser(regInfo));
				else
				{
					//start registration procedure place it in RouteInteraction with check if session is null
				}
			}

			var t = new T();
			t.Session = session;
			t.User = session.User;
			t.Interaction = interaction;
			t.Connection = Connection;

			return InteractionRouter.RouteInteraction(t);
		}
	}
}
