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
			BindingOperations.EnableCollectionSynchronization(Thumbnails, ThumbLock);				
		}
		
		public ObservableCollection<TransmittMedia> Thumbnails { get; set; } = new ObservableCollection<TransmittMedia>();
		private object ThumbLock = new object();

	private void DrivesCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			currentDrive = removableDrives[DrivesCombobox.SelectedIndex];
			Task.Factory.StartNew(() =>
			{
				mediaFilesOnTheDrive = currentDrive.RootDirectory.GetFiles("*.*", SearchOption.AllDirectories).Where(path => path.Name.ToLower().EndsWith(".mp4")
																										|| path.Name.ToLower().EndsWith(".jpg")
																										|| path.Name.ToLower().EndsWith(".3gp")
																										|| path.Name.ToLower().EndsWith(".mov"));
				long totalSize = mediaFilesOnTheDrive.Sum(f => f.Length);
				TotalCountSize.Dispatcher.Invoke(() =>
				{
					TotalCountSize.Content = " (" + (totalSize.AsMB().ToString() + " MB)");
				});
				mediaFilesOnTheDrive.AsParallel().Select(f => new TransmittMedia(f)).ForAll(obj => Thumbnails.Add(obj));
			});
		}

		private void OnSyncButtonClick(object sender, RoutedEventArgs e)
		{
			progressBar.Maximum = Thumbnails.Where(i => i != null && i.IsSelected).Count();
			foreach (var item in Thumbnails.Where(i=>i.IsSelected).ToList())
			{
				var pathFolder = System.IO.Path.Combine(SyncPathTextBlock.Content.ToString(), item.fileInfo.CreationTime.ToString("yyyy.MM"), item.fileInfo.CreationTime.ToString("dd"));
				if (!Directory.Exists(pathFolder))
					Directory.CreateDirectory(pathFolder);
				var destenation = System.IO.Path.Combine(pathFolder, item.fileInfo.Name);
				var info = new FileInfo(destenation);
				if (!info.Exists)
					item.fileInfo.CopyTo(destenation);
				else
				{					
					var Text = String.Format("File {0} already exist. Old size {1} KB new size {2} KB. Overwrite?", item.fileInfo.Name, item.fileInfo.Length.AsKB(), info.Length.AsKB());
					var result = Xceed.Wpf.Toolkit.MessageBox.Show( Text, "File already exists", MessageBoxButton.OKCancel, Resources["ExistStyle"] as Style);
					if (result == MessageBoxResult.OK)
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
				using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
				{
					using (var original = System.Drawing.Image.FromStream(fs))
					{
						_thumbnail = original.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
					}
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
			var thumbnail = new Bitmap(memoryStream);
			player.Close();
			return thumbnail;
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
