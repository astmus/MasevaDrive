using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Interaction;

namespace TelegramService.Jarvise.Interfaces
{
	public interface IReplier
	{
		public Task ReplyAsync(IHandleResult replyInfo, IInteractionContext context, CancellationToken cancelToken);
	}
}
