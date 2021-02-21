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
	public class InteractionHandler : IInteractionHandler
	{
		/// <summary>
		/// 
		/// </summary>
		public InteractionContext Context { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		public virtual Task HandleAsync(CancellationToken cancelToken)
		{
			return Task.Run(() => { Console.WriteLine(Context.ToString()); }, cancelToken);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="error"></param>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		public virtual Task HandleErrorAsync(Exception error, CancellationToken cancelToken)
		{
			return Task.Run(() => { Console.WriteLine(Context.ToString()); }, cancelToken);
		}
	}
}
