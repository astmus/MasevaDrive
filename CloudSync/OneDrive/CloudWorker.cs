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
       
	}
}
