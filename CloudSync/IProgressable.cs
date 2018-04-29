using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CloudSync
{
    public interface IProgressable : INotifyPropertyChanged
    {
        string TaskName { get; set; }
        event Action<int> PercentCompleted;
        event Action<IProgressable, ProgressableEventArgs> Completed;
        void DoWork();
        int CompletedPercent { get; set; }
    }

	public class ProgressableEventArgs : EventArgs
	{
		public bool Successfull { get; set; }
		public Exception Error { get; set; }
	}
}
