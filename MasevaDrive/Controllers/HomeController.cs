using MasevaDrive.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace MasevaDrive.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index(string id)
		{
			string response = string.Empty;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9090/storage/"+id);			
			httpWebRequest.Method = "GET";			
			HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			if (httpResponse.StatusCode == HttpStatusCode.OK)
			{

				if (httpResponse.ContentType == "application/json; charset=utf-8")
				{
					using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
					{
						response = streamReader.ReadToEnd();
					}
					httpResponse.Close();
					var jresult = JArray.Parse(response);
					var allItems = jresult.ToObject<List<StorageItem>>();					
					return View(allItems);
				}
				else
				{
					byte[] binaryResponse;
					using (BinaryReader streamReader = new BinaryReader(httpResponse.GetResponseStream()))
					{
						binaryResponse = streamReader.ReadBytes((int)httpResponse.ContentLength);
					}
					httpResponse.Close();
					return View("ImageContent", binaryResponse);
				}
			}
			return View();
		}

		[ActionName("PostAction")]
		[HttpPost]		
		public ActionResult PostAction(List<StorageItem> item)
		{
			var r = Request.Form;
			
			return Index("");
		}

		public ActionResult AboutTest()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}