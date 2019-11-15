using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CloudSync
{
    public abstract class CloudWorker : IProgressable
    {
        public string TaskName { get; set; }
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
		protected DispatcherSynchronizationContext currentContext;
		public virtual event Action<int> PercentCompleted;
        public virtual event Action<CloudWorker, ProgressableEventArgs> Completed;
		//public delegate void WorkerCompleted(CloudWorker worker, ProgressableEventArgs progress);
		//public virtual event WorkerCompleted Completed;
		public event PropertyChangedEventHandler PropertyChanged;
		public virtual Task TaskWithWork { get; protected set; }
		public abstract void CancelWork();
		public abstract void Dismantle();
		public abstract Task DoWorkAsync();
        
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
			//currentContext.Send(state =>
			//{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			//}, null);
        }

        protected void RaiseFailed(Exception error)
        {
			//currentContext.Send(state =>
			//{
				CompletedPercent = -1;
				Completed?.Invoke(this, new ProgressableEventArgs() { Successfull = false, Error = error });
			//}, null);
        }

        protected void RaisePercentCompleted(int i)
        {
			//currentContext.Send(state =>
			//{
				PercentCompleted?.Invoke(i);
			//}, this);
        }

        protected void RaiseCompleted()
        {
			//currentContext.Send(state =>
			//{
				CompletedPercent = 101;
				Completed?.Invoke(this, new ProgressableEventArgs() { Successfull = true });
			//}, null);
			//Completed?. (this, new ProgressableEventArgs() { Successfull = true });
		}
	}
}
