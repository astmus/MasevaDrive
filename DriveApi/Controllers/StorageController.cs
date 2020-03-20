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
using System.Diagnostics;
using System.Configuration;
using DriveApi.Extensions;

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
					if (!item.FileSysInfo.Exists)
						return Request.CreateErrorResponse(HttpStatusCode.OK, "File does not exist");
					
					HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, Storage.GetChildrenByParentId(id));
					result.Content = new StreamContent(item.FileSysInfo.OpenRead());
					result.Content.Headers.ContentLength = item.FileSysInfo.Length;

					result.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(item.Name));
					return result;
				}
				else
					return Request.CreateResponse(HttpStatusCode.OK, Storage.GetChildrenByParentId(id));
			}
			catch
			{
				var message = string.Format("Item with id = {0} not found", id);
				return Request.CreateErrorResponse(HttpStatusCode.OK, message);
			}
		}

		[HttpGet]
		[Route("storage/{id}/content")]
		public HttpResponseMessage GetFileContent(string id)
		{
			var item = Storage.GetFileById(id);
			if (item == null)
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, string.Format("File with id = {0} not found", id));
			var image = File.ReadAllBytes(item.Path);
			HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
			result.Content = new ByteArrayContent(image);
			result.Content.Headers.ContentLength = image.Length;			
			result.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(item.Name));			
			result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = item.Name, Size = item.FileSysInfo.Length, CreationDate = item.File.CreationTime, ModificationDate = item.File.CreationTime };
			return result;
		}

		[HttpGet]
		[Route("storage/thumb{id}")]
		public HttpResponseMessage GetThumbnail(string id)
		{
			var item = Storage.GetFileById(id);
			if (item == null)
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, string.Format("File with id = {0} not found", id));

			var pathToThumbnail = Path.Combine(ConfigurationManager.AppSettings.PathToThumbnails(), item.Id)+".jpeg";

			var thumbnailInfo = new FileInfo(pathToThumbnail);
			if (!thumbnailInfo.Exists)
			{
				RunFFmpeg(item.Path, pathToThumbnail);
				thumbnailInfo = new FileInfo(pathToThumbnail);
			}

			if (thumbnailInfo.Exists)
			{				
				HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
				result.Content = new StreamContent(File.OpenRead(pathToThumbnail));
				result.Content.Headers.ContentLength = thumbnailInfo.Length;
				result.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(item.Name));
				return result;
			}
			return Request.CreateResponse(HttpStatusCode.OK, new byte[0]);
		}

		private string RunFFmpeg(string sourceFile, string destFile)
		{
			var processInfo = new ProcessStartInfo();
			processInfo.FileName = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),"ffmpeg.exe");
			processInfo.Arguments = string.Format("-i \"{0}\" -qscale:v 5 -vf scale=\"360:-1\" \"{1}\"", sourceFile, destFile);
			processInfo.CreateNoWindow = true;
			processInfo.UseShellExecute = false;
			//processInfo.RedirectStandardOutput = true;
			using (var process = new Process())
			{
				process.StartInfo = processInfo;				
				process.Start();				
				process.WaitForExit();
				process.ErrorDataReceived += Process_ErrorDataReceived;
				return destFile;
				//return process.StandardOutput.BaseStream;			
			}			
		}

		private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			var v = e.Data;
		}

		private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			var v = e.Data;
		}
	}
}
