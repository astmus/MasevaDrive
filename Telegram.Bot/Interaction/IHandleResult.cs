using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.ExtensionsBase;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Interaction
{
	

	public interface IHandleResult : IDisposable
	{
		IEnumerable<string> Options { get; }
		IEnumerable<string> Controls { get; }
		IEnumerable<object> DisplayContent { get; }
		string TextLabel { get; }
		bool ResetKeyboard { get; }
		bool HideKeyboard { get; }
		bool RemoveKeyboard { get; }
	}
	public class HandleResult : IHandleResult
	{
		public HandleResult() { }
		public IEnumerable<string> Options { get; private set; }
		public IEnumerable<string> Controls { get; private set; }
		public IEnumerable<object> DisplayContent { get; private set; }
		public string TextLabel { get; private set; }
		public bool ResetKeyboard { get; private set; }
		public bool HideKeyboard { get; private set; }
		public bool RemoveKeyboard { get; private set; }

		private bool disposedValue;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					Options = null;
					Controls = null;
					DisplayContent = null;
					TextLabel = null;
				}
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
	/// <summary>
	/// 
	/// </summary>
	/*public class Interactor<T> : IInteractor<T> where T : IInteractionContext
	{
		private bool disposedValue;

		/// <summary>
		/// 
		/// </summary>
		public Interactor(T context)
		{
			Context = context;
		}
		///
		public Interactor(T context, IEnumerable<Type> registeredHandlers) : this(context)
		{			
			var attrType = typeof(InteractionsSupportedAttribute);
			foreach (var t in registeredHandlers)
			{
				var attr = t.GetCustomAttribute<InteractionsSupportedAttribute>();
				if (attr.IsCommandSupport(Context.Message.Text))
				{
					var ins = Activator.CreateInstance(t);
					CurrentHandler = (IInteractionHandler<T>)ins;
					CurrentHandler.Configure(Context);
					if (t.GetCustomAttribute<WithoutHandlerAttribute>() != null)
						return;
					var allmethods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
					var method = allmethods.FirstOrDefault(m => m.GetCustomAttribute<InterationRunMethodAttribute>()?.CommandName == Context.Message.Text);
					RunDelegate handler = (RunDelegate)method.CreateDelegate(typeof(RunDelegate), CurrentHandler);
					CurrentHandler.InteractionHandle = handler;
				}
			}

		}
		///
		public IInteractionResult ReplyData { get; private set; }
		/// <summary>
		/// 
		/// </summary>
		public T Context { get; set; }
		///
		public IInteractionHandler<T> CurrentHandler { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Message Message => Context.Update.Message;
		/// <summary>
		/// 
		/// </summary>
		public long ChatId => Context.Update.Message.Chat.Id;
		/// <summary>
		/// 
		/// </summary>
		public MessageType TypeOfMessage => Context.Update.Message.Type;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		public virtual async Task HandleAsync(CancellationToken cancelToken)
		{
#if NET5_0
			if (CurrentHandler == null)
				await Task.CompletedTask;
			else
			{
				await CurrentHandler?.RunHandler(cancelToken);
				ReplyData = CurrentHandler;
				Context.Session.BackupState(ReplyData);
			}
#else
			await Task.CompletedTask;
#endif
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="error"></param>
		/// <param name="cancelToken"></param>
		/// <returns></returns>
		public virtual async Task HandleErrorAsync(Exception error, CancellationToken cancelToken)
		{
#if NET5_0
			await CurrentHandler.HandleExceptionsError(error, cancelToken);
#else
			await Task.CompletedTask;
#endif
		}

		///
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~BaseInteractionHandler()
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
	}*/
}
