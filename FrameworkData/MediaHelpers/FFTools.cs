using FrameworkData.Settings;
using Medallion.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkData
{
	public static class FFTools
	{
		public static MediaFileInfo GetMetadata(string pathToFile)
		{
			MediaFileInfo result;
			using (var cmd = Command.Run(SolutionSettings.Default.FFprobePath, null, options => options.StartInfo(i => i.Arguments = string.Format("-i \"{0}\" -v error -print_format json -show_format -show_streams", pathToFile))))
			{
				cmd.Wait();
				result = MediaFileInfo.FromJson(cmd.StandardOutput.ReadToEnd());
			}
			return result;
		}

		public static MediaFileInfo GetMetadata(this FileInfo file)
		{
			return GetMetadata(file.FullName);
		}

		public static MemoryStream CreateThumbnail(string filePath)
		{
			string args = string.Format("-hide_banner -i \"{0}\" -qscale:v 5 -vf scale=\"360:-1\" -vframes 1 -f image2pipe pipe:1", filePath);
			var createThumbnailProcess = Command.Run(SolutionSettings.Default.FFmpegPath, null, options => options.StartInfo((i) =>
			{
				i.Arguments = args;
				i.RedirectStandardOutput = true;
				i.UseShellExecute = false;
			}));

			var result = new MemoryStream();
			createThumbnailProcess.RedirectTo(result);
			createThumbnailProcess.Wait();

			result.Seek(0, SeekOrigin.Begin);
			return result;
		}

		public static MemoryStream CreateThumbnail(string filePath, int width, int height = -1)
		{
			string args = string.Format("-hide_banner -i \"{0}\" -qscale:v 5 -vf scale=\"360:-1\" -vframes 1 -f -pix_fmt yuvj422p image2pipe pipe:1", filePath);
			var createThumbnailProcess = Command.Run(SolutionSettings.Default.FFmpegPath, null, options => options.StartInfo((i) =>
			{
				i.Arguments = args;
				i.RedirectStandardOutput = true;
				i.UseShellExecute = false;
			}));

			var result = new MemoryStream();
			createThumbnailProcess.RedirectTo(result);
			createThumbnailProcess.Wait();

			result.Seek(0, SeekOrigin.Begin);
			return result;
		}

		public static Command CreateInlineThumbnail(string filePath, long size,ref MemoryMappedViewStream stream)
		{
			string args = string.Format(@"-hide_banner -i {0} -vf ""scale = 'min({1},iw)':min'({1},ih)':force_original_aspect_ratio = decrease,pad = {1}:{1}:-1:-1:color = gray"" -pix_fmt yuvj422p -vframes 1 -f image2pipe pipe:1", filePath, size);
			var createThumbnailProcess = Command.Run(SolutionSettings.Default.FFmpegPath, null, options => options.StartInfo((i) =>
			{
				i.Arguments = args;
				i.RedirectStandardOutput = true;
				i.UseShellExecute = false;
			}));

			createThumbnailProcess.RedirectTo(stream);
			return createThumbnailProcess;
		}

		public static MemoryStream CreateThumbnail(string filePath, out bool hasInvalidInputData)
		{
			MemoryStream result = null;
			hasInvalidInputData = false;
			try
			{
				string args = string.Format("-hide_banner -i \"{0}\" -qscale:v 16 -an -vf scale=\"320:240\" -vframes 1 -pix_fmt yuvj422p -f image2pipe pipe:1", filePath);
				using (var createThumbnailProcess = Command.Run(SolutionSettings.Default.FFmpegPath, null, options => options.StartInfo((i) =>
				{
					i.Arguments = args;
					i.RedirectStandardOutput = true;
					i.UseShellExecute = false;
				})))
				{
					result = new MemoryStream();
					createThumbnailProcess.RedirectTo(result);
					createThumbnailProcess.Wait();
					if (createThumbnailProcess.Task.Result.Success == false &&
						(createThumbnailProcess.Task.Result.StandardError.IndexOf("Invalid data found when processing input") != -1
						|| createThumbnailProcess.Task.Result.StandardError.IndexOf("does not contain any stream") != -1))
						hasInvalidInputData = true;
					else
						hasInvalidInputData = false;
				}

				result.Seek(0, SeekOrigin.Begin);

			}
			catch
			{
				hasInvalidInputData = true;
			}
			return result;
		}

	}
}
