using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting.Engine;
using System.Configuration;
using DriveApi.Storage;
using System.Diagnostics;
using Medallion.Shell;
using System.Threading;
using System.IO;
using Medallion.Shell.Streams;
using FrameworkData.Settings;

namespace DriveApi
{
	class Program
	{
		static void Main(string[] args)
	{
			// Start OWIN host 						
			var address = SolutionSettings.Default.BaseApiAddress;
			using (WebApp.Start<Startup>(url: address))
			{
				// Create HttpClient and make a request to api/values 
				HttpClient client = new HttpClient();

				var response = client.GetAsync(address).Result;

				Console.WriteLine(response);
				Console.WriteLine(response.Content.ReadAsStringAsync().Result);


				//string arg = string.Format("-i \"{0}\" -vcodec libvpx -b:v 1600K -bufsize 5M -crf 4 -ac 2 -c:a libopus -b:a 96k \"{1}\"", @"D:\Temp\3.mp4", @"D:\Temp\0.webm");
				/*string arg = string.Format("-i \"{0}\" -vcodec libvpx -vsync 2 -b:v 1600K -bufsize 5M -auto-alt-ref 1 -an \"{1}\"", @"D:\Temp\3.mp4", @"D:\Temp\_3.webm");

				var cmd = Command.Run(@"ffmpeg.exe", null, options => options.StartInfo((i) =>
				{
					i.Arguments = arg;
				}));
				
				//var fileWrite = cmd.StandardOutput.PipeToAsync(file);					

				var str = cmd.RedirectStandardErrorTo(Console.Out);
					*/

				Console.ReadKey();
			}
		}

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
	}
}
