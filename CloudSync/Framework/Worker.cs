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

		public abstract Task DoWorkAsync();
		public abstract void CancelWork();

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
	}
}
