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
using System.Windows.Shapes;
using CloudSync.Extensions;

namespace CloudSync.Windows
{
	/// <summary>
	/// Interaction logic for SyncUSB.xaml
	/// </summary>
	public partial class SyncUSB : Window
	{
		List<DriveInfo> removableDrives = null;
		DriveInfo currentDrive = null;
		IEnumerable<FileInfo> mediaFilesOnTheDrive;
		public SyncUSB()
		{
			InitializeComponent();
			removableDrives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable).ToList();
			DrivesCombobox.ItemsSource = removableDrives.Select(d=> d.RootDirectory + "(" + d.VolumeLabel + ")");
			DrivesCombobox.SelectionChanged += DrivesCombobox_SelectionChanged;
		}

		private void DrivesCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			currentDrive = removableDrives[DrivesCombobox.SelectedIndex];
			mediaFilesOnTheDrive = currentDrive.RootDirectory.GetFiles("*.*", SearchOption.AllDirectories).Where(path => path.Name.ToLower().EndsWith(".mp4") 
																									|| path.Name.ToLower().EndsWith(".jpg")
																									|| path.Name.ToLower().EndsWith(".3gp")
																									|| path.Name.ToLower().EndsWith(".mov"));
			long totalSize = mediaFilesOnTheDrive.Sum(f => f.Length);
			TotalCountSize.Content = mediaFilesOnTheDrive.Count() + " files (" + (totalSize.AsMB().ToString() + " MB)");
		}

		private void OnSyncButtonClick(object sender, RoutedEventArgs e)
		{

			this.Close();
		}
	}
}
