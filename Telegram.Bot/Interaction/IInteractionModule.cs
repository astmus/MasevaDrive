using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Types;
using Telegram.Bot.Interaction;

namespace Telegram.Bot.Interaction
{
	public static class UpdateExtension
	{
		public static string RawCommand(this Update u) => u?.Message.Text ?? u?.ChannelPost.Text ?? u?.EditedChannelPost.Text ?? u?.EditedMessage.Text ?? u?.InlineQuery.Query ?? "";
	}

	public interface IInteractionModule : ICommandSupport
	{
		public string Title { get; }
		public string CommandsSuffix { get; }
		//public T InitContext<T>(Update u) where T : class, IInteractionContext, new();
		//public HandleUnit MakeHandler<T>(T ctx) where T : IInteractionContext;
	}

	public interface ICommandSupport
	{
		public Func<Update,Type> IsCommandSupport { get; }
	}
}
