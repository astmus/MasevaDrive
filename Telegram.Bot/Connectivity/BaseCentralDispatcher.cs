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
	public abstract class BaseCentralDispatcher : ICentralDispatcher
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
		public abstract IInteractionRouter InteractionRouter { get; set; }
		/// <summary>
		/// 
		/// </summary>
		protected IBot Connection { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="concreteRouter"></param>
		/// <param name="usersInfoProvider"></param>
		public BaseCentralDispatcher(IInteractionRouter concreteRouter, IEnumerable<RegisteredUser> usersInfoProvider)
		{
			InteractionRouter = concreteRouter ?? SetupInteractionRouter();
			RegisteredUsers = new List<RegisteredUser>(usersInfoProvider);
			SessionDispatcher = new SessionDispatcher();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual IInteractionRouter SetupInteractionRouter()
		{
			return new DefaultInteractionRouter();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="interaction"></param>
		/// <returns></returns>
		public IInteractionHandler CreateInteractionHandler(Update interaction)
		{
			int senderUserId = interaction.GetOwner().Id;
			var session = SessionDispatcher[senderUserId];
			if (session == null)
			{
				var regInfo = RegisteredUsers.FirstOrDefault(registered => registered.Id == senderUserId);
				if (regInfo != null) //user registered but session not oppened yet
					SessionDispatcher.StartSession(interaction.GetOwner().ToRegisteredUser(regInfo));
				else
				{
					//start registration procedure place it in RouteInteraction with check if session is null
				}
			}

			var context = SessionDispatcher.DispatchInteractionToSession(interaction);
			SetupAPIConnection(context);
			return InteractionRouter.RouteInteraction(context);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		protected abstract void SetupAPIConnection(InteractionContext context);
	}
}
