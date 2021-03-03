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
	public class InteractionHandler : IInteractionHandler<InteractionContext>
	{
		private bool disposedValue;

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

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					Context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~InteractionHandler()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
