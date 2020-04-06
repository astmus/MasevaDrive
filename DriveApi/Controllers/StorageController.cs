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
using System.Threading;
using System.Threading.Tasks;
using DriveApi.Network;
using Medallion.Shell;
using DriveApi.Model;

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
					if (item.File.IsPicture)
					{
						HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, Storage.GetChildrenByParentId(id));
						result.Content = new StreamContent(item.FileSysInfo.OpenRead());
						result.Content.Headers.ContentLength = item.FileSysInfo.Length;
						result.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(item.Name));
						return result;
					}
					else
					{
						/*if (Request.Headers.Range != null)
						{
							Console.WriteLine(Request.Headers.Range.ToString());
							HttpResponseMessage response;
							response = new HttpResponseMessage();
							response.Headers.AcceptRanges.Add("bytes");
							response.StatusCode = HttpStatusCode.PartialContent;
							//var c = new ByteRangeStreamContent(item.FileSysInfo.OpenRead(), Request.Headers.Range, MimeMapping.GetMimeMapping(item.Name));
							var c = new ByteRangeStreamContent(item.File.encodedStream.Value, Request.Headers.Range, "video/webm");
							response.Content = c;
							return response;
						}*/
						//HttpResponseMessage response = new HttpResponseMessage();//Request.CreateResponse(HttpStatusCode.OK);
						//														 //response.Headers.AcceptRanges.Add("bytes");
						//														 //response.StatusCode = HttpStatusCode.PartialContent;						
						//														 //var c = new ByteRangeStreamContent(item.FileSysInfo.OpenRead(), Request.Headers.Range, MimeMapping.GetMimeMapping(item.Name));
						//response.Content = new PushStreamContent(async (outputStream, httpContent, transportContext) =>
						//{
						//	//httpContent.Headers.ContentLength = 2969600;
						//	//httpContent.Headers.ContentRange = new ContentRangeHeaderValue(0, 1000000, 2969600);
						//	Command encodeProcess = null;
						//	try
						//	{								
						//		byte[] buffer = new byte[65536];
						//		string arg = string.Format("-hide_banner -i \"{0}\" -vcodec libvpx -quality realtime -b:v {2} -bufsize 1k -metadata duration=\"{1}\" -ac 2 -c:a libopus -b:a {3} -f webm pipe:1", item.FileSysInfo.FullName, (int)item.File.MediaInfo.Format.Duration, 1000000, 64000);
						//		Console.WriteLine(arg);
						//		encodeProcess = Command.Run(@"ffmpeg.exe", null, options => options.StartInfo((i) =>
						//		{
						//			i.Arguments = arg;
						//		}));
						//		encodeProcess.RedirectStandardErrorTo(Console.Out);
						//		int readed = 0;
						//		while ((readed = await encodeProcess.StandardOutput.BaseStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
						//		{
						//			await outputStream.WriteAsync(buffer, 0, readed);
						//		}
						//	}
						//	catch (HttpException ex)
						//	{
						//		if (ex.ErrorCode == -2147023667) // The remote host closed the connection. 
						//		{
						//			return;
						//		}
						//		encodeProcess.Kill();
						//	}
						//	finally
						//	{
						//		// Close output stream as we are done
						//		outputStream.Close();
						//	}
						//}, "video/webm");
						//return response;
						string tag = GetETag(Request);
						if (tag == "\"" + item.FileSysInfo.Name.ToHash() + "\"")
						{							
							var res = Request.CreateResponse(HttpStatusCode.NotModified);
							res.Headers.Add("SuppressContent","1");
							return res;
						}

						long totalLength = item.FileSysInfo.Length;

						RangeHeaderValue rangeHeader = base.Request.Headers.Range;
						HttpResponseMessage response = new HttpResponseMessage();

						response.Headers.AcceptRanges.Add("bytes");
						response.Headers.ETag = new EntityTagHeaderValue("\"" + item.FileSysInfo.Name.ToHash() + "\"");
						Console.WriteLine(Request.Headers.Range);

						long start = 0, end = 0;

						// 1. If the unit is not 'bytes'.
						// 2. If there are multiple ranges in header value.
						// 3. If start or end position is greater than file length.
						if (rangeHeader.Unit != "bytes" || rangeHeader.Ranges.Count > 1 ||
							!TryReadRangeItem(rangeHeader.Ranges.First(), totalLength, out start, out end))
						{
							response.StatusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
							response.Content = new StreamContent(Stream.Null);  // No content for this status.
							response.Content.Headers.ContentRange = new ContentRangeHeaderValue(totalLength);
							response.Content.Headers.ContentType = new MediaTypeHeaderValue("video/webm");

							return response;
						}

						var contentRange = new ContentRangeHeaderValue(start, end, totalLength);

						// We are now ready to produce partial content.
						response.StatusCode = HttpStatusCode.PartialContent;
						response.Content = new PushStreamContent(async (outputStream, httpContent, transpContext)
						=>
						{							
							try
							{
								Console.WriteLine("File open");
								using (var inputStream = Stream.Synchronized(item.FileSysInfo.OpenRead()))
								{
									int count = 0;
									long remainingBytes = end - start + 1;
									long position = start;
									byte[] buffer = new byte[ReadStreamBufferSize];

									inputStream.Position = start;
									do
									{

										if (remainingBytes > ReadStreamBufferSize)
											count = await inputStream.ReadAsync(buffer, 0, ReadStreamBufferSize);
										else
											count = await inputStream.ReadAsync(buffer, 0, (int)remainingBytes);
										await outputStream.WriteAsync(buffer, 0, count);

										position = inputStream.Position;
										remainingBytes = end - position + 1;
									} while (position <= end);
								}
							}
							catch (System.Exception ex)
							{
								Console.WriteLine("Output stream exception"+ex.Message);
								outputStream.Close();
								outputStream.Dispose();
							}
							finally
							{
								Console.WriteLine("Closed output stream");
								outputStream.Close();								
							}

						}, "video/webm");

						response.Content.Headers.ContentLength = end - start + 1;
						response.Content.Headers.ContentRange = contentRange;

						return response;
					}
				}
				else
					return Request.CreateResponse(HttpStatusCode.OK, Storage.GetChildrenByParentId(id));
			}
			catch (Exception e)
			{
				var message = string.Format("Item with id = {0} not found", id);
				return Request.CreateResponse(HttpStatusCode.NoContent, message);
			}
		}

		private static string GetETag(HttpRequestMessage request)
		{
			IEnumerable<string> values = null;
			if (request.Headers.TryGetValues("If-None-Match", out values))
				return new EntityTagHeaderValue(values.FirstOrDefault()).Tag;
			/*else
				if (request.Headers.TryGetValues("If-Range", out values))
				return new EntityTagHeaderValue(values.FirstOrDefault()).Tag;*/

			return null;
		}

		private static bool TryReadRangeItem(RangeItemHeaderValue range, long contentLength,
			out long start, out long end)
		{
			if (range.From != null)
			{
				start = range.From.Value;
				if (range.To != null)
					end = range.To.Value;
				else
					end = start + contentLength - 1;
			}
			else
			{
				end = contentLength - 1;
				if (range.To != null)
					start = contentLength - range.To.Value;
				else
					start = 0;
			}
			return (start < contentLength && end < contentLength);
		}
		// This will be used in copying input stream to output stream.
		public const int ReadStreamBufferSize = 65536;

		[HttpGet]
		[Route("storage/{id}/content")]
		public HttpResponseMessage GetFileContent(string id)
		{
			var item = Storage.GetFileById(id);
			if (item == null)
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, string.Format("File with id = {0} not found", id));
			HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
			result.Content = new StreamContent(item.FileSysInfo.OpenRead());
			result.Content.Headers.ContentLength = item.FileSysInfo.Length;
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

			var pathToThumbnail = Path.Combine(ConfigurationManager.AppSettings.PathToThumbnails(), item.Id) + ".jpeg";

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

		/*
		 //-i input.mov -vcodec libvpx -qmin 0 -qmax 50 -crf 10 -b:v 1M -acodec libvorbis output.webm
				//- i test.wav - f avi pipe:1 | cat > test.avi
				string path = @"x:\Programming\MasevaDrive\DriveApi\bin\Debug\ffmpeg.exe";
				string arg = "-re -i \"z:\\Images&Video\\2013 New Year\\01012013003.mp4\" -vcodec libvpx -b:v 640k -acodec libvorbis -crf 4 -s 1920x1080 -f webm pipe:1";
				//ExcuteProcess(@"x:\Programming\MasevaDrive\DriveApi\bin\Debug\ffmpeg.exe", "-i \"z:\\Images&Video\\2013 New Year\\01012013003.mp4\" -vcodec libvpx -qmin 0 -qmax 50 -crf 10 -b:v 1M -acodec libvorbis -f webm output", (s, e) => { }
				//);
				using (FileStream file = System.IO.File.OpenWrite("D:\\Temp\\video13.webm"))
				{
					var cmd = Command.Run(@"ffmpeg.exe", null, options => options.StartInfo((i)=> {
						i.Arguments = arg;
						i.UseShellExecute = false;
						i.CreateNoWindow = true;
						i.RedirectStandardError = true;
						i.RedirectStandardOutput = true;
						i.RedirectStandardInput = false;
					}));										
					var fileWrite = cmd.StandardOutput.PipeToAsync(file);
					cmd.Task.
					fileWrite.Wait();
					var str = cmd.StandardError.ReadToEnd();
				}
			 */

		/*
		 -i input.mov -vcodec libvpx -qmin 0 -qmax 50 -crf 10 -b:v 1M -acodec libvorbis output.webm
				/*ExcuteProcess(@"x:\Programming\MasevaDrive\DriveApi\bin\Debug\ffmpeg.exe", "-i \"z:\\Images&Video\\2013 New Year\\01012013003.mp4\" -vcodec libvpx -qmin 0 -qmax 50 -crf 10 -b:v 1M -acodec libvorbis output.webm", (s, e) => {
					Console.WriteLine(e.Data);
				});
			 */
		/*static void ExcuteProcess(string exe, string arg, DataReceivedEventHandler output)
		{
			using (var p = new Process())
			{
				p.StartInfo.FileName = exe;
				p.StartInfo.Arguments = arg;
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.RedirectStandardError = true;
				p.StartInfo.RedirectStandardOutput = true;
				p.OutputDataReceived += output;
				p.ErrorDataReceived += output;
				p.Start();
				p.BeginOutputReadLine();
				p.BeginErrorReadLine();
				p.WaitForExit();
			}
		}*/

		private string RunFFmpeg(string sourceFile, string destFile)
		{
			var processInfo = new ProcessStartInfo();
			processInfo.FileName = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "ffmpeg.exe");
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
