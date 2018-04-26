using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CloudSync.OneDrive;
using System.Net;
using System.Net.Http;

namespace CloudSync.Windows
{
    /// <summary>
    /// Interaction logic for OneDriveAuthorizationWindow.xaml
    /// </summary>
    public partial class OneDriveAuthorizationWindow : Window
    {		
		public OneDriveAuthorizationWindow()
        {
            InitializeComponent();
            this.Loaded += OneDriveAuthorizationWindow_Loaded;
        }

		OneDriveClient result;
		private void OneDriveAuthorizationWindow_Loaded(object sender, RoutedEventArgs e)
        {
			var authStr = OneDriveClient.GetAuthorizationRequestUrl();
			browser.Navigate(authStr);
			browser.Navigated += OnNavigated;
		}

		private void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			var dict = HttpUtility.ParseQueryString(e.Uri.Query);
			string code = dict.Get("code");
			if (code != null)
			{		
				result = OneDriveClient.AcquireUserByAuthorizationCode(code);
				this.Close();
			}
		}

		public new OneDriveClient Show()
        {
            this.ShowDialog();
			return result;
        }
    }
}
