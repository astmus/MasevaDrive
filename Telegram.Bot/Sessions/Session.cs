using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Sessions
{
	public enum SessionState
	{
		Default,
		Browse
	}
	/// <summary>
	/// 
	/// </summary>
	public class Session
	{
		public SessionState State { get; set; }
		/// <summary>
		/// 
		/// </summary>		
		public RegisteredUser User { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Session()
		{

		}
		/// <summary>
		/// 
		/// </summary>
		public List<Message> MessagesHystory = new List<Message>();
		///
		public Stack<string> PreviousStates = new Stack<string>();

		///
		//public void BackupState(IInteractionResult info)
		//{
			//PreviousStates.Push(JsonConvert.SerializeObject(info));
		//}
		///
		//public IInteractionResult PreviousState => PreviousStates.Count > 0 ? JsonConvert.DeserializeObject<IInteractionResult>(PreviousStates.Pop()) : null;
	}
}
