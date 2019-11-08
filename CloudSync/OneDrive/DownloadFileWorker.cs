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
using CloudSync.Interfaces;

namespace CloudSync
{
	public class DownloadFileWorker : CloudWorker
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		public string Link { get; set; }
		public string Destination { get; private set; }
		public string Token { get; set; }
		public Exception LastException { get; private set; }
		//private OneDriveClient owner;

		public bool DeleteOldestFileOnSuccess { get; set; } = false;
		private ICloudStreamProvider streamProvider;
		private OneDriveSyncItem syncItem;

		public DownloadFileWorker()
		{
		}

		public DownloadFileWorker(OneDriveSyncItem item, string destination, ICloudStreamProvider provider) : base()
        {
            this.syncItem = item;
            this.Destination = destination;
			this.streamProvider = provider;
        }

        public async override void DoWork()
        {
			int bufferSize = 4096;

			if (syncItem.Size.AsMB() > 50)
				bufferSize = 16384;
			else
			if (syncItem.Size.AsMB() > 10)
				bufferSize = 8192;

			using (Stream contentStream = await streamProvider.GetStreamToFileAsync(syncItem.Link), fileStream = new FileStream(Destination, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
			{
				var totalRead = 0L;
				int readed = 0;
				var buffer = new byte[bufferSize];

				while ((readed = await contentStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
				{
					await fileStream.WriteAsync(buffer, 0, readed);
					totalRead += readed;
					CompletedPercent = (int)Math.Truncate(((double)totalRead / syncItem.Size) * 100);
				}
				contentStream.Close();
			}
			/*WebClient client = new WebClient();
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
            }*/

		}

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
			if (e.Error == null)
				RaiseCompleted();
			else
				LastException = e.Error;
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
