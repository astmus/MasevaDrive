﻿using System;
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
            WebClient client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + OneDriveStat.authResult.AccessToken;
            try
            {
                await client.DownloadFileTaskAsync(link, destination);
            }
            catch (System.Exception ex)
            {
            }
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
