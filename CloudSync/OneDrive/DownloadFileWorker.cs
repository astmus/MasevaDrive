using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.IO;
using System.Net;
using CloudSync;
using CloudSync.Models;

namespace CloudSync
{
	public class DownloadFileWorker : CloudWorker
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		public string Link { get; set; }
		public string Destination { get; set; }
		public string Token { get; set; }
		//private OneDriveClient owner;
		//public OneDriveSyncItem SyncItem;
		public bool DeleteOldestFileOnSuccess { get; set; } = false;

		public DownloadFileWorker()
		{
		}

		public DownloadFileWorker(string link, string destination, string token) : base()
        {
            this.Link = link;
            this.Destination = destination;
			this.Token = token;
        }

        public override void DoWork()
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + Token;
            try
            {
				logger.Debug("start download of file " + Destination);
                client.DownloadFileTaskAsync(Link, Destination);
            }
            catch (System.Exception ex)
            {
				logger.Warn(ex, "DoWork failed reason = {0}", ex);
            }
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
			if (e.Error == null)
				RaiseCompleted();
			else
				RaiseFailed(e.Error);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            CompletedPercent = e.ProgressPercentage;
        }

		public override string ToString()
		{
			return String.Format("{0}",Path.GetFileName(Destination));
		}
	}
}
