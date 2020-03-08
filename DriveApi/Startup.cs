using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Extensions;
using System.IO;
using DriveApi.Storage;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Unity;
using System.Web.Http.Dependencies;
using Unity.Lifetime;
using DriveApi.Network;
using System.Web.Http.Dispatcher;
using System.Configuration;
using DriveApi.Extensions;

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
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute("API Default", "{controller}/{id}", defaults: new { id = ConfigurationManager.AppSettings.RootPath().ToHash() });
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;

			//config.Initializer = Init;
			
			

			ODataModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<StorageItem>("Items");			
			config.MapODataServiceRoute(
			routeName: "ODataRoute",
			routePrefix: null,
			model: builder.GetEdmModel());
			config.EnsureInitialized();

			app.UseUnityContainerPerRequest(container);
			app.UseCustomMiddleware();
			
			
			app.UseWebApi(config);
		}
	}
	
	/*public class IpMiddleware : OwinMiddleware
	{
		private readonly HashSet<string> _deniedIps;

		public IpMiddleware(OwinMiddleware next, HashSet<string> deniedIps) :
			base(next)
		{
			_deniedIps = deniedIps;
		}

		public override async Task Invoke(IOwinContext context)
		{
			var ipAddress = (string)context.Request.Environment["server.RemoteIpAddress"];

			if (_deniedIps.Contains(ipAddress))
			{
				context.Response.StatusCode = 403;
				return;
			}

			await Next.Invoke(context);
		}
	}*/
}
