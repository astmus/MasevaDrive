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
		public Update Interaction { get; set; }
		///
		public User User { get; set; }
		///
		public Session Session { get; set; }
		///
		public IBot Connection { get; set; }
		///
		public bool IsAuthorizedUser => User as RegisteredUser != null;
		///
		public InteractionContext()
		{

		}
		///
		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3}", Interaction.ToString(), User.ToString(), Session?.ToString(), Connection?.ToString());
		}
	}
}
