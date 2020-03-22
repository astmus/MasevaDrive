using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting.Engine;
using System.Configuration;
using DriveApi.Extensions;
using DriveApi.Storage;
using System.Diagnostics;
using Medallion.Shell;
using System.Threading;
using System.IO;
using Medallion.Shell.Streams;

namespace DriveApi
{
	class Program
	{
		static void Main(string[] args)
		{
			InitializeAppSettings();			
			// Start OWIN host 						
			using (WebApp.Start<Startup>(url: ConfigurationManager.AppSettings.BaseAddress()))
			{
				// Create HttpClient and make a request to api/values 
				HttpClient client = new HttpClient();

				var response = client.GetAsync(ConfigurationManager.AppSettings.BaseAddress()).Result;

				Console.WriteLine(response);
				Console.WriteLine(response.Content.ReadAsStringAsync().Result);


				//string path = @"x:\Programming\MasevaDrive\DriveApi\bin\Debug\ffmpeg.exe";
				string arg = "-i \"d:\\Temp\\1.MOV\" -c:v libvpx -minrate 8M -maxrate 8M -b:v 8M -bufsize 1k -c:a libopus -b:a 96k -f webm pipe:1";
				//ExcuteProcess(@"x:\Programming\MasevaDrive\DriveApi\bin\Debug\ffmpeg.exe", "-i \"z:\\Images&Video\\2013 New Year\\01012013003.mp4\" -vcodec libvpx -qmin 0 -qmax 50 -crf 10 -b:v 1M -acodec libvorbis -f webm output", (s, e) => { }
				//);
				using (FileStream file = System.IO.File.OpenWrite("D:\\Temp\\video09111.webm"))
				{
					var cmd = Command.Run(@"ffmpeg.exe", null, options => options.StartInfo((i) =>
					{
						i.Arguments = arg;
						i.UseShellExecute = false;
						i.CreateNoWindow = true;
						i.RedirectStandardError = true;
						i.RedirectStandardOutput = true;
						i.RedirectStandardInput = false;					
					}));
					var fileWrite = cmd.StandardOutput.PipeToAsync(file);					
					
					var str = cmd.StandardError.ReadToEnd();
					fileWrite.Wait();
				}
				

				   Console.ReadKey();
			}
		}

		/*ExcuteProcess(@"x:\Programming\MasevaDrive\DriveApi\bin\Debug\ffmpeg.exe", "-i \"z:\\Images&Video\\2013 New Year\\01012013003.mp4\" -vcodec libvpx -qmin 0 -qmax 50 -crf 10 -b:v 1M -acodec libvorbis -f webm pipe:1", (s, e) =>
							{
								Console.WriteLine(e.Data);
							});*/
		static ProcessStartInfo newInfo(string exe, string arg)
		{
			ProcessStartInfo i = new ProcessStartInfo();
			i.FileName = exe;
			i.Arguments = arg;
			i.UseShellExecute = false;
			i.CreateNoWindow = true;
			i.RedirectStandardError = true;
			i.RedirectStandardOutput = true;
			return i;
		}

		static void ExcuteProcess(string exe, string arg, DataReceivedEventHandler output)
		{
			using (var p = new Process())
			{
				p.StartInfo.FileName = exe;
				p.StartInfo.Arguments = arg;
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.RedirectStandardError = true;
				p.StartInfo.RedirectStandardOutput = true;				
				//p.OutputDataReceived += output;
				p.ErrorDataReceived += P_ErrorDataReceived;
				p.Start();				
				p.BeginErrorReadLine();
				FileStream file = System.IO.File.OpenWrite("video.webm");
				p.WaitForExit();
				file.Close();
			}
		}

		private static void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			Console.WriteLine(e.Data);
		}

		private static void InitializeAppSettings()
		{
			var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (configFile.AppSettings.Settings["RootPath"] == null)
				configFile.AppSettings.Settings.Add("RootPath", @"z:\Images&Video");
			if (configFile.AppSettings.Settings["BaseAddress"] == null)
				configFile.AppSettings.Settings.Add("BaseAddress", "http://192.168.0.103:9090/");
			if (configFile.AppSettings.Settings["PathToThumbnails"] == null)
				configFile.AppSettings.Settings.Add("PathToThumbnails", @"z:\Temp\Thumbnails");

			configFile.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
		}
	}
}
