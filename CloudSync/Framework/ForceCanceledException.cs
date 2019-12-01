using CloudSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync.Framework
{
	public class ForceCanceledException : Exception
	{
		public OneDriveSyncItem SyncItem {get; private set;}
		public ForceCanceledException(OneDriveSyncItem syncItem) : base(string.Format("Item {0} force canceled", syncItem.Name))
		{
			SyncItem = syncItem;
		}
	}
}
