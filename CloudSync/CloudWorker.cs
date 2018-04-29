﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync
{
    public abstract class CloudWorker : IProgressable
    {
        public string TaskName { get; set; }
        private int completedPercent;
        public int CompletedPercent
        {
            get { return completedPercent; }
            set
            {
                if (value != completedPercent)
                {
                    completedPercent = value;
                    NotifyPropertyChanged();
                }
            }
        }        

        public virtual event Action<int> PercentCompleted;
        public virtual event Action<IProgressable, ProgressableEventArgs> Completed;
        public event PropertyChangedEventHandler PropertyChanged;        

		public abstract void DoWork();
        
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaiseFailed(string error)
        {
            CompletedPercent = -1;
			Completed?.Invoke(this, new ProgressableEventArgs() { Successfull = false, Error = new Exception(error) });  
        }

        protected void RaisePercentCompleted(int i)
        {
            PercentCompleted?.Invoke(i);
        }

        protected void RaiseCompleted()
        {
            CompletedPercent = 0;
            Completed?.Invoke(this, new ProgressableEventArgs() { Successfull = true });
        }
    }
}
