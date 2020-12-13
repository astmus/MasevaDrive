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
using CloudSync.Extensions;
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
		private int attemptsCount = 0;
		private Semaphore downloadLimiter;
		
		public override int NumberOfAttempts
		{
			get { return attemptsCount; }			
		}

		public DownloadFileWorker(OneDriveSyncItem item, string destination, ICloudStreamProvider provider) : base()
        {
            this.SyncItem = item;
			this.downloadLimiter = provider.GetDownloadSrteamLimiter();
			this.Destination = destination;
			this.streamProvider = provider;
			Status += " download";
        }

		public override Task DoWorkAsync()
		{
			return StartWorkCancellable(0);
		}	

		public override Task DoWorkAsync(int delay = 0)
		{
			return StartWorkCancellable(delay);
		}

		public override void Dismantle()
		{
			RaiseFailed(new DismantileWorkerException());
		}

		public override void CancelWork()
		{
			cancelTokenSource.Cancel(true);
		}

		private Task StartWorkCancellable(int delay = 0)
		{
			cancelTokenSource = new CancellationTokenSource();
			var cancelToken = cancelTokenSource.Token;
			currentContext = SynchronizationContext.Current as DispatcherSynchronizationContext;
			if (delay != 0)
				TaskWithWork = Task.Delay(delay, cancelToken).ContinueWith((t) => { DoWork(cancelToken); }, cancelToken);
			else
				TaskWithWork = Task.Run(() => { DoWork(cancelToken); }, cancelToken);
			return TaskWithWork;
		}

		private async void DoWork(CancellationToken token)
        {
			attemptsCount++;
			Status = Statuses.Started;
			int bufferSize = 4096;			
			if (SyncItem.Size.AsMB() > 50)
				bufferSize = 32768;
			else
			if (SyncItem.Size.AsMB() > 10)
				bufferSize = 16384;			
			Stream contentStream = null;
			try
			{
				downloadLimiter.WaitOne();
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
			finally
			{
				downloadLimiter.Release();
			}
		}

		public override string ToString()
		{
			return String.Format("{0}",Path.GetFileName(Destination));
		}
	}
}
