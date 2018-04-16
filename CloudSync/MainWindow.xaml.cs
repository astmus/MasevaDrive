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
		string[] _scopes = new string[] { "user.read" };
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
			AuthenticationResult authResult = null;

			try
			{
				authResult = await App.PublicClientApp.AcquireTokenSilentAsync(_scopes, App.PublicClientApp.Users.FirstOrDefault());
			}
			catch (MsalUiRequiredException ex)
			{
				// A MsalUiRequiredException happened on AcquireTokenSilentAsync. This indicates you need to call AcquireTokenAsync to acquire a token
				System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

				try
				{
					authResult = await App.PublicClientApp.AcquireTokenAsync(_scopes);
				}
				catch (MsalException msalex)
				{
					ResultText.Text = $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
				}
			}
			catch (Exception ex)
			{
				ResultText.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
				return;
			}

			if (authResult != null)
			{
				ResultText.Text = await GetHttpContentWithToken(_graphAPIEndpoint, authResult.AccessToken);
				DisplayBasicTokenInfo(authResult);
				this.SignOutButton.Visibility = Visibility.Visible;
			}
		}

		/// <summary>
		/// Perform an HTTP GET request to a URL using an HTTP Authorization header
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="token">The token</param>
		/// <returns>String containing the results of the GET operation</returns>
		public async Task<string> GetHttpContentWithToken(string url, string token)
		{
			var httpClient = new System.Net.Http.HttpClient();
			System.Net.Http.HttpResponseMessage response;
			try
			{
				var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
				//Add the token in Authorization header
				request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				response = await httpClient.SendAsync(request);
				var content = await response.Content.ReadAsStringAsync();
				return content;
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
		}

		/// <summary>
		/// Sign out the current user
		/// </summary>
		private void SignOutButton_Click(object sender, RoutedEventArgs e)
		{
			if (App.PublicClientApp.Users.Any())
			{
				try
				{
					App.PublicClientApp.Remove(App.PublicClientApp.Users.FirstOrDefault());
					this.ResultText.Text = "User has signed-out";
					this.CallGraphButton.Visibility = Visibility.Visible;
					this.SignOutButton.Visibility = Visibility.Collapsed;
				}
				catch (MsalException ex)
				{
					ResultText.Text = $"Error signing-out user: {ex.Message}";
				}
			}
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

		public string OpenAndAwait(string url)
		{
			WebBrowser br = new WebBrowser();
			br.Navigate(url);
			return "";
		}
	}
}
