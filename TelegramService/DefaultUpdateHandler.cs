using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramService
{
	class DefaultUpdateHandler : IUpdateHandler
	{
        #nullable enable
        public UpdateType[]? AllowedUpdates { get; set; }
        #nullable disable
        /// <summary>
        /// Constructs a new <see cref="DefaultUpdateHandler"/> with the specified callback functions
        /// </summary>
        /// <param name="updateHandler">The function to invoke when an update is received</param>
        /// <param name="errorHandler">The function to invoke when an error occurs</param>
        /// <param name="allowedUpdates">Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates</param>
        public DefaultUpdateHandler()
        {
            AllowedUpdates = new UpdateType[] {UpdateType.Unknown };
        }

        /// <inheritdoc />
        public Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            return Task.Run(()=> { 

            });
        }

        /// <inheritdoc />
        public Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.Run(() => { 

            });
        }
    }
}
