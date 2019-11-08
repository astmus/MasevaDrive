using CloudSync.Models;
using System;
using System.Collections.Generic;
using System.IO;
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


namespace CloudSync
{
    /// <summary>
    /// Interaction logic for FolderSyncConfigurator.xaml
    /// </summary>
    public partial class FolderSyncConfigurator : Window
    {
		OneDriveAccount account;

		public FolderSyncConfigurator(OneDriveAccount account)
        {
            InitializeComponent();
            this.Loaded += OnWindowsLoaded;
			this.account = account;
        }

        private async void OnWindowsLoaded(object sender, RoutedEventArgs e)
        {
			this.Loaded -= OnWindowsLoaded;
			busyIndicator.IsBusy = true;

			if (account.RootFolders == null || account.RootFolders.Count == 0)
				account.RootFolders = await account.RequestRootFolders();
			folders.ItemsSource = account.RootFolders;
			busyIndicator.IsBusy = false;
        }

        private void selectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Grid parent = b.Parent as Grid;
            Label folderPath = (parent.FindName("StackHolder") as StackPanel).FindName("folderPath") as Label;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    folderPath.Content = dialog.SelectedPath;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (OneDriveFolder item in folders.ItemsSource)
            {
				if (item.IsActive && item.PathToSync == null)
				{
					using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
					{
						if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							item.PathToSync = dialog.SelectedPath;
					}
				}

				if (item.PathToSync != null)
				{
					var dirPath = Path.Combine(item.PathToSync as String, item.Name);
					if (!Directory.Exists(dirPath))
						try
						{
							Directory.CreateDirectory(dirPath);
						}
						catch (System.Exception ex)
						{
							MessageBox.Show("Please select valid path for all directory");
							return;
						}
				}             
            }
			DialogResult = true;
            this.Close();
        }        
    }
}
