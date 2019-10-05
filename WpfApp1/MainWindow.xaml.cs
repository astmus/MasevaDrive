using OneDriveRestAPI;
using OneDriveRestAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Client client;
		private string clientID = "32171e35-694f-4481-a8bc-0498cb7da487";
		private string redirectUri = "https://login.live.com/oauth20_desktop.srf";
		private void Button_Click(object sender, RoutedEventArgs e)
        {
            var options = new Options
            {
                ClientId = "32171e35-694f-4481-a8bc-0498cb7da487",
                ClientSecret = "gX@c2]dxG/OcWc.ZJ93iQ2Tcb-ln@g2g",
                CallbackUrl = "https://login.live.com/oauth20_desktop.srf",
                AutoRefreshTokens = true,
                PrettyJson = false,
                ReadRequestsPerSecond = 2,
                WriteRequestsPerSecond = 2
            };

            client = new Client(options);
			var authRequestUrl = String.Format("https://login.microsoftonline.com/common/oauth2/v2.0/authorize?client_id={0}&scope=user.read files.readwrite.all&response_type=code&redirect_uri={1}", clientID, redirectUri);
			browser.Navigated += Browser_Navigated;                  
            browser.LoadCompleted += Browser_LoadCompleted;
            browser.Navigate(authRequestUrl);
            
        }

        bool shouldTake = false;
        private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            
        }
        private async void Browser_Navigated(object sender, NavigationEventArgs e)
        {
                var dict = HttpUtility.ParseQueryString(e.Uri.Query);
                string code = dict.Get("code");
                if (code != null)
                {
                    var token = await client.GetAccessTokenAsync(code);

                }
        }
    }
}
