using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DriveApi.Network
{
	public class UnityContainerPerRequestMiddleware : OwinMiddleware
	{
		private readonly OwinMiddleware _next;
		private readonly IUnityContainer _container;

		public UnityContainerPerRequestMiddleware(OwinMiddleware next, IUnityContainer container) : base(next)
		{
			_next = next;
			_container = container;
		}

		public override async Task Invoke(IOwinContext context)
		{
			// Create child container (whose parent is global container)
			var childContainer = _container.CreateChildContainer();

			// Set created container to owinContext (to become available at other places using OwinContext.Get<IUnityContainer>(key))
			context.Set(HttpApplicationKey.OwinPerRequestUnityContainerKey, childContainer);

			await _next.Invoke(context);

			// Dispose container that would dispose each of container's registered service
			childContainer.Dispose();
			//_container.Dispose();
		}		
	}

	public static class UnityMiddlewareExtensions
	{
		public static IAppBuilder UseUnityContainerPerRequest(this IAppBuilder app, IUnityContainer container)
		{
			app.Use(typeof(UnityContainerPerRequestMiddleware), container);
			return app;
		}
	}
}
