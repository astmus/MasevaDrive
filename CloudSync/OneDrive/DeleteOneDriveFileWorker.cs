using CloudSync.Framework.Exceptions;
using CloudSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudSync.Interfaces;
using CloudSync.Framework;

namespace CloudSync.OneDrive
{
	class DeleteOneDriveFileWorker : CloudWorker
	{
		private IDeleteSyncItemProvider connectionProvider;

		public DeleteOneDriveFileWorker(OneDriveSyncItem syncItem, IDeleteSyncItemProvider connectionProvider)
		{
			SyncItem = syncItem;
			this.connectionProvider = connectionProvider;
		}

		private int numberOfAttempts = 0;
		public override int NumberOfAttempts
		{
			get { return numberOfAttempts; }
		}

		public override void CancelWork()
		{
			
		}

		public override void Dismantle()
		{
			RaiseFailed(new DismantileWorkerException());
		}

		public override Task DoWorkAsync()
		{
			return StartWorkCancellable();
		}

		public override Task DoWorkAsync(int delayMilliseconds = 0)
		{
			return StartWorkCancellable(delayMilliseconds);
		}

		private Task StartWorkCancellable(int delay = 0)
		{
			if (delay != 0)
				TaskWithWork = Task.Delay(delay).ContinueWith((t) => { DoWork(); });
			else
				TaskWithWork = Task.Run(() => { DoWork(); });
			return TaskWithWork;
		}

		private async void DoWork()
		{
			numberOfAttempts++;
			Status = "Request delete file";
			var result = await connectionProvider.DeleteItem(SyncItem);
			if (result == null)
				RaiseCompleted();
			else
				RaiseFailed(result);
		}
	}
}
