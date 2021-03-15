using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot.Interaction
{
#if NET5_0
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member	
	public delegate Task<TRes> InteractionHandleDelegate<TRes,in TContext>(TContext context, CancellationToken cancel) where TRes : IHandleResult where TContext : IInteractionContext;
	public delegate TRes InteractionHandleDelegateSync<TRes, TContext>(TContext context, CancellationToken cancel) where TRes : IHandleResult where TContext : IInteractionContext;
	
	public interface IContext<out T> where T : IInteractionContext
	{
	
	}

	public interface IInteractionHandler<Ctx> : IContext<Ctx> where Ctx: IInteractionContext
	{

		//InteractionHandleDelegate<TRes, TContext> AsyncHandlerDelegate { get; set; }
		//	InteractionHandleDelegateSync<TRes, TContext> HandlerDelegate { get; set; }
		public Task HandleAsync(Ctx context, CancellationToken cancelToken); 

							  //TRes Handle<TRes>(TContext context) where TRes : class, IHandleResult, new();


	}

	public abstract class BaseHandler<Ctx> : IInteractionHandler<Ctx> where Ctx: IInteractionContext
	{
		public BaseHandler()
		{
		}

		public Task HandleAsync(Ctx context, CancellationToken cancelToken) => throw new NotImplementedException();
	}
#endif
}
