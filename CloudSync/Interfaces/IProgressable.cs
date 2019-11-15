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
		string AdditionalInfo { get; set; }
        event Action<int> PercentCompleted;        
        Task DoWorkAsync();
        int CompletedPercent { get; set; }
    }

	public class ProgressableEventArgs : EventArgs
	{
		public bool Successfull { get; set; }
		public Exception Error { get; set; }
	}
}
