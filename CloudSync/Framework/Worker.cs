using CloudSync.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync.Framework
{
	public abstract class Worker : IProgressable
	{
		public static class Statuses
		{
			public static readonly string WaitToStart = "Wait to start";
			public static readonly string Started = "Started";
		}

		public abstract int NumberOfAttempts { get; }
		public abstract Task DoWorkAsync();
		public abstract void CancelWork();
		public abstract void Dismantle();
		public virtual Task TaskWithWork { get; protected set; }
		public virtual event Action<Worker, ProgressableEventArgs> Completed;
		public OneDriveSyncItem SyncItem { get; protected set; }

		private int completedPercent;
		public int CompletedPercent
		{
			get { return completedPercent; }

			set { if (value != completedPercent) { completedPercent = value; NotifyPropertyChanged(); } }
		}

		private string additionalInfo;
		public string AdditionalInfo
		{
			get { return additionalInfo; }

			set { if (value != additionalInfo) { additionalInfo = value; NotifyPropertyChanged(); } }
		}

		private string status = Statuses.WaitToStart;
		public string Status
		{
			get { return status; }

			set { if (value != status) { status = value; NotifyPropertyChanged(); } }
		}

		public virtual string TaskName { get; set; }

		public event Action<int> PercentCompleted;
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			//currentContext.Send(state =>
			//{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			//}, null);
		}
		protected void RaisePercentCompleted(int i)
		{
			//currentContext.Send(state =>
			//{
			PercentCompleted?.Invoke(i);
			//}, this);
		}

		public abstract Task DoWorkAsync(int delayMilliseconds = 0);

		public void ForceCancel()
		{
			RaiseFailed(new ForceCanceledException(SyncItem));
		}		

		protected void RaiseFailed(Exception error)
		{
			//currentContext.Send(state =>
			//{
			AdditionalInfo = error.Message;
			CompletedPercent = -1;
			Completed?.Invoke(this, new ProgressableEventArgs() { Successfull = false, Error = error });
			//}, null);
		}

		protected void RaiseCompleted()
		{
			//currentContext.Send(state =>
			//{			
			SyncItem.CurrentState = (SyncState)(((int)SyncItem.CurrentState) + 1);
			CompletedPercent = 101;
			Completed?.Invoke(this, new ProgressableEventArgs() { Successfull = true });
			//}, null);
			//Completed?. (this, new ProgressableEventArgs() { Successfull = true });
		}

		public override string ToString()
		{
			return TaskName + SyncItem.ToString();
		}
	}
}
