using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.ExtensionsBase
{
	/// <summary>
	/// A very simple <see cref="IUpdateHandler"/> implementation
	/// </summary>
	public class CommonHandler : IUpdateHandler
	{
		/// <summary>
		/// Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates
		/// </summary>
#nullable enable
		public UpdateType[]? AllowedUpdates { get; set; }
#nullable disable

		private readonly Func<ITelegramBot, Update, CancellationToken, Task> _updateHandler;

		private readonly Func<ITelegramBot, Exception, CancellationToken, Task> _errorHandler;

		/// <summary>
		/// Constructs a new <see cref="IUpdateHandler"/> with the specified callback functions
		/// </summary>
		/// <param name="updateHandler">The function to invoke when an update is received</param>
		/// <param name="errorHandler">The function to invoke when an error occurs</param>
		/// <param name="allowedUpdates">Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates</param>
		public CommonHandler(
			Func<ITelegramBot, Update, CancellationToken, Task> updateHandler,
			Func<ITelegramBot, Exception, CancellationToken, Task> errorHandler,
#nullable enable
			UpdateType[]? allowedUpdates = default)
#nullable disable
		{
			_updateHandler = updateHandler ?? throw new ArgumentNullException(nameof(updateHandler));
			_errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
			AllowedUpdates = allowedUpdates;
		}

		/// <inheritdoc />
		public Task HandleUpdate(ITelegramBot botClient, Update update, CancellationToken cancellationToken)
		{
			return _updateHandler(botClient, update, cancellationToken);
		}

		/// <inheritdoc />
		public Task HandleError(ITelegramBot botClient, Exception exception, CancellationToken cancellationToken)
		{
			return _errorHandler(botClient, exception, cancellationToken);
		}
	}
}
