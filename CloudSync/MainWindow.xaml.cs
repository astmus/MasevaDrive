using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Identity.Client;
using System.Linq;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CloudSync.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace CloudSync
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private System.Windows.Forms.NotifyIcon trayIcon;
		string _graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";//Set the scope for API call to user.read
		List<OneDriveSyncFolder> oneDriveFolders
        {
            get { return Settings.Instance.FoldersForSync; }
            set { Settings.Instance.FoldersForSync = value; }
        }

        public ObservableCollection<IProgressable> currentWorkers { get; set; } = new ObservableCollection<IProgressable>();
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
            Settings.Instance.Load();
            foldersListBox.ItemsSource = oneDriveFolders;            
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private async void OnNewWorkerReady(IProgressable worker)
        {
            currentWorkers.Add(worker);
            worker.Completed += OnWorkerCompleted;
            worker.Failed += OnWorkerFailed;
            await worker.DoWork();
        }

        private void OnWorkerFailed(IProgressable sender, string message)
        {
            ListBoxItem item = workers.ItemContainerGenerator.ContainerFromItem(sender) as ListBoxItem;
            if (item == null) return;
            TextBlock messageBox = item.FindVisualChildWithName<TextBlock>("message");
            messageBox.Text = message;            
        }

        private void OnWorkerCompleted(IProgressable sender)
        {
            currentWorkers.Remove(sender);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			//pwoxqniESAV467|(YOY35*|
			//1040774072306-lrse4dhjchjotlf3e12nlk6tumvi6vv1.apps.googleusercontent.com
			//A1JB6_bP36d8GtzxVPHEVSNI
		}
        AuthenticationResult authResult;
        private async void CallGraphButton_Click(object sender, RoutedEventArgs e)
		{
             authResult = await OneDrive.Authenticate();

            if (authResult != null)
			{
                users.Items.Add(authResult.User.DisplayableId+" "+ authResult.User.Name);
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
			if (authResult == null)
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
            foreach (var folder in oneDriveFolders)
            {
                folder.NewWorkerReady += OnNewWorkerReady;
                folder.Sync();
            }
            //
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*List<OneDriveItem> f = new List<OneDriveItem>() { new OneDriveItem() { Size=549392,Name="Maxim" }, new OneDriveItem() { Size = 2495912, Name = "De ewr wer wer wer rtt" } };
            FolderSyncConfigurator window = new FolderSyncConfigurator(f);
            oneDriveFolders = window.Show();
            foldersListBox.ItemsSource = oneDriveFolders;
            foldersListBox.SelectAll();
            oneDriveFolders[0].Sync();*/ //https://graph.microsoft.com/v1.0/me/drive/items/65FA3479348E5262!209837/content
            ResultText.Text = await OneDrive.GetHttpContentWithToken(requestFiled.Text, authResult.AccessToken);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Settings.Instance.Save();
        }

        private void folderName_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            (box.DataContext as OneDriveSyncFolder).IsActive = box.IsChecked.Value;
        }

        private async void OnTryAgainPressed(object sender, RoutedEventArgs e)
        {
            IProgressable item = (e.Source as MenuItem).DataContext as IProgressable;
            await item.DoWork();
        }
               
    }
}
