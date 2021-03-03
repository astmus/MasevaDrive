using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Telegram.Bot.Connectivity
{
	/// <summary>
	/// 
	/// </summary>
	public class InteractionContext : IDisposable
	{
		private bool disposedValue;

		///
		public Update Interaction { get; set; }
		///
		public User User { get; set; }
		///
		public Session Session { get; set; }
		///
		public IBot Connection { get; set; }
		///
		public bool IsAuthorizedUser => User as RegisteredUser != null;
		///
		public InteractionContext()
		{

		}
		///
		public override string ToString()
		{
			return string.Format("{0} {1} {2} {3}", Interaction.ToString(), User.ToString(), Session?.ToString(), Connection?.ToString());
		}
		///
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
					Interaction = null;
					User = null;
					Session = null;
					Connection = null;
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~InteractionContext()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }
		///
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
