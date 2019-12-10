using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;
using CloudSync.OneDrive;
using CloudSync.Framework;
using System.Windows.Threading;

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

		DispatcherTimer saveSettingsTimer = new DispatcherTimer();
		public RootWindow()
		{
			InitializeComponent();
			trayIcon.Click += RestoreWindow;
			foreach (var acount in Settings.Instance.Accounts)
				acount.Client.NeedRelogin += OnAcountNeedAuthorization;
			ConnectedAccounts.ItemsSource = Settings.Instance.Accounts;			
			System.Windows.Application.Current.Exit += OnApplicationExit;
			System.Windows.Application.Current.MainWindow.Loaded += OnMainWindowLoaded;
			saveSettingsTimer.Tick += OnSaveSettingsTimerTick;
			saveSettingsTimer.Interval = TimeSpan.FromMinutes(10);
			saveSettingsTimer.Start();
		}

		private void OnSaveSettingsTimerTick(object sender, EventArgs e)
		{
			Settings.Instance.Save();
		}

		private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
		{
			Settings.Instance.Accounts.ToList().ForEach(account =>
			{
				account.StartSyncActiveFolders();
			});
		}		

		private void OnApplicationExit(object sender, ExitEventArgs e)
		{
			var res = Parallel.ForEach<OneDriveAccount>(from account in Settings.Instance.Accounts select account, new Action<OneDriveAccount>((curAccount) => { curAccount.CancelAndDestructAllActiveWorkers(); }));
			Settings.Instance.Save();
		}

		private void OnAcountNeedAuthorization(OneDriveClient client)
		{
			if (BrowserTitle.Dispatcher.CheckAccess())
			{
				BrowserTitle.Text = "Reauthorization for " + client.UserData.DisplayName;
				var forDel = Settings.Instance.Accounts.First(account => account.Client == client);
				Settings.Instance.Accounts.Remove(forDel);
				OnConnectButtonClick(null, null);
			}
			else
				Browser.Dispatcher.Invoke(() =>
				{
					BrowserTitle.Text = "Reauthorization for " + client.UserData.DisplayName;
					var forDel = Settings.Instance.Accounts.First(account => account.Client == client);
					Settings.Instance.Accounts.Remove(forDel);
					OnConnectButtonClick(null, null);
				});
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
			//Application.Current.Shutdown();
		}

		//public static IPublicClientApplication PublicClientApp;
		private void OnConnectButtonClick(object sender, RoutedEventArgs e)
		{			
			BrowserHolder.Visibility = Visibility.Visible;
			var authStr = OneDriveClient.GetAuthorizationRequestUrl();			
			Browser.Navigate(authStr);
			

			/*	PublicClientApp = PublicClientApplicationBuilder.Create(ClientId).WithAuthority(AzureCloudInstance.AzurePublic, Tenant).Build();
				var CopyauthResult = await PublicClientApp.AcquireTokenInteractive(new List<string>() { "user.read", "files.readwrite", "offline_access" }).ExecuteAsync();

				string Secret = "GsMUnEBN6CYR7PqhE4VAjGneX]cS.4=_";

				//In this example, I am attempting to access the Graph API
				string[] scopes = new string[] { "https://graph.microsoft.com/.default"};
				string url = String.Format("https://login.microsoftonline.com/common/oauth2/v2.0/token");
				string redirectURI = "https://login.live.com/oauth20_desktop.srf";

			//Instantiating and adding properties to Confidential Client

			IConfidentialClientApplication app = ConfidentialClientApplicationBuilder.Create("73b0cdce-a88e-414a-8bb6-571accae6e8a").WithClientSecret(Secret).WithRedirectUri(redirectURI).Build();
				/*Task<Uri> authCodeURL = app.GetAuthorizationRequestUrl(scopes).WithAuthority(url, true).ExecuteAsync();
				var r = await authCodeURL;*/
			//Acquiring token using Client Credentials

			/*Task<AuthenticationResult> tokenTask = app.AcquireTokenForClient(scopes).WithAuthority(url, true).ExecuteAsync();
			var CopyauthResult = await tokenTask;
			app.GetAccountAsync(app.UserTokenCache);*/
		}

		private void ClearIECache()
		{
			ProcessStartInfo p = new ProcessStartInfo("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8");
			p.CreateNoWindow = true;
			p.UseShellExecute = false;
			p.RedirectStandardOutput = true;
			p.RedirectStandardInput = true;
			p.RedirectStandardError = true;
			p.WindowStyle = ProcessWindowStyle.Hidden;
			
			System.Diagnostics.Process.Start(p);
			//Cookies
			p.Arguments = "InetCpl.cpl,ClearMyTracksByProcess 2";			
			//History
			//System.Diagnostics.Process.Start(p);
			//p.Arguments = "InetCpl.cpl,ClearMyTracksByProcess 16";
			//Form(Data)
			System.Diagnostics.Process.Start(p);
			p.Arguments = "InetCpl.cpl,ClearMyTracksByProcess 32";
			//Passwords
			var result = System.Diagnostics.Process.Start(p);
			result.WaitForExit();
		}

		private void OnWebBrowserNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			var dict = HttpUtility.ParseQueryString(e.Uri.Query);
			string code = dict.Get("code");
			if (code != null)
			{
				try
				{
					var newAccount = new OneDriveAccount(OneDriveClient.AcquireClientByCode(code));
					var userId = newAccount.Client.UserData.Id;
					var existingConnectedUser = Settings.Instance.Accounts.FirstOrDefault(a => a.Client.UserData.Id == userId);
					if (existingConnectedUser == null)
						Settings.Instance.Accounts.Add(newAccount);
					else
						existingConnectedUser.Client.CredentialData = newAccount.Client.CredentialData;

					BrowserHolder.Visibility = Visibility.Collapsed;
					ClearIECache();
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

		private void Caption_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void OnSelectedAccountChanged(object sender, SelectionChangedEventArgs e)
		{			
			var SelectedAccount = ConnectedAccounts.SelectedItem as OneDriveAccount;
			if (SelectedAccount != null && (SelectedAccount.RootFolders?.Count ?? 0) == 0)
			{
				ConfigFolderForAccout(SelectedAccount);
				ConnectedAccounts.SelectedItem = null;
			}
		}

		private void ConfigFolderForAccout(OneDriveAccount account)
		{
			if (account != null)
			{
				FolderSyncConfigurator window = new FolderSyncConfigurator(account);
				if (window.ShowDialog() == true)
				{
					account.StartSyncActiveFolders();
				}				
			}
		}

		private void OnTryAgainPressed(object sender, RoutedEventArgs e)
		{
			IProgressable item = (e.Source as MenuItem).DataContext as IProgressable;
			item.DoWorkAsync();
		}

		private void OnResetDeltaLinkClick(object sender, RoutedEventArgs e)
		{
			var selectedAccount = ConnectedAccounts.SelectedItem as OneDriveAccount;
			foreach (var folder in selectedAccount.RootFolders)
				folder.ResetDeltaLink();
		}

		private void OnFolderForSyncClick(object sender, RoutedEventArgs e)
		{
			var SelectedAccount = ConnectedAccounts.SelectedItem as OneDriveAccount;
				ConfigFolderForAccout(SelectedAccount);
		}

		private void ConnectedAccounts_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ConnectedAccounts.ContextMenu.IsOpen = true;
		}

		private void OnAccountContextMenuClosed(object sender, RoutedEventArgs e)
		{
			ConnectedAccounts.SelectedItem = null;
		}

		private void OnRemoveWorkerClick(object sender, RoutedEventArgs e)
		{
			Worker item = (e.Source as MenuItem).DataContext as Worker;
			item.ForceCancel();
		}
	}
}
