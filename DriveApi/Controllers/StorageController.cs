using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using DriveApi.Storage;
using System.Net.Http.Headers;
using System.IO;

namespace DriveApi.Controllers
{
	public class StorageController : ApiController
	{
		IStorageItemsProvide Storage;
		public StorageController(IStorageItemsProvide provider)
		{
			Storage = provider;
		}
		[HttpGet]
		public IEnumerable<StorageItem> GetRoot()
		{
			return Storage.GetRoot();
		}

		[HttpGet]
		[Route("storage/{id}")]
		public HttpResponseMessage GetById(string id)
		{
			try
			{
				var item = Storage.GetById(id);
				if (item.FileSysInfo != null)
				{
					var image = File.ReadAllBytes(item.Path);
					HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, Storage.GetChildrenByParentId(id));
					result.Content = new ByteArrayContent(image);
					result.Content.Headers.ContentLength = image.Length;
					if (Path.GetExtension(item.Name) == ".jpeg" || Path.GetExtension(item.Name) == ".jpg")
						result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
					else
						result.Content.Headers.ContentType = new MediaTypeHeaderValue("video/" + Path.GetExtension(item.Name).Substring(1));
					return result;
				}
				else
					return Request.CreateResponse(HttpStatusCode.OK, Storage.GetChildrenByParentId(id));
			}
			catch
			{
				var message = string.Format("Product with id = {0} not found", id);
				return Request.CreateErrorResponse(HttpStatusCode.OK, message);
			}			
		}

		[HttpGet]
		[Route("storage/{id}/content")]
		public HttpResponseMessage GetFileContent(string id)
		{
			try
			{
				var item = Storage.GetById(id);
				if (item.FileSysInfo != null)
				{
					var image = File.ReadAllBytes(item.Path);
					HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
					result.Content = new ByteArrayContent(image);
					result.Content.Headers.ContentLength = image.Length;
					if (Path.GetExtension(item.Name) == "jpeg")
						result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
					else
						result.Content.Headers.ContentType = new MediaTypeHeaderValue("video/" + Path.GetExtension(item.Name).Substring(1));
					result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = item.Name, Size = item.FileSysInfo.Length, CreationDate = item.File.CreationTime, ModificationDate = item.File.CreationTime };
					return result;
				}
				else
					return Request.CreateErrorResponse(HttpStatusCode.NoContent, "There is no content for download");
			}
			catch
			{
				var message = string.Format("Product with id = {0} not found", id);
				return Request.CreateErrorResponse(HttpStatusCode.OK, message);
			}
		}		
	}
}
