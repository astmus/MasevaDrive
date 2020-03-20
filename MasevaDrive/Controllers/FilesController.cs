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
	public class FilesController : Controller
	{
		private static string baseStorageURL = "http://192.168.0.103:9090/storage/";
		[ActionName("View")]
		[HttpGet]
		public ActionResult ContentOfStorageItem(string id)
		{
			if (Request.Params["name"] != null)
				ViewBag.ItemName = Request.Params["name"];

			string response = string.Empty;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(baseStorageURL + id);
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
			}
			return View("Error", "Wrong content type");
		}

		[HttpGet]
		public ActionResult ViewImage(string id)
		{
			if (Request.Params["name"] != null)
				ViewBag.ItemName = Request.Params["name"];
			return View("ImageViewContent", model: baseStorageURL + id);
		}

		[HttpGet]
		public ActionResult ViewVideo(string id)
		{
			if (Request.Params["name"] != null)
				ViewBag.ItemName = Request.Params["name"];
			return View("VideoViewContent", model: baseStorageURL + id);
		}

		[ActionName("PostAction")]
		[HttpPost]
		public ActionResult PostAction(List<StorageItem> item)
		{
			var r = Request.Form;

			return ContentOfStorageItem("");
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