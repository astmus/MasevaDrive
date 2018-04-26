using CloudSync.OneDrive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSync
{
	class CloudAccountManaged : Dictionary<string,OneDriveClient>
	{
		private static readonly Lazy<CloudAccountManaged> _instance = new Lazy<CloudAccountManaged>(() => new CloudAccountManaged());
		private CloudAccountManaged()
		{
			
		}	

		public static CloudAccountManaged Instance
		{
			get
			{
				return _instance.Value;
			}
		}
	}
}
