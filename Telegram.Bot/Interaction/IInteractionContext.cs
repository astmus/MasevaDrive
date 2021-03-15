
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Sessions;
using Telegram.Bot.Types;

namespace Telegram.Bot.Interaction
{
	public interface IInteractionContext 
	{		
		Update Update { get; }
		Session Session { get; set; }
		User User { get; }
		Message Message { get; }
		ITelegramBot BotConnection { get; set; }
		public string RawCommand { get; }
		public ICommand Command { get; }
	}
}