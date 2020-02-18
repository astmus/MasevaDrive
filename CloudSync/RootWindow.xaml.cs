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
using System.IO;
using CloudSync.Windows;
using Microsoft.Win32;
using System.Windows.Media;
using NLog;

namespace CloudSync
{
	/// <summary>
	/// Interaction logic for RootWindow.xaml
	/// </summary>
	public partial class RootWindow : Window
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
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
				BorderThickness = new Thickness(2,2,2,2),
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
#if DEBUG
			saveSettingsTimer.Interval = TimeSpan.FromMinutes(1);
#else
			saveSettingsTimer.Interval = TimeSpan.FromMinutes(10);
#endif

			saveSettingsTimer.Start();
		}

		private void OnSaveSettingsTimerTick(object sender, EventArgs e)
		{
			Settings.Instance.Save();
		}

		private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
		{
			RegistryKey rk = Registry.CurrentUser.OpenSubKey
				("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if (rk.GetValue("CloudSync") == null)
				StartupButton.BorderBrush = new SolidColorBrush(Colors.Orange);
			else
				StartupButton.BorderBrush = new SolidColorBrush(Colors.LightGreen);
			Settings.Instance.Accounts.ToList().ForEach(account =>
			{
				account.StartSyncActiveFolders();
			});
		}		

		private void OnApplicationExit(object sender, ExitEventArgs e)
		{			
			var res = Parallel.ForEach<OneDriveAccount>(from account in Settings.Instance.Accounts select account, new Action<OneDriveAccount>((curAccount) => { curAccount.CancelAndDestructAllActiveWorkers(); }));
			Log.Info("App being close and save settings");
			Settings.Instance.Save();
			Log.Info("App saved setting. Exuit code = "+e.ApplicationExitCode);
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
			ShowInTaskbar = true;
		}

		private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
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

		//public static IPublicClientApplication PublicClientApp;
		private void OnConnectButtonClick(object sender, RoutedEventArgs e)
		{			
			BrowserHolder.Visibility = Visibility.Visible;
			var authStr = OneDriveClient.GetAuthorizationRequestUrl();			
			Browser.Navigate(authStr);
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

		private void OnRecieveFromUSBButton(object sender, RoutedEventArgs e)
		{
			SyncUSB window = new SyncUSB();
			window.SyncPathTextBlock.Content = Settings.Instance.RootFolder;
			window.ShowDialog();
		}

		private void OnAutoStartButtonClick(object sender, RoutedEventArgs e)
		{
			RegistryKey rk = Registry.CurrentUser.OpenSubKey
				("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			var msgBox = new Xceed.Wpf.Toolkit.MessageBox()
			{
				WindowBackground = this.BorderBrush,
				Background = this.Background,
				BorderBrush = this.BorderBrush,
				BorderThickness = this.BorderThickness,
				Foreground = Caption.Foreground,
				CaptionForeground = Caption.Foreground,
				Text = "Application scheduled for startup",
				Caption = "Startup",
				ButtonRegionBackground = this.Background
			};

			var v = rk.GetValue("CloudSync");
			if (rk.GetValue("CloudSync") == null || rk.GetValue("CloudSync").ToString() != Application.ResourceAssembly.Location)
			{
				rk.SetValue("CloudSync", Application.ResourceAssembly.Location);
				msgBox.ShowDialog();
				StartupButton.BorderBrush = new SolidColorBrush(Colors.LightGreen);
			}
			else
			{
				rk.DeleteValue("CloudSync", false);
				msgBox.Text = "Application start up disabled";
				msgBox.ShowDialog();
				StartupButton.BorderBrush = new SolidColorBrush(Colors.Orange);
			}
		}
	}
}
