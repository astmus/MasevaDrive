using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.IO;
using System.Net;
using CloudSync.OneDrive;
using CloudSync.Models;

namespace CloudSync
{
	public class DownloadFileWorker : CloudWorker
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		public string Link { get; set; }
		public string Destination { get; set; }
		private OneDriveClient owner;
		public OneDriveSyncItem SyncItem;
		public bool DeleteOldestFileOnSuccess { get; set; } = false;
		public DownloadFileWorker(OneDriveSyncItem syncItem, string destination, OneDriveClient owner) : base()
        {
            this.Link = syncItem.Link;
            this.Destination = destination;
			this.owner = owner;
			this.SyncItem = syncItem;
        }

        public override void DoWork()
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + owner.AccessToken;
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
				RaiseFailed(e.Error.Message);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            CompletedPercent = e.ProgressPercentage;
        }

		public override string ToString()
		{
			return String.Format("{0}",SyncItem);
		}
	}
}
