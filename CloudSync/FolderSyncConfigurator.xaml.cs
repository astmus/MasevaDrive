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
        
        public FolderSyncConfigurator(List<OneDriveItem> driveFolders)
        {
            InitializeComponent();
            folders.ItemsSource = driveFolders;
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

        List<OneDriveSyncFolder> res = new List<OneDriveSyncFolder>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            foreach (var item in folders.SelectedItems)
            {
                ListBoxItem container = folders.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                TextBox folderSyncPath = container.FindVisualChild<TextBox>();
                var syncFolder = new OneDriveSyncFolder(item as OneDriveItem, folderSyncPath.Text);
                res.Add(syncFolder);
                Directory.CreateDirectory(Path.Combine(syncFolder.PathToSync, syncFolder.Name));
            }
            this.Close();
        }

        

        public new List<OneDriveSyncFolder> Show()
        {
            this.ShowDialog();
            return res;
        }
    }
}
