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

		///
		public IInteractionHandler<T> CreateInteractionHandler(Update interaction, Action<T> configureContext)
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

			var context = new T();
			context.Session = session;
			context.User = session.User;
			context.Interaction = interaction;
			context.Connection = Connection;
			configureContext(context);

			return InteractionRouter.RouteInteraction(context);
		}
	}
}
