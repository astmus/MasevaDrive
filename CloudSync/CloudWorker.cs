using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync
{
    public class CloudWorker : IProgressable
    {
        public string TaskName { get; set; }

        public event Action<int> PercentCompleted;
        public event Action<int> Completed;

        public virtual void DoWork()
        {
            
        }
    }
}
