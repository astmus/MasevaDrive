using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.IO;
using System.Net;

namespace CloudSync
{
    public class DownloadFileWorker : CloudWorker
    {
        private string link;
        private string destination;
        public DownloadFileWorker(string link, string destination) : base()
        {
            this.link = link;
            this.destination = destination;
        }
        public async override Task DoWork()
        {
            //return Task.Factory.StartNew(() =>            {
            
            WebClient client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted; 
            try
            {
                await client.DownloadFileTaskAsync(link + "34", destination);
            }
            catch (System.Exception ex)
            {
               
            }
            
            /*FileStream fileStream = File.Create(destination);
            fileStream.Write(res, 0, res.Length);
            fileStream.Close();
            res.Clone();*/
            /*using (var client = new HttpClient())
            {
                using (var stream = await client.GetStreamAsync(link))
                {
                    var t = stream.Length;
                    FileStream fileStream = File.Create(destination);
                    byte[] buffer = new byte[1024];
                    double percent = 1024 * 100.0 / stream.Length;
                    double totalPercent = 0;
                    int readed;
                    do
                    {
                        readed = stream.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, readed);
                        totalPercent += percent;
                        CompletedPercent = (int)totalPercent;
                    }
                    while (readed != 0);
                    fileStream.Close();
                    stream.Close();
                }
            }*/
            //});
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
                RaiseCompleted();
            else
                RaiseFailed(e.Error.Message);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            CompletedPercent = e.ProgressPercentage;
        }
    }
}
