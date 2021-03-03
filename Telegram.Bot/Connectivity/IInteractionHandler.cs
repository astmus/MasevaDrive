using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public interface IInteractionHandler<T> : IDisposable where T : InteractionContext
	{
		/// <summary>
		/// 
		/// </summary>
		T Context { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		Task HandleAsync(CancellationToken cancelToken);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="error"></param>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		Task HandleErrorAsync(Exception error, CancellationToken cancelToken);
	}
}
