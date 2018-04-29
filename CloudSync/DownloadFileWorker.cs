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
        private string link;
        private string destination;
		private OneDriveClient owner;
		public OneDriveSyncItem SyncItem;
		public DownloadFileWorker(OneDriveSyncItem syncItem, string destination, OneDriveClient owner) : base()
        {
            this.link = syncItem.Link;
            this.destination = destination;
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
                client.DownloadFileTaskAsync(link, destination);
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
