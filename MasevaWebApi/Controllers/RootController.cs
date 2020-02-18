using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MasevaWebApi.Models;

namespace MasevaWebApi.Controllers
{
	public class RootController : ApiController
    {
		//[HttpGet]
		[Route("")]
		public IEnumerable<int> GetValues()
		{
			return Enumerable.Range(0, 10);
		}

		//[HttpGet]
		/*[Route("items")]
		public IEnumerable<int> GetItems()
		{
			return Enumerable.Range(0, 15);
		}
		*/
		//[HttpGet]
		[Route("string/{value}/{val2}")]
		public string GetStringValue(string value, string val2)
		{
			return "new string" + value+" "+val2;
		}

		[Route("book")]
		public Book GetBook()
		{
			return new Book() { Author = "Author", Genre = "Genre", Title = "Title" };
		}
	}
}
