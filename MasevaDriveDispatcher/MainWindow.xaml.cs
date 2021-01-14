﻿using FrameworkData.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MasevaDriveDispatcher
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{		
		DispatcherTimer checkState = new DispatcherTimer();
		FileSystemWatcher newFilesWatcher;
		DispatcherTimer storageServiceStatusCheck;
		private static SolidColorBrush notActive = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#280a42"));
		private static SolidColorBrush active = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7300e6"));
		public MainWindow()
		{
			InitializeComponent();
			
			
			AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;			
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (StorageServiceHelper.IsInstalled() != true)
			{
				StorageServiceStateTextBlock.Text = "not installed";
				StorageServiceButtton.IsEnabled = false;
			}
			else
			{
				storageServiceStatusCheck = new DispatcherTimer();
				storageServiceStatusCheck.Tick += OnCheckDriveServiceStatus;
				storageServiceStatusCheck.Interval = TimeSpan.FromSeconds(10);
				storageServiceStatusCheck.Start();
				DisplayCurrentServiceStatus();
			}
		}

		private void DisplayCurrentServiceStatus()
		{
			try
			{
				var status = StorageServiceHelper.CurrentStatus();
				StorageServiceStateTextBlock.Text = status.ToString();
				if (status == ServiceControllerStatus.Stopped)
					StorageServiceButtton.Background = notActive;
				else
					StorageServiceButtton.Background = active;
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
				storageServiceStatusCheck.Stop();
			}			
		}

		private void OnCheckDriveServiceStatus(object sender, EventArgs e)
		{
			DisplayCurrentServiceStatus();
		}
				
		private void StorageServiceButtton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (StorageServiceHelper.CurrentStatus() != ServiceControllerStatus.Running)
					StorageServiceHelper.StartService(startStopComletionCallback);
				else
					StorageServiceHelper.StopService(startStopComletionCallback);
				DisplayCurrentServiceStatus();
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
		}

		private void startStopComletionCallback(int errorCode, string message)
		{
			if (errorCode != 0)
				MessageBox.Show(message);
			else
				DisplayCurrentServiceStatus();
		}

		private void CheckState_Tick(object sender, EventArgs e)
		{
			//InformationService.Content = host.State.ToString();
		}
		
		private void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			//host?.Close();
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		private void OnToggleStateOfInformationService(object sender, RoutedEventArgs e)
		{
			/*if (host.State == CommunicationState.Closed)
			{
				host = new ServiceHost(typeof(StorageDataDriveService), new Uri[] { new Uri("net.pipe://localhost") });
				host.AddServiceEndpoint(typeof(IStorageDataDriveService), new NetNamedPipeBinding(), "StorageItemsInfoPipe");
			}

			if (host.State == CommunicationState.Opened)			
				host.Close();
			else
				host.Open();*/			
		}

		private void OnDebug(object sender, RoutedEventArgs e)
		{
			/*if (newFilesWatcher == null)
			{
				newFilesWatcher = new FileSystemWatcher(SolutionSettings.Default.RootOfMediaFolder);
				newFilesWatcher.IncludeSubdirectories = true;
				newFilesWatcher.InternalBufferSize = 1024 * 64;
				newFilesWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
				newFilesWatcher.Created += NewFilesWatcher_Created;
				newFilesWatcher.Deleted += NewFilesWatcher_Deleted;
				newFilesWatcher.Changed += NewFilesWatcher_Changed;
				newFilesWatcher.Renamed += NewFilesWatcher_Renamed;				
				watcherButtton.Content = "Started";
			}
			newFilesWatcher.EnableRaisingEvents = !newFilesWatcher.EnableRaisingEvents;
			watcherButtton.Content = "Started "+ newFilesWatcher.EnableRaisingEvents;*/
		}

		private void NewFilesWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			/*log.Dispatcher.Invoke(() =>
			{
				log.Items.Insert(1,e.ChangeType.ToString() + " ;" + e.OldName + " ;" + e.Name);
			});*/
		}

		private void NewFilesWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			/*log.Dispatcher.Invoke(() =>
			{
				log.Items.Insert(1, e.ChangeType.ToString() + " ;" + e.FullPath + " ;" + e.Name);
			});*/
		}

		private void NewFilesWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			/*log.Dispatcher.Invoke(() =>
			{
				log.Items.Insert(1,e.ChangeType.ToString() + " ;" + e.FullPath + " ;" + e.Name);
			});*/
		}

		private void NewFilesWatcher_Created(object sender, FileSystemEventArgs e)
		{
			/*log.Dispatcher.Invoke(()=> {
				log.Items.Insert(1,e.ChangeType.ToString() + " ;" + e.FullPath + " ;" + e.Name);
			});*/
		}

		
		private void account_Checked(object sender, RoutedEventArgs e)
		{
			var board = this.Resources["openPanel"] as Storyboard;
			Storyboard.SetTargetProperty(board, new PropertyPath("Height"));
			Storyboard.SetTargetName(board, subPanel.Name);
			board.Begin();
		}

		private void account_Unchecked(object sender, RoutedEventArgs e)
		{
			var board = this.Resources["collapsePanel"] as Storyboard;
			Storyboard.SetTargetProperty(board, new PropertyPath("Height"));
			Storyboard.SetTargetName(board, subPanel.Name);
			board.Begin();
		}		
	}
}
