﻿using CloudSync.Models;
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
using System.Windows.Shapes;

namespace CloudSync
{
    /// <summary>
    /// Interaction logic for FolderSyncConfigurator.xaml
    /// </summary>
    public partial class FolderSyncConfigurator : Window
    {
        public FolderSyncConfigurator(List<OneDriveFolder> driveFolders)
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
                if ( dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    folderPath.Text = dialog.SelectedPath;
                } 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            foreach (var item in folders.SelectedItems)
            {
                ListBoxItem container = folders.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                TextBox folderSyncPath = FindVisualChild<TextBox>(container);
                
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
                  where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
