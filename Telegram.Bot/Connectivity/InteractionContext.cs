using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public class InteractionContext
	{
		///
		public Update Interaction { get; }
		///
		public User User { get; }
		///
		public Session Session { get; }
		///
		public IBot Connection { get; set; }
		///
		public bool IsAuthorizedUser => User as RegisteredUser != null;
		///
		public InteractionContext()
		{

		}
		///
		public InteractionContext(Update interaction, User user, Session session, IBot bocConnection = null)
		{
			Interaction = interaction;
			User = user;
			Session = session;
			Connection = bocConnection;
		}
		///
		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3}", Interaction.ToString(), User.ToString(), Session?.ToString(), Connection?.ToString());
		}
	}
}
