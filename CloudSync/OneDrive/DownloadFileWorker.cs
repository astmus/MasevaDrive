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
using System.Windows.Threading;
using CloudSync.Framework.Exceptions;

namespace CloudSync
{
	public class DownloadFileWorker : CloudWorker
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		public string Link { get; set; }
		public string Destination { get; private set; }
		public Exception LastException { get; private set; }

		private ICloudStreamProvider streamProvider;
		private CancellationTokenSource cancelTokenSource;		
		public OneDriveSyncItem SyncItem { get; private set; }
				
		public DownloadFileWorker(OneDriveSyncItem item, string destination, ICloudStreamProvider provider) : base()
        {
            this.SyncItem = item;
            this.Destination = destination;
			this.streamProvider = provider;
			Status += " download";
        }

		public override Task DoWorkAsync()
		{
			cancelTokenSource = new CancellationTokenSource();
			var cancelToken = cancelTokenSource.Token;
			currentContext = SynchronizationContext.Current as DispatcherSynchronizationContext;
			TaskWithWork = Task.Run(() => { DoWork(cancelToken); }, cancelToken);
			System.Diagnostics.Debug.WriteLine(DateTime.Now.TimeOfDay.ToString()+"("+TaskWithWork.Id+")" + TaskWithWork.IsCompleted.ToString() + TaskWithWork.Status);
			TaskWithWork.ContinueWith((t) => { System.Diagnostics.Debug.WriteLine(DateTime.Now.TimeOfDay.ToString()+ "(" + TaskWithWork.Id + ")" + t.IsCompleted.ToString() + t.Status); });
			return TaskWithWork;
		}

		public override void Dismantle()
		{
			RaiseFailed(new DismantileWorkerException());
		}

		public override void CancelWork()
		{
			cancelTokenSource.Cancel(true);
		}

		private async void DoWork(CancellationToken token)
        {
			Status = Statuses.Started;
			int bufferSize = 4096;			
			if (SyncItem.Size.AsMB() > 50)
				bufferSize = 16384;
			else
			if (SyncItem.Size.AsMB() > 10)
				bufferSize = 8192;			
			Stream contentStream = null;
			try
			{
				Status = "Request file";
				contentStream = await streamProvider.GetStreamToFileAsync(SyncItem.Link);
				using (var fileStream = new FileStream(Destination, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
				{
					var totalRead = 0L;
					int readed = 0;
					var buffer = new byte[bufferSize];
					Status = "Download";
					while ((readed = await contentStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
					{
						await fileStream.WriteAsync(buffer, 0, readed);
						if (token.IsCancellationRequested)
						{
							fileStream.Flush();
							fileStream.Close();
							contentStream.Close();
							token.ThrowIfCancellationRequested();
						}
						totalRead += readed;
						CompletedPercent = (int)Math.Truncate(((double)totalRead / SyncItem.Size) * 100);
					}
					fileStream.Close();
				}
				Status = "Download completed";
				RaiseCompleted();				
			}
			catch (System.Exception ex)
			{
				Status = "Failed";
				contentStream?.Close();
				RaiseFailed(ex);				
			}
		}

		public override string ToString()
		{
			return String.Format("{0}",Path.GetFileName(Destination));
		}
	}
}
