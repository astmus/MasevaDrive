using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.ExtensionsBase
{
	/// <summary>
	/// Processes <see cref="Update"/>s and errors.
	/// </summary>
	public interface IUpdateHandler
	{
		/// <summary>
		/// Handles an <see cref="Update"/>
		/// </summary>
		/// <param name="botClient">The <see cref="ITelegramBot"/> instance of the bot receiving the <see cref="Update"/></param>
		/// <param name="update">The <see cref="Update"/> to handle</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/> which will notify that method execution should be cancelled</param>
		/// <returns></returns>
		Task HandleUpdate(ITelegramBot botClient, Update update, CancellationToken cancellationToken);

		/// <summary>
		/// Handles an <see cref="Exception"/>
		/// </summary>
		/// <param name="botClient">The <see cref="ITelegramBot"/> instance of the bot receiving the <see cref="Exception"/></param>
		/// <param name="exception">The <see cref="Exception"/> to handle</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/> which will notify that method execution should be cancelled</param>
		/// <returns></returns>
		Task HandleError(ITelegramBot botClient, Exception exception, CancellationToken cancellationToken);

		/// <summary>
		/// Indicates which <see cref="UpdateType"/>s are allowed to be received. null means all updates
		/// </summary>
#nullable enable
		UpdateType[]? AllowedUpdates { get; }
#nullable disable
	}
}
