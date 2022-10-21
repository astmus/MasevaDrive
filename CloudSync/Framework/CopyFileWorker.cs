using CloudSync.Framework.Exceptions;
using CloudSync.Models;
using CloudSync.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudSync.Framework
{
	class CopyFileWorker : Worker
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		public string Link { get; set; }
		public string DestinationFolder { get; private set; }
		public string DispathFolder { get; private set; }
		public string DestinationFullFilePath { get; private set; }

		private CancellationTokenSource cancelTokenSource;

		public CopyFileWorker(OneDriveSyncItem item, string dispatchFolder, string destinationFolder) : base()
        {
			this.SyncItem = item;
			this.DestinationFolder = destinationFolder;
			this.DispathFolder = dispatchFolder;
			Status += " copy";
		}

		public override Task DoWorkAsync()
		{
			return StartWorkCancellable(0);
		}

		public override Task DoWorkAsync(int delay = 0)
		{
			return StartWorkCancellable(delay);
		}

		public override void CancelWork()
		{
			cancelTokenSource.Cancel(true);
		}

		public override void Dismantle()
		{
			RaiseFailed(new DismantileWorkerException());
		}		

		private Task StartWorkCancellable(int delay = 0)
		{
			cancelTokenSource = new CancellationTokenSource();
			var cancelToken = cancelTokenSource.Token;
			if (delay != 0)
				TaskWithWork = Task.Delay(delay, cancelToken).ContinueWith((t) => { DoWork(cancelToken); }, cancelToken);
			else
				TaskWithWork = Task.Run(() => { DoWork(cancelToken); }, cancelToken);
			return TaskWithWork;
		}

		private async void DoWork(CancellationToken token)
		{
			Status = Statuses.Started;
			int bufferSize = 4096;			
			try
			{
				Status = "Open file";
				string pathToLoadedFile = Path.Combine(DispathFolder, SyncItem.Name);
				FileInfo file = new FileInfo(pathToLoadedFile);
				//if (file.Length != SyncItem.Size)
				//	throw new FileMismatchSizeException(SyncItem.Name, SyncItem.Size, file.Length, pathToLoadedFile);
				using (var sourceStream = new FileStream(pathToLoadedFile, FileMode.Open, FileAccess.Read, FileShare.None, bufferSize, true))
				{
					Status = "Create destination file";
					string destinationFolderWithMonthSubFolder = Path.Combine(DestinationFolder, SyncItem.CreatedDateTime.ToString("yyyy.MM"));
					if (!Directory.Exists(destinationFolderWithMonthSubFolder))
						Directory.CreateDirectory(destinationFolderWithMonthSubFolder);
					DestinationFullFilePath = Path.Combine(destinationFolderWithMonthSubFolder, SyncItem.Name);
					using (var fileStream = new FileStream(DestinationFullFilePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
					{
						var totalRead = 0L;
						int readed = 0;
						var buffer = new byte[bufferSize];						
						Status = "Copy file";
						while ((readed = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
						{
							await fileStream.WriteAsync(buffer, 0, readed);
							if (token.IsCancellationRequested)
							{
								fileStream.Flush();
								fileStream.Close();
								sourceStream.Close();
								token.ThrowIfCancellationRequested();
							}
							totalRead += readed;
							CompletedPercent = (int)Math.Truncate(((double)totalRead / SyncItem.Size) * 100);
						}
						fileStream.Close();
					}
				}
				Status = "Copy file completed";
				RaiseCompleted();
				File.Delete(pathToLoadedFile);
			}
			catch (System.Exception ex)
			{
				Status = "Failed";
				RaiseFailed(ex);
			}
		}

		public override string ToString()
		{
			return String.Format("{0}", Path.GetFileName(DestinationFolder));
		}		
	}
}
