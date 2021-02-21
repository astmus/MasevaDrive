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
using System.Threading;
using System.Threading.Tasks;
using DriveApi.Network;
using Medallion.Shell;
using System.Reflection;
using FrameworkData.Settings;
using FrameworkData;

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
		[Route("storage/{id}/content")]
		public HttpResponseMessage GetFileContent(string id)
		{
			var item = Storage.GetFileById(id);
			if (item == null)
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, string.Format("File with id = {0} not found", id));
			HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
			result.Content = new StreamContent(item.GetStream());
			result.Content.Headers.ContentLength = item.Size;
			result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(item.Name));
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = item.Name, Size = item.Size, CreationDate = item.DateTimeCreation };
			return result;
		}

		[HttpGet]
		[Route("storage/thumb{id}={size}")]
		public HttpResponseMessage GetThumbnail(string id, int size)
		{
			var item = Storage.GetFileById(id);
			if (item == null)
				return Request.CreateErrorResponse(HttpStatusCode.NoContent, string.Format("File with id = {0} not found", id));

			var pathToThumbnail = Path.Combine(SolutionSettings.Default.ThumbnailsFolder, item.Hash) + ".jpeg";

			var thumbnailInfo = new FileInfo(pathToThumbnail);
			Stream stream = null;			
			if (!thumbnailInfo.Exists)
			{				
				var fileStream = File.Create(pathToThumbnail);				
				stream.CopyTo(fileStream);
				fileStream.Close();
				stream.Seek(0, SeekOrigin.Begin);
				thumbnailInfo = new FileInfo(pathToThumbnail);
			}
			else
				stream = FrameworkData.FFTools.CreateInlineThumbnail(item.FullPath, size);


			if (stream?.Length > 0)
			{
				HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
				result.Content = new StreamContent(stream);
				result.Content.Headers.ContentLength = stream.Length;
				result.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(item.Name));
				return result;
			}
			else
				return Request.CreateResponse(HttpStatusCode.OK, new byte[0]);
		}

		[HttpGet]
		public IEnumerable<StorageItemInfo> GetRoot()
		{
			return Storage.GetRoot();
		}

		[HttpGet]
		[Route("storage/{id}")]
		public HttpResponseMessage GetById(string id)
		{
			try
			{
				StorageItemInfo item = Storage.GetById(id);
				if (item == null || !File.Exists(item.FullPath))
						return Request.CreateErrorResponse(HttpStatusCode.OK, "File does not exist");
					if (item.IsFile)
					{
						HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, Storage.GetChildrenByParentId(id));
						result.Content = new StreamContent(item.GetStream());
						result.Content.Headers.ContentLength = item.Size;
						result.Content.Headers.ContentType = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(item.Name));
						return result;
					}
					//else
					//{
					//	/*string tag = GetETag(Request);
					//	if (tag == "\"" + item.FileSysInfo.Name.ToHash() + "\"")
					//	{							
					//		var res = Request.CreateResponse(HttpStatusCode.NotModified);
					//		res.Headers.Add("SuppressContent","1");
					//		Console.WriteLine(Environment.NewLine + tag +Environment.NewLine);
					//		return res;
					//	}*/
					//	var info = item.FileSysInfo.GetMediaInfo();
					//	double targetVideoBitRate = info.Streams[0].BitRate * .5D;
					//	double targetAudioBitRate = info.Streams[1].BitRate * .5D;
					//	targetAudioBitRate = 96000;//Math.Max(32000, Math.Min(targetAudioBitRate, 192000));
					//	targetVideoBitRate = 3000000;//Math.Max(32000, Math.Min(targetVideoBitRate, 512000));
					//	double totalDurationSeconds = info.Format.Duration;
					//	long originalSize = info.Format.Size;
					//	//var leftBits = (info.Format.BitRate * totalDurationSeconds) - ((info.Streams[0].BitRate + info.Streams[1].BitRate) * totalDurationSeconds);

					//	long expectedSize = Convert.ToInt64(((targetVideoBitRate + targetAudioBitRate) * totalDurationSeconds) / 8D *0.99);						
					//	double targetBitRate = (targetVideoBitRate + targetAudioBitRate) / 8D;
					//	Console.WriteLine(Environment.NewLine + expectedSize + "bytes " + expectedSize / 1024.0 + " KB" + Environment.NewLine);

					//	RangeHeaderValue rangeHeader = Request.Headers.Range;
					//	HttpResponseMessage response = new HttpResponseMessage();

					//	response.Headers.AcceptRanges.Add("bytes");

					//	response.Headers.ETag = new EntityTagHeaderValue("\"" + item.FileSysInfo.Name.ToHash() + "\"");
					//	Console.WriteLine(Environment.NewLine + Request.Headers.Range + Environment.NewLine);
					//	long end;
					//	long start;						
					//	string arg = string.Format("-hide_banner -i \"{0}\" -vf scale=1280:-1 ! -metadata duration=\"{1}\" -b:a 96000 -vcodec libvpx -b:v 3000k -quality realtime -g 240 -threads 8 -speed 5 -qmin 4 -qmax 48 -ac 2 -c:a libopus -pass 2 -passlogfile \"D:\\Temp\\MaxOlgaClip-1280x720\" -f webm pipe:1", item.FileSysInfo.FullName, item.File.MediaInfo.Format.Duration);
					//	if (rangeHeader.Ranges.First().From != null/* && rangeHeader.Ranges.First().From > 0*/)
					//	{

					//	}
					//	// 1. If the unit is not 'bytes'.
					//	// 2. If there are multiple ranges in header value.
					//	// 3. If start or end position is greater than file length.
					//	if (rangeHeader.Unit != "bytes" || rangeHeader.Ranges.Count > 1 ||
					//		!TryReadRangeItem(rangeHeader.Ranges.First(), expectedSize/*originalSize*/, out start, out end))
					//	{
					//		response.StatusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
					//		response.Content = new StreamContent(Stream.Null);  // No content for this status.
					//		response.Content.Headers.ContentRange = new ContentRangeHeaderValue(expectedSize);
					//		response.Content.Headers.ContentType = new MediaTypeHeaderValue("video/webm");

					//		return response;
					//	}

					//	var contentRange = new ContentRangeHeaderValue(start, end, expectedSize);
						
					//	//var contentRange = new ContentRangeHeaderValue(start, end, originalSize);

					//	// We are now ready to produce partial content.						
					//	response.StatusCode = HttpStatusCode.PartialContent;
					//	response.Content = new PushStreamContent(async (outputStream, httpContent, transportContext) =>
					//		{
					//			await OnStreamConnected(outputStream, httpContent as PushStreamContent, transportContext, item, start, end, arg, targetBitRate);
					//		}, "video/webm");
						
						
					//	//response.Content.Headers.Expires = DateTime.UtcNow.Date.AddHours(1);
					//	response.Content.Headers.ContentLength = end - start + 1;
					//	response.Content.Headers.ContentRange = contentRange;
					//	Console.WriteLine("Range - "+contentRange.ToString());					
											
					//	return response;
					//}
				else
					return Request.CreateResponse(HttpStatusCode.NotFound);
			}
			catch (Exception e)
			{
				var message = string.Format("Item with id = {0} not found", id);
				return Request.CreateResponse(HttpStatusCode.NoContent, message);
			}
		}

		private static bool TryReadRangeItem(RangeItemHeaderValue range, long contentLength, out long start, out long end)
		{
			if (range.From != null)
			{
				start = range.From.Value;
				if (range.To != null)
					end = range.To.Value;
				else
					end = (contentLength - start) - 1;
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
		private const int ReadStreamBufferSize = 65536;

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
