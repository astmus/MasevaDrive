using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using CloudSync.Framework;

namespace CloudSync
{
    public abstract class CloudWorker : Worker
	{		
		protected DispatcherSynchronizationContext currentContext;
        public virtual event Action<CloudWorker, ProgressableEventArgs> Completed;
		public virtual Task TaskWithWork { get; protected set; }		
		public abstract void Dismantle();
		
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
				CompletedPercent = 101;
				Completed?.Invoke(this, new ProgressableEventArgs() { Successfull = true });
			//}, null);
			//Completed?. (this, new ProgressableEventArgs() { Successfull = true });
		}
	}
}
