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
			busyIndicator.IsBusy = true;
			Task.Factory.StartNew(() =>
			{
				mediaFilesOnTheDrive = new List<FileInfo>();
				currentDrive.RootDirectory.EnumerateDirectories().Where(dir => dir.Name != "System Volume Information").ToList().ForEach(dir2 =>
				{
					mediaFilesOnTheDrive.AddRange(dir2.GetFiles("*.*", SearchOption.AllDirectories).Where(path => path.Name.ToLower().EndsWith(".mp4")
																											|| path.Name.ToLower().EndsWith(".jpg")
																											|| path.Name.ToLower().EndsWith(".3gp")
																											|| path.Name.ToLower().EndsWith(".mov")));

				});
				mediaFilesOnTheDrive.AddRange(currentDrive.RootDirectory.GetFiles("*.*", SearchOption.TopDirectoryOnly).Where(path => path.Name.ToLower().EndsWith(".mp4")
																											|| path.Name.ToLower().EndsWith(".jpg")
																											|| path.Name.ToLower().EndsWith(".3gp")
																											|| path.Name.ToLower().EndsWith(".mov")));
			    long totalSize = mediaFilesOnTheDrive.Sum(f => f.Length);				
				progressBar.Dispatcher.Invoke(() => { progressBar.Maximum = mediaFilesOnTheDrive.Count; });
				TotalCountSize.Dispatcher.Invoke(() =>
				{
					TotalCountSize.Content = " (" + (totalSize.AsMB().ToString() + " MB)(" + mediaFilesOnTheDrive.Count + " items)");
				});
				
				var result = mediaFilesOnTheDrive.OrderBy(t => t.CreationTime).AsParallel().AsOrdered().WithMergeOptions(ParallelMergeOptions.NotBuffered).Select(f => new TransmittMedia(f));
				foreach(TransmittMedia t in result)
				{
					Thumbnails.Add(t);
					progressBar.Dispatcher.Invoke(() => { progressBar.Value++; });
				}
				progressBar.Dispatcher.Invoke(() => { progressBar.Value = 0; });
				busyIndicator.Dispatcher.Invoke(() => { busyIndicator.IsBusy = false; });				
			});
		}

		private async void OnSyncButtonClick(object sender, RoutedEventArgs e)
		{
			progressBar.Value = 0;
			progressBar.Maximum = Thumbnails.Where(i => i.IsSelected).Count();
			busyIndicator.IsBusy = true;
			busyIndicator.BusyContent = "Please wait..." + Environment.NewLine+String.Format("{0} copied from {1}", progressBar.Value, progressBar.Maximum);		
			foreach (var item in Thumbnails.Where(i=> i.IsSelected).ToList())
			{
				var pathFolder = System.IO.Path.Combine(SyncPathTextBlock.Content.ToString(), item.fileInfo.CreationTime.ToString("yyyy.MM"), item.fileInfo.CreationTime.ToString("dd"));
				if (!Directory.Exists(pathFolder))
					Directory.CreateDirectory(pathFolder);
				var destenation = System.IO.Path.Combine(pathFolder, item.fileInfo.Name);
				var info = new FileInfo(destenation);
				progressBarDetail.Value = 0;
				if (!info.Exists)
					await XCopy.CopyAsync(item.fileInfo.FullName, destenation, false, true, (o, progress)=> 
					{
						progressBarDetail.Dispatcher.InvokeAsync(()=> { progressBarDetail.Value++; });
					});
				else
				{					
					var Text = String.Format("File {0} already exist. Old size {1} KB new size {2} KB. Overwrite?", item.fileInfo.Name, item.fileInfo.Length.AsKB(), info.Length.AsKB());
					var result = Xceed.Wpf.Toolkit.MessageBox.Show( Text, "File already exists", MessageBoxButton.OKCancel, Resources["ExistStyle"] as Style);
					if (result == MessageBoxResult.OK)
					{
						info.Delete();
						await XCopy.CopyAsync(item.fileInfo.FullName, destenation, false, true, (o, progress) =>
						{
							progressBarDetail.Dispatcher.InvokeAsync(() => { progressBarDetail.Value++; });
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
			GC.Collect();
		}

		private void UnselectAllClicked(object sender, RoutedEventArgs e)
		{
			foreach (var item in Thumbnails)
			{
				item.IsSelected = false;
			}
		}

		private void SelectAllClicked(object sender, RoutedEventArgs e)
		{
			foreach (var item in Thumbnails)
			{
				item.IsSelected = true;
			}
		}

		private void SyncWithFolderName(object sender, RoutedEventArgs e)
		{
			progressBar.Maximum = Thumbnails.Where(i => i.IsSelected).Count();
			var res = PromptDialogBox.ShowPrompt();
			if (res.ChooseCondition != PromptDialogBox.Result.Ok)
				return;

			var additionalFolderName = res.ValueHolder.Text;
			foreach (var item in Thumbnails.Where(i => i.IsSelected).ToList())
			{
				var pathFolder = System.IO.Path.Combine(SyncPathTextBlock.Content.ToString(), item.fileInfo.CreationTime.ToString("yyyy.MM"), item.fileInfo.CreationTime.ToString("dd") + " " + additionalFolderName);
				
				if (!Directory.Exists(pathFolder))
					Directory.CreateDirectory(pathFolder);
				var destenation = System.IO.Path.Combine(pathFolder, item.fileInfo.Name);
				var info = new FileInfo(destenation);
				if (!info.Exists)
					item.fileInfo.CopyTo(destenation);
				else
				{
					var Text = String.Format("File {0} already exist. Old size {1} KB new size {2} KB. Overwrite?", item.fileInfo.Name, item.fileInfo.Length.AsKB(), info.Length.AsKB());
					var result = Xceed.Wpf.Toolkit.MessageBox.Show(Text, "File already exists",MessageBoxButton.YesNo, Resources["ExistStyle"] as Style);
					if (result == MessageBoxResult.Yes)
					{
						info.Delete();
						item.fileInfo.CopyTo(destenation);
					}
				}
				Thumbnails.Remove(item);
				progressBar.Value += 1;
			}
			progressBar.Value = 0;
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
			progressBar.Maximum = Thumbnails.Where(i => i.IsSelected).Count();
			
			foreach (var item in Thumbnails.Where(i => i.IsSelected).ToList())
			{
				var pathFolder = selectedFolder;
				if (!Directory.Exists(pathFolder))
					Directory.CreateDirectory(pathFolder);
				var destenation = System.IO.Path.Combine(pathFolder, item.fileInfo.Name);
				var info = new FileInfo(destenation);
				if (!info.Exists)
					item.fileInfo.CopyTo(destenation);
				else
				{
					var Text = String.Format("File {0} already exist. Old size {1} KB new size {2} KB. Overwrite?", item.fileInfo.Name, item.fileInfo.Length.AsKB(), info.Length.AsKB());
					var result = Xceed.Wpf.Toolkit.MessageBox.Show(Text, "File already exists", MessageBoxButton.YesNo, Resources["ExistStyle"] as Style);
					if (result == MessageBoxResult.Yes)
					{
						info.Delete();
						item.fileInfo.CopyTo(destenation);
					}
				}
				Thumbnails.Remove(item);
				progressBar.Value += 1;
			}
			progressBar.Value = 0;
		}

		private void OnDeleteItemClick(object sender, RoutedEventArgs e)
		{
			TransmittMedia item = (e.Source as System.Windows.Controls.MenuItem).DataContext as TransmittMedia;
			var result = Xceed.Wpf.Toolkit.MessageBox.Show("Remove "+ item.fileInfo.Name+" ?", "Delete file", MessageBoxButton.YesNo, Resources["ExistStyle"] as Style);
			if (result == MessageBoxResult.Yes)
			{
				item.fileInfo.Delete();
				Thumbnails.Remove(item);
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

		public TransmittMedia(FileInfo info)
		{
			fileInfo = info;

			if (fileInfo.Name.ToLower().EndsWith(".jpg"))
				using (var original = System.Drawing.Image.FromFile(fileInfo.FullName))
				{
					_thumbnail = original.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
				}
			else
				_thumbnail = GenerateVideoThumbnail(fileInfo.FullName, 0.5f, null);
		}

		public static System.Drawing.Image GenerateVideoThumbnail(string mediaFile, float amount, System.Drawing.Size? imagesize)
		{
			System.Drawing.Size size = imagesize ?? new System.Drawing.Size(120, 80);
			MediaPlayer player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };

			player.Open(new Uri(mediaFile));
			player.Pause();
			//We need to give MediaPlayer some time to load.			 
			System.Threading.Thread.Sleep(1000);
			var totalduration = player.NaturalDuration;
			double offset;

			if (totalduration.HasTimeSpan)
				offset = totalduration.TimeSpan.TotalSeconds * amount;
			else
				offset = 1;

			player.Position = TimeSpan.FromSeconds(offset);
			player.Play();
			player.Pause();

			System.Threading.Thread.Sleep(500);

			RenderTargetBitmap rtb = new RenderTargetBitmap(size.Width, size.Height, 96, 96, PixelFormats.Pbgra32);
			DrawingVisual dv = new DrawingVisual();			
			using (DrawingContext dc = dv.RenderOpen())
			{
				dc.DrawVideo(player, new Rect(0, 0, size.Width, size.Height));
			}
			rtb.Render(dv);
			Duration duration = player.NaturalDuration;
			BitmapFrame frame = BitmapFrame.Create(rtb).GetCurrentValueAsFrozen() as BitmapFrame;			
			BitmapEncoder encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(frame as BitmapFrame);
			
			MemoryStream memoryStream = new MemoryStream();
			encoder.Save(memoryStream);
			//Here we have the thumbnail in the MemoryStream!
			memoryStream.Seek(0, SeekOrigin.Begin);
			player.Close();
			return new Bitmap(memoryStream);
		}

		public FileInfo fileInfo { get; set; }

		private System.Drawing.Image _thumbnail;
		public ImageSource Thumbnail { get { return ConvertImage(_thumbnail); }
			set
			{				
				NotifyPropertyChanged();
			}
		}
		private bool _isSelected = true;
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
