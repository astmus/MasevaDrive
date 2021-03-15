#if NET5_0
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.SqlQuery;
using Telegram.Bot.AsyncUpdate;
using Telegram.Bot.Interaction;
using Telegram.Bot.Requests;
using Telegram.Bot.Sessions;
using Telegram.Bot.Storage.InteractionHandlers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramService;
using TelegramService.Jarvise.Interaction;
using TelegramService.Storage;

namespace Telegram.Bot.ExtensionsBase
{
#nullable enable
	
	using CommandHandleDelegate = Func<IInteractionContext, CancellationToken, Task<HandleResult>>;
	using UseCondition = Predicate<ICommand>;
	using UseModuleCondition = Predicate<IInteractionModule>;
	using SeedFunc = Func<IInteractionContext, BaseHandler<IInteractionContext>>;
	//using UseModuleCommandCondition = Predicate<string>;
	/// <summary>
	/// Telegram bot for manage storage files
	/// </summary>
	public class StorageTelegramBot<T> : QueuedUpdateTelegramBot, IDisposable where T : class, IInteractionContext, new()
	{
		#region private
		private static RootBotOptions defOptions = new RootBotOptions();
		Dictionary<string, SeedFunc> nucleoStorage = new Dictionary<string, SeedFunc>();
		#endregion
		#region public
		public readonly SessionDispatcher SessionDispatcher;
		#endregion
		public StorageTelegramBot() : base(defOptions)
		{
			//SessionDispatcher = new SessionDispatcher(new List<RegisteredUser>(defOptions.RegisteredUsersSource));
		}

		/*public void StartReceivingQueue(CancellationToken cancellationToken, Func<Exception, CancellationToken, Task>? errorHandler = default)
		{
			var listenUpdates = 
			QueueReciever.StartReceiving(listenUpdates, errorHandler, cancellationToken: cancellationToken);
		}*/

		public override void StartReceiving(UpdateType[]? allowedUpdates = default, Func<Exception, CancellationToken, Task>? errorHandler = default, CancellationToken cancellationToken = default)
		{
			var updates = allowedUpdates ?? new UpdateType[] { UpdateType.Message, UpdateType.InlineQuery, UpdateType.ChosenInlineResult, UpdateType.CallbackQuery, UpdateType.EditedMessage, UpdateType.ChannelPost, UpdateType.EditedChannelPost };
			base.StartReceiving(updates, errorHandler, cancellationToken);
		}

		public BaseHandler<T> MakeHandler(T context)
		{
			//if (nucleoStorage.ContainsKey(context.RawCommand/*.Signature*/))
			//	return nucleoStorage[context.RawCommand/*.Signature*/](context);
	
			//return ResolveHandler(context);
			return null!;
		}

		private BaseHandler<T> ResolveHandler(T context)
		{
			return null!;//new FSInteractionModule();
		}

		[InteractionsSupported(new string[] { "/start", "/menu", "/reset", "/отмена", "/назад" })]
		public class BotCommandHandler : BaseHandler<InteractionContext>, ICommandSupport
		{
			private static readonly string[] supportedCommands = { "/start", "/menu", "hide_menu", "/cancel", "/reset" };
			private bool disposedValue;

			public Predicate<Update> IsCommandSupport => u => supportedCommands.Contains(u.RawCommand());

			Func<Update, Type> ICommandSupport.IsCommandSupport { get; } = null!;

			public BotCommandHandler()
			{
			}

			protected virtual void Dispose(bool disposing)
			{
				if (!disposedValue)
				{
					if (disposing)
					{
						// TODO: dispose managed state (managed objects)
					}

					// TODO: free unmanaged resources (unmanaged objects) and override finalizer
					// TODO: set large fields to null
					disposedValue = true;
				}
			}

			public void Dispose()
			{
				// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
				Dispose(disposing: true);
				GC.SuppressFinalize(this);
			}

			/*public override void Configure(InteractionContext Context)
{
DisplayContent = DisplayContent;
switch (Context.Message.Text)
{
case "/start":
TextLabel = "Hello my name is Jarvise.You are registered";
ResetKeyboard = true;
break;
case "/menu":
TextLabel = "Основное меню";
Options = new string[] { "Папки и файлы", "Просмотр последних", "Альбомы", "Ярлыки", "Настройки" };
Controls = new string[] { "Отмена" };
break;
case "/reset":
TextLabel = "kb";
ResetKeyboard = true;
break;
case "Назад":
case "Отмена":
//UseThisState = Context.Session.PreviousState;
ResetKeyboard = true;
break;
default:
break;

}
}*/
		}
	}

	public static class ParticleExtension
	{
		public static ModuleParticle Use<TModule>(this IInteractionContext ctx, TModule module) where TModule : IInteractionModule => new Use(module, ctx.Command); //evolute
		public static CommandParticle When(this ModuleParticle modulePart, UseModuleCondition umc) => modulePart.TryModule(umc); //evolute
		public static ModuleParticle Use<TAModule>(this CommandParticle cmd, TAModule module) where TAModule : IInteractionModule => cmd.SetModule(module); //regression		
		public static UseParticle By(this CommandParticle cmdPart, CommandHandleDelegate mt) => cmdPart.SetMethod(mt); //evolute
		public static Use If(this UseParticle usePart, UseCondition uc) => usePart.TryMethod(uc);//evolution
		public static CommandParticle By(this UseParticle usePart, CommandHandleDelegate mt) => usePart.SetMethod(mt);//regression


		//public static ModuleParticle When(this ModuleParticle modulePart,  uc) => modulePart; //regressoin
	}

	public abstract class ModuleParticle : IModuleParticle
	{
		protected ModuleParticle() { }
		public IInteractionModule Module { get; protected set; } = null!;
		public ICommand Command { get; protected set; } = null!;
		public UseModuleCondition WhenPredicate { get; protected set; } = null!;
		public UseParticle SetModule(IInteractionModule module) { Module = module; return (UseParticle)this; }
		public CommandParticle TryModule(UseModuleCondition predicate)
		{
			if (predicate(Module))
			{
				WhenPredicate = predicate;
				return (CommandParticle)this;
			}
			return null!;
		}
		//public bool Applied => WhenPredicate != null;
	}

	internal interface IModuleParticle
	{
	}

	public abstract class CommandParticle : ModuleParticle, ICommandParticle
	{
		protected CommandParticle() { }
		public CommandHandleDelegate Method { get; protected set; } = null!;
		public UseCondition IfPredicate { get; protected set; } = null!;
		private new CommandParticle TryModule(UseModuleCondition predicate) => null!;
		public UseParticle SetMethod(CommandHandleDelegate predicate) { Method = predicate; return (UseParticle)this; }
	}

	internal interface ICommandParticle
	{
	}

	public class UseParticle : CommandParticle, IUseParticle
	{
		protected UseParticle() { }
		private new UseParticle SetMethod(CommandHandleDelegate predicate) => null!;
		public Use TryMethod(UseCondition predicate) => null!;
	}

	internal interface IUseParticle
	{
	}

	public class Use : UseParticle, IUse
	{
				
		public Use(IInteractionModule module, ICommand command)
		{
			Command = command;
			Module = module;
		}
		public CommandHandleDelegate Resolve() => new CommandHandleDelegate(Method);
	}

	internal interface IUse
	{
		public CommandHandleDelegate Resolve();
	}


	public static class Command
	{
		public static Predicate<string> EqualToFSModuleName = s => s == "Папки и файлы";
	}
}
#nullable disable
#endif