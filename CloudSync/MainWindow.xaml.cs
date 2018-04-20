using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Identity.Client;
using System.Linq;

namespace CloudSync
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private System.Windows.Forms.NotifyIcon trayIcon;
		string _graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";//Set the scope for API call to user.read
		
		/// <summary>
		/// OneDriveApi instance to work with
		/// </summary>

		/// <summary>
		/// The refresh token stored in the App Config
		/// </summary>
		public string RefreshToken;
		public MainWindow()
		{
			InitializeComponent();
			trayIcon = new System.Windows.Forms.NotifyIcon();			
			trayIcon.DoubleClick += TrayIcon_DoubleClick;
			StateChanged += MainWindow_StateChanged;
			this.Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			//pwoxqniESAV467|(YOY35*|

			//1040774072306-lrse4dhjchjotlf3e12nlk6tumvi6vv1.apps.googleusercontent.com
			//A1JB6_bP36d8GtzxVPHEVSNI
		}

		private async void CallGraphButton_Click(object sender, RoutedEventArgs e)
		{
            var authResult = await OneDrive.Authenticate();

            if (authResult != null)
			{
				ResultText.Text = await OneDrive.GetHttpContentWithToken(_graphAPIEndpoint, authResult.AccessToken);
				DisplayBasicTokenInfo(authResult);
				this.SignOutButton.Visibility = Visibility.Visible;
			}
		}		

		/// <summary>
		/// Sign out the current user
		/// </summary>
		private void SignOutButton_Click(object sender, RoutedEventArgs e)
		{
            OneDrive.SignOut();
		}

		private void DisplayBasicTokenInfo(AuthenticationResult authResult)
		{
			TokenInfoText.Text = "";
			if (authResult != null)
			{
				TokenInfoText.Text += $"Name: {authResult.User.Name}" + Environment.NewLine;
				TokenInfoText.Text += $"Username: {authResult.User.DisplayableId}" + Environment.NewLine;
				TokenInfoText.Text += $"Token Expires: {authResult.ExpiresOn.ToLocalTime()}" + Environment.NewLine;
				TokenInfoText.Text += $"Access Token: {authResult.AccessToken}" + Environment.NewLine;
			}
		}

		private void TrayIcon_DoubleClick(object sender, EventArgs e)
		{
			this.ShowInTaskbar = true;
			trayIcon.Visible = false;
		}

		private void MainWindow_StateChanged(object sender, EventArgs e)
		{
			switch (WindowState)
			{
				case WindowState.Minimized:
					this.ShowInTaskbar = false;					
					trayIcon.Icon = new System.Drawing.Icon(Application.GetResourceStream(new Uri("pack://application:,,,/1.ico")).Stream);
					trayIcon.Visible = true;
					trayIcon.Text = "Cloud Sync";
					/*trayIcon.BalloonTipTitle = "Minimize Sucessful";
					trayIcon.BalloonTipText = "Minimized the app ";
					trayIcon.ShowBalloonTip(400);*/
					break;
				default:										
					break;
			}
		}        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
