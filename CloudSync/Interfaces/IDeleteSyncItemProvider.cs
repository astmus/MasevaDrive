using CloudSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync.Interfaces
{
	interface IDeleteSyncItemProvider
	{
		Task<Exception> DeleteItem(OneDriveItem item);
	}
}
