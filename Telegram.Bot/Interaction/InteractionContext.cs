using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Interaction;
using Telegram.Bot.Sessions;
using Telegram.Bot.Types;

namespace Telegram.Bot.Interaction
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public class InteractionContext : IInteractionContext
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	{
		public Update Update { get; }
		public Session Session { get; set; }
		public User User { get; }
		public Message Message { get; }
		public ITelegramBot BotConnection { get; set; }
		public string RawCommand { get; }
		public ICommand Command { get; }
	}
}
