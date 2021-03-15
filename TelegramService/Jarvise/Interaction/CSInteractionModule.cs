using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Interaction;
using Telegram.Bot.Types;

namespace TelegramService.Jarvise.Interaction
{
	public class CSInteractionModule : IInteractionModule
	{
		public string Title { get; } = "Коллекция";
		public string CommandsSuffix { get; } = "CS";
		public Func<Update, Type> IsCommandSupport { get; }
	}
}
