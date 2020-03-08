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
		IStorageItemsProvide Provider;
		public StorageController(IStorageItemsProvide provider)
		{
			Provider = provider;
		}
		[HttpGet]
		public IEnumerable<StorageItem> GetRoot()
		{
			return Provider.GetRoot();
		}

		[HttpGet]
		public IEnumerable<StorageItem> GetChildrebByParentId(string id)
		{
			return Provider.GetChildrenByParentId(id);
		}

		[HttpGet]		
		[Route("storage/{id}/content")]
		public HttpResponseMessage GetFileContent(string id)
		{
			var item = Provider.GetById(id);
			if (item.FileSysInfo != null)
			{
				var image = File.ReadAllBytes(item.Path);				
				HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
				result.Content = new ByteArrayContent(image);
				result.Content.Headers.ContentLength = image.Length;
				result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");				
				return result;
			}			
			else
				return new HttpResponseMessage(HttpStatusCode.NoContent);
		}

		//[HttpGet]
		/*[Route("items")]
		public IEnumerable<int> GetItems()
		{
			return Enumerable.Range(0, 15);
		}
		*/
		//[HttpGet]
		/*[Route("string/{value}/{val2}")]
		public string GetStringValue(string value, string val2)
		{
			return "new string" + value+" "+val2;
		}*/
	}
}
