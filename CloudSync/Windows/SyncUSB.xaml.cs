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
using System.Collections.ObjectModel;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xceed.Wpf.Toolkit;
using System.Windows.Forms;
using CloudSync.Framework;
using Medallion.Shell;
using System.Diagnostics;

namespace CloudSync.Windows
{
	/// <summary>
	/// Interaction logic for SyncUSB.xaml
	/// </summary>
	public partial class SyncUSB : Window
	{
		List<DriveInfo> removableDrives = null;
		DriveInfo currentDrive = null;
		List<FileInfo> mediaFilesOnTheDrive;
		
		public SyncUSB()
		{
			InitializeComponent();
			removableDrives = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable).ToList();
			DrivesCombobox.ItemsSource = removableDrives.Select(d=> d.RootDirectory + "(" + d.VolumeLabel + ")");
			DrivesCombobox.SelectionChanged += DrivesCombobox_SelectionChanged;
			BindingOperations.EnableCollectionSynchronization(Thumbnails, ThumbLock);				
		}
		
		public ObservableCollection<TransmittMedia> Thumbnails { get; set; } = new ObservableCollection<TransmittMedia>();
		private object ThumbLock = new object();

		private void DrivesCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			currentDrive = removableDrives[DrivesCombobox.SelectedIndex];
			LoadMediaFilesByPath(currentDrive.RootDirectory.FullName);
		}

		private void LoadMediaFilesByPath(string pathToSourceDirectory)
		{
			DirectoryInfo directory = new DirectoryInfo(pathToSourceDirectory);
			if (directory.Exists == false)
				return;
			busyIndicator.IsBusy = true;
			Task.Factory.StartNew(() =>
			{
				mediaFilesOnTheDrive = new List<FileInfo>();
				directory.EnumerateDirectories().Where(dir => dir.Name != "System Volume Information").ToList().ForEach(dir2 =>
				{
					mediaFilesOnTheDrive.AddRange(dir2.GetFiles("*.*", SearchOption.AllDirectories));
				});
				mediaFilesOnTheDrive.AddRange(directory.GetFiles("*.*", SearchOption.TopDirectoryOnly));
				long totalSize = mediaFilesOnTheDrive.Sum(f => f.Length);
				progressBar.Dispatcher.Invoke(() => { progressBar.Maximum = mediaFilesOnTheDrive.Count; });
				TotalCountSize.Dispatcher.Invoke(() =>
				{
					TotalCountSize.Content = " (" + (totalSize.AsMB().ToString() + " MB)(" + mediaFilesOnTheDrive.Count + " items)");
				});

				var result = mediaFilesOnTheDrive.OrderBy(tm => tm.LastWriteTime).AsParallel().AsOrdered().WithMergeOptions(ParallelMergeOptions.NotBuffered).Select(f => new TransmittMedia(f));

				foreach (TransmittMedia t in result)
				{
					if (t.HasInvalidInputData)
						continue;
					if (t.IsEmpty)
						t.RegenerateThumbnail();					
					Thumbnails.Add(t);
					progressBar.Dispatcher.Invoke(() => { progressBar.Value++; });
				}				
				
				progressBar.Dispatcher.Invoke(() => { progressBar.Value = 0; });
				busyIndicator.Dispatcher.Invoke(() => { busyIndicator.IsBusy = false; });
			});
		}

		private void OnSyncButtonClick(object sender, RoutedEventArgs e)
		{
			CopyFilesToFolder((TransmittMedia item)=> { return System.IO.Path.Combine(SyncPathTextBlock.Content.ToString(), item.FileInfo.LastWriteTime.ToString("yyyy.MM"), item.FileInfo.LastWriteTime.ToString("dd")); });
		}

		private async void CopyFilesToFolder(Func<TransmittMedia, string> fullNameGenerate)
		{
			var selectedFiles = Thumbnails.Where(i => i.IsSelected).ToList();
			if (selectedFiles.Count == 0 && Thumbnails.Count > 0 && Xceed.Wpf.Toolkit.MessageBox.Show(Owner, "Select all files?", "No selected files", MessageBoxButton.YesNo, Resources["ExistStyle"] as Style) == MessageBoxResult.Yes)
				selectedFiles = Thumbnails.ToList();
			progressBar.Maximum = Math.Max(Thumbnails.Where(i => i.IsSelected).Count(), 1);
			busyIndicator.IsBusy = true;
			busyIndicator.BusyContent = "Please wait..." + Environment.NewLine + String.Format("{0} copied from {1}", progressBar.Value, progressBar.Maximum);
			foreach (var item in selectedFiles)
			{
				var pathFolder = fullNameGenerate(item);
				if (!System.IO.Directory.Exists(pathFolder))
					System.IO.Directory.CreateDirectory(pathFolder);
				var destenation = System.IO.Path.Combine(pathFolder, item.FileInfo.Name);
				var info = new FileInfo(destenation);
				progressBarDetail.Value = 0;
				if (!info.Exists)
					await XCopy.CopyAsync(item.FileInfo.FullName, destenation, false, true, (o, progress) =>
					{
						progressBarDetail.Dispatcher.InvokeAsync(() => { progressBarDetail.Value = (progress as ProgressChangedEventArgs).ProgressPercentage; });
					});
				else
				{
					var Text = String.Format("File {0} already exist. Old size {1} KB new size {2} KB. Overwrite?", item.FileInfo.Name, item.FileInfo.Length.AsKB(), info.Length.AsKB());
					var result = Xceed.Wpf.Toolkit.MessageBox.Show(Owner, Text, "File already exists", MessageBoxButton.YesNo, Resources["ExistStyle"] as Style);
					if (result == MessageBoxResult.Yes)
					{
						await XCopy.CopyAsync(item.FileInfo.FullName, destenation, true, true, (o, progress) =>
						{
							progressBarDetail.Dispatcher.InvokeAsync(() => { progressBarDetail.Value = (progress as ProgressChangedEventArgs).ProgressPercentage; });
						});
					}
				}
				Thumbnails.Remove(item);
				progressBar.Value += 1;
				progressBarDetail.Dispatcher.Invoke(() => { progressBarDetail.Value = 0; });
				busyIndicator.BusyContent = "Please wait..." + Environment.NewLine + String.Format("{0} copied from {1}", progressBar.Value, progressBar.Maximum);
				busyIndicator.InvalidateArrange();
				busyIndicator.UpdateLayout();
				this.UpdateLayout();
			}
			busyIndicator.IsBusy = false;
			progressBar.Value = 0;
			progressBarDetail.Value = 0;
			GC.Collect();
		}				

		private void SyncWithFolderName(object sender, RoutedEventArgs e)
		{
			var res = PromptDialogBox.ShowPrompt();
			if (res.ChooseCondition != PromptDialogBox.Result.Ok)
				return;
			var additionalFolderName = res.ValueHolder.Text;
			CopyFilesToFolder((TransmittMedia item) => { return System.IO.Path.Combine(SyncPathTextBlock.Content.ToString(), item.FileInfo.LastWriteTime.ToString("yyyy.MM ") + additionalFolderName); });
		}

		private void SyncToCertainFolder(object sender, RoutedEventArgs e)
		{
			string selectedFolder;
			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					selectedFolder = dialog.SelectedPath;
				else
					return;
			}
			CopyFilesToFolder((TransmittMedia item) => { return selectedFolder; });			
		}
		private void UnselectAllClicked(object sender, RoutedEventArgs e)
		{
			foreach (var item in Thumbnails)
				item.IsSelected = false;
		}

		private void SelectAllClicked(object sender, RoutedEventArgs e)
		{
			foreach (var item in Thumbnails)
				item.IsSelected = true;
		}

		private void OnDeleteItemClick(object sender, RoutedEventArgs e)
		{
			var selected = Thumbnails.Where(i => i.IsSelected).ToList();
			if (selected.Count > 0)
			{				
				var result = Xceed.Wpf.Toolkit.MessageBox.Show(string.Format("Remove {0} files?", selected.Count), "Delete file", MessageBoxButton.YesNo, Resources["ExistStyle"] as Style);
				if (result == MessageBoxResult.Yes)
				{
					selected.ForEach((item) =>
					{
						item.FileInfo.Delete();
						Thumbnails.Remove(item);
					});
				}
			}
		}

		private void OnAddFromFolderClick(object sender, RoutedEventArgs e)
		{
			string sourceFolder = null;
			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					sourceFolder = dialog.SelectedPath;
				else
					return;
			}			
			LoadMediaFilesByPath(sourceFolder);
		}

		private void OnFileMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2)
			{
				TransmittMedia file = (sender as Grid).DataContext as TransmittMedia;
				System.Diagnostics.Process.Start(file.FileInfo.FullName);
			}
		}
	}
	
	public class TransmittMedia : INotifyPropertyChanged
	{
		public static System.Windows.Media.ImageSource ConvertImage(System.Drawing.Image image)
		{
			try
			{
				if (image != null)
				{
					var bitmap = new System.Windows.Media.Imaging.BitmapImage();
					bitmap.BeginInit();
					System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
					image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
					memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
					bitmap.StreamSource = memoryStream;
					bitmap.EndInit();
					return bitmap;
				}
			}
			catch { }
			return null;
		}

		private string TypeName { get { return FileInfo.Extension.Remove(0,1).ToUpper(); } }
		private string FormatSize { get { return ((FileInfo.Length / 1024.0) / 1024.0).ToString("0.##") + " MB"; } }
		public string DetailedData { get { return FormatSize + " " + TypeName; } }
		public string FormattedLastWriteTime { get { return FileInfo.LastWriteTime.ToShortDateString(); } }

		public TransmittMedia(FileInfo info)
		{
			FileInfo = info;			
			try
			{
				var imageStream = CreateThumbnailOfFile(FileInfo.FullName);
				if (!HasInvalidInputData)
					_thumbnail = System.Drawing.Image.FromStream(imageStream);
			}
			catch (System.Exception ex)
			{
				Debug.WriteLine(FileInfo.Name + " " + ex.ToString());
			}			
		}

		public bool RegenerateThumbnail()
		{
			if (HasInvalidInputData)
				return false;	
			try
			{
				var imageStream = CreateThumbnailOfFile(FileInfo.FullName);				
				_thumbnail = System.Drawing.Image.FromStream(imageStream);
				return true;
			}
			catch (System.Exception ex)
			{
				return false;
			}
		}

		private MemoryStream CreateThumbnailOfFile(string filePath)
		{
			try
			{
				string args = string.Format("-hide_banner -i \"{0}\" -qscale:v 16 -an -vf scale=\"320:240\" -vframes 1 -f image2pipe pipe:1", filePath);
				var createThumbnailProcess = Command.Run(Settings.Instance.AppProperties.FFmpegPath, null, options => options.StartInfo((i) =>
				{
					i.Arguments = args;
					i.RedirectStandardOutput = true;
					i.UseShellExecute = false;
				}));

				var result = new MemoryStream();
				createThumbnailProcess.RedirectTo(result);
				createThumbnailProcess.Wait();
				if (createThumbnailProcess.Task.Result.Success == false &&
					(createThumbnailProcess.Task.Result.StandardError.IndexOf("Invalid data found when processing input") != -1
					|| createThumbnailProcess.Task.Result.StandardError.IndexOf("does not contain any stream") != -1))
					HasInvalidInputData = true;
				
				result.Seek(0, SeekOrigin.Begin);
				return result;
			}
			catch (System.Exception ex)
			{
				return null;
			}			
		}

		public FileInfo FileInfo { get; set; }

		private System.Drawing.Image _thumbnail;
		public bool IsEmpty { get { return _thumbnail == null; } }
		public bool HasInvalidInputData { get; private set; }
		public ImageSource Thumbnail { get { return ConvertImage(_thumbnail); }
			set
			{				
				NotifyPropertyChanged();
			}
		}
		private bool _isSelected = false;
		public bool IsSelected { get { return _isSelected; }
			set {
				_isSelected = value;
				NotifyPropertyChanged();
			}
		}

		protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
