using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync.Framework.Exceptions
{
	public class DismantileWorkerException : Exception
	{
		public DismantileWorkerException() : base("Worker was dismantiled")
		{
		}
	}
}
