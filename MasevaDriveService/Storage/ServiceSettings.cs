using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasevaDriveService
{
	public class ServiceSettings : ApplicationSettingsBase
	{
		[UserScopedSetting()]
		[DefaultSettingValue("")]
		public string Subscribers
		{
			get
			{
				return ((string)this["Subscribers"]);
			}
			set
			{
				this["Subscribers"] = (string)value;
			}
		}
	}
}
