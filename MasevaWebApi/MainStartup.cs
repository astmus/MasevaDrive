using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.IO;
using MasevaWebApi.Models;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(MasevaWebApi.MainStartup))]

namespace MasevaWebApi
{
	public class MainStartup
	{
		Dictionary<string, StorageItem> hashs;
		public MainStartup()
		{
			DirectoryInfo d = new DirectoryInfo(@"z:\Images&Video\");
			hashs = new Dictionary<string, StorageItem>();
			var folders = d.GetDirectories("*", SearchOption.AllDirectories);
			foreach (var f in folders)
			{
				StorageItem item = new StorageItem(f);
				hashs.Add(item.Hash, item);
			};

			var files = d.GetFiles("*", SearchOption.AllDirectories);
			foreach (var f in files)
			{
				StorageItem item = new StorageItem(f);
				hashs.Add(item.Hash, item);
			};
		}

		public void Configuration(IAppBuilder app)
		{			
			app.UseErrorPage();
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
			var config = new HttpConfiguration();
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute("API Default", "{controller}/{action}");
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;

			app.UseWebApi(config);

		}
	}
}
