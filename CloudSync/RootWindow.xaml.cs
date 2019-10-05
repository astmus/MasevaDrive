using CloudSync.OneDrive;
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

namespace CloudSync
{
	/// <summary>
	/// Interaction logic for RootWindow.xaml
	/// </summary>
	public partial class RootWindow : Window
	{
		System.Windows.Forms.NotifyIcon trayIcon = new System.Windows.Forms.NotifyIcon()
		{
			Icon = new System.Drawing.Icon(Application.GetResourceStream(new Uri("pack://application:,,,/1.ico")).Stream),
			Text = "Cloud Sync"
		};
		OneDriveClient Client;

		private Xceed.Wpf.Toolkit.MessageBox InitializeErrorMessageBox()
		{
			return new Xceed.Wpf.Toolkit.MessageBox()
			{
				WindowBackground = this.BorderBrush,
				Background = this.Background,
				BorderBrush = this.BorderBrush,
				BorderThickness = this.BorderThickness,
				Foreground = Caption.Foreground,
				CaptionForeground = Caption.Foreground,
				Text = "Can't get access to the account",
				Caption = "Error",
				ButtonRegionBackground = this.Background
			};
		}

		Xceed.Wpf.Toolkit.MessageBox _errorMessageBox;
		Xceed.Wpf.Toolkit.MessageBox ErrorMessageBox
		{
			get
			{
				return (_errorMessageBox ?? (_errorMessageBox = InitializeErrorMessageBox()));
			}
		}
				
		public RootWindow()
		{
			InitializeComponent();
			trayIcon.Click += RestoreWindow;
		}

		private void RestoreWindow(object sender, EventArgs e)
		{
			this.Show();
			this.WindowState = WindowState.Normal;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
			ShowInTaskbar = false;
		}

		private void OnWindowStateChanged(object sender, EventArgs e)
		{
			switch (WindowState)
			{
				case WindowState.Minimized:
					trayIcon.Visible = true;
					this.Hide();
					break;
				case WindowState.Normal:
					trayIcon.Visible = false;
					break;
				default:
					break;
			}
		}

		private void OnCloseButtonClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void OnConnectButtonClick(object sender, RoutedEventArgs e)
		{
			BrowserHolder.Visibility = Visibility.Visible;
			var authStr = OneDriveClient.GetAuthorizationRequestUrl();
			Browser.Navigate(authStr);
		}

		private void OnWebBrowserNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			var dict = HttpUtility.ParseQueryString(e.Uri.Query);
			string code = dict.Get("code");
			if (code != null)
			{
				try
				{
					Client = OneDriveClient.AcquireUserByAuthorizationCode(code);
					BrowserHolder.Visibility = Visibility.Collapsed;
					ConnectButton.Visibility = Visibility.Collapsed;
				}
				catch (System.Exception ex)
				{
					ErrorMessageBox.ShowDialog();
				}
			}			
			if (dict.Get("error") != null)
			{
				
				BrowserHolder.Visibility = Visibility.Collapsed;
				ErrorMessageBox.ShowDialog();
			}

		}
	}
}
