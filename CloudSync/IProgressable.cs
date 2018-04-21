using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync
{
    public interface IProgressable
    {
        string TaskName { get; set; }
        event Action<int> PercentCompleted;
        event Action<int> Completed;
        void DoWork();
    }
}
