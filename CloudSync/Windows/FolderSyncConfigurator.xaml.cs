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
			var rootFolders = await account.RootDirectories();
			folders.ItemsSource = rootFolders;
            busyIndicator.IsBusy = false;
        }

        private void selectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Grid parent = b.Parent as Grid;
            TextBox folderPath = parent.FindName("folderPath") as TextBox;
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    folderPath.Text = dialog.SelectedPath;
                } 
            }
        }

        public List<OneDriveFolder> Result;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = new List<OneDriveFolder>();
            foreach (OneDriveItem item in folders.SelectedItems)
            {
                ListBoxItem container = folders.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                TextBox folderSyncPath = container.FindVisualChild<TextBox>();
                if (string.IsNullOrEmpty(folderSyncPath.Text))
                {
                    MessageBox.Show("Please select valid path for all directory");
					Result = null;
                    return;
                }
                string dirPath = Path.Combine(folderSyncPath.Text, item.Name);
                if (!Directory.Exists(dirPath))
                    try
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Please select valid path for all directory");
						Result = null;
						return;
                    }                    
                var syncFolder = new OneDriveFolder(item, folderSyncPath.Text);
                Result.Add(syncFolder);               
            }
            this.Close();
        }        

        public new List<OneDriveFolder> Show()
        {
            this.ShowDialog();
            return Result;
        }
    }
}
