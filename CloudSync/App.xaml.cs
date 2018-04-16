using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CloudSync
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static string ClientId = "32171e35-694f-4481-a8bc-0498cb7da487";
		public static PublicClientApplication PublicClientApp = new PublicClientApplication(ClientId);
	}
}
