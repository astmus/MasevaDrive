using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Extensions;
using System.IO;
using DriveApi.Storage;
using System.Collections.Generic;
//using Microsoft.AspNet.OData.Builder;
//using Microsoft.AspNet.OData.Extensions;
using Unity;
using System.Web.Http.Dependencies;
using Unity.Lifetime;
using DriveApi.Network;
using System.Web.Http.Dispatcher;
using System.Configuration;
using System.Net;
using System.Web.Http.ExceptionHandling;
using System.Threading;
using System.Net.Http;
using System.Web.Http.Filters;
using System.ComponentModel.DataAnnotations;
using FrameworkData.Settings;
using System.ServiceModel;
using System.Web.Http.SelfHost.Channels;

[assembly: OwinStartup(typeof(DriveApi.Startup))]

namespace DriveApi
{

	public class Startup
	{


		public void Configuration(IAppBuilder app)
		{
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
			/*app.Run(context =>
			{
				if (context.Request.Path.Equals(new PathString("/fail")))
				{
					throw new Exception("Random exception");
				}
				context.Response.ContentType = "text/plain";
				return context.Response.WriteAsync("Hello, world.");
			});*/
			/*Microsoft.Owin.BuilderProperties.AppProperties
				
			app.Use((context, next) =>
			{
				PrintCurrentIntegratedPipelineStage(context, "Middleware 1");
				return next.Invoke();
			});
			app.Use((context, next) =>
			{
				PrintCurrentIntegratedPipelineStage(context, "2nd MW");
				return next.Invoke();
			});
			app.UseStageMarker(PipelineStage.Authenticate);
			app.Run(context =>
			{
				PrintCurrentIntegratedPipelineStage(context, "3rd MW");
				return context.Response.WriteAsync("Hello world");
			});
			app.UseStageMarker(PipelineStage.ResolveCache);*/
			//var container = 

			var config = new HttpConfiguration();
			
			var container = UnityConfig.GetConfiguredContainer();
			config.DependencyResolver = new UnityResolver(container);
			// Use our own IHttpControllerActivator implementation 
			// (to prevent DefaultControllerActivator's behaviour of creating child containers per request)
			config.Services.Replace(typeof(IHttpControllerActivator), new ControllerActivator());
			//config.Services.Replace(typeof(IExceptionHandler), new ExHandler());
			config.MapHttpAttributeRoutes();			
			config.Routes.MapHttpRoute("API Default", "{controller}/{id}", defaults: new { id = SolutionSettings.Default.RootOfMediaFolder.ToHash() });
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"));
			//config.MessageHandlers.Add(new CloseConnectionHandler());
			//config.Initializer = Init;			

			/*ODataModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<StorageItem>("Items");			
			config.MapODataServiceRoute(
			routeName: "ODataRoute",
			routePrefix: null,
			model: builder.GetEdmModel());*/
			config.EnsureInitialized();
						
			app.UseUnityContainerPerRequest(container);

			app.UseCustomMiddleware();			
			app.UseWebApi(config);
		}

		public class CloseConnectionHandler : DelegatingHandler
		{
			protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
			{
				return base.SendAsync(request, cancellationToken).ContinueWith(t =>
				{					
					var response = t.Result;
					//response.Headers.ConnectionClose = true;

					return response;
				});
			}
		}

		/*public class ExHandler : ExceptionHandler
		{
			public override Task HandleAsync(ExceptionHandlerContext context,
											CancellationToken cancellationToken)
			{
				if (!ShouldHandle(context))
				{
					return Task.FromResult(0);
				}

				return HandleAsyncCore(context, cancellationToken);
			}

			public virtual Task HandleAsyncCore(ExceptionHandlerContext context,
											   CancellationToken cancellationToken)
			{
				HandleCore(context);
				return Task.FromResult(0);
			}

			public virtual void HandleCore(ExceptionHandlerContext context)
			{
			}

			public override bool ShouldHandle(ExceptionHandlerContext context)
			{
				return true;
			}
		}*/
	}
}
