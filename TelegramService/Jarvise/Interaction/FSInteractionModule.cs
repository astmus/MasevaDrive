using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Interaction;
using Telegram.Bot.Types;
using TelegramService.Storage;

namespace TelegramService.Jarvise.Interaction
{	
	[Description("File system interaction module")]
	public class FSInteractionModule : IInteractionModule
	{
		public const string Title = "Папки и файлы";
		public const string CommandsSuffix = "FS";
		public Func<Update, Type> IsCommandSupport => DefaultPredicate;

		string IInteractionModule.Title => Title;
		string IInteractionModule.CommandsSuffix => CommandsSuffix;

		public static Func<Update, Type> DefaultPredicate = u => u.RawCommand() == Title || u.RawCommand().Split('.')?[0] == CommandsSuffix ? typeof(FSInteractionModule) : null;

        /*public T InitContext<T>(Update u) where T :class, IInteractionContext, new()
        {
            return new T() { Update = u };
        }

		public HandleUnit MakeHandler<T>(T ctx) where T: IInteractionContext
		{
			return ctx.Use(OnModuleSelectedAsync).When(Command.EqualToModuleName);
			
			//return new HandleUnit();
		}*/


		public async Task<HandleResult> OnModuleSelectedAsync(IInteractionContext context, CancellationToken cancelToken)
		{
			var result = new HandleResult();



			return result;
		}
	}
}

