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
	/// Interaction logic for RootWindow.xaml
	/// </summary>
	public partial class RootWindow : Window
	{
		System.Windows.Forms.NotifyIcon trayIcon = new System.Windows.Forms.NotifyIcon()
		{
			Icon = new System.Drawing.Icon(Application.GetResourceStream(new Uri("pack://application:,,,/1.ico")).Stream),
			Text = "Cloud Sync"
		};
		public RootWindow()
		{
			InitializeComponent();
			trayIcon.Click += RestoreWindow;			
		}

		private void RestoreWindow(object sender, EventArgs e)
		{
			this.Show();
			this.WindowState = WindowState.Normal;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
			ShowInTaskbar = false;
		}

		private void OnWindowStateChanged(object sender, EventArgs e)
		{
			switch (WindowState)
			{
				case WindowState.Minimized:
					trayIcon.Visible = true;
					this.Hide();					
					break;
				case WindowState.Normal:
					trayIcon.Visible = false;
					break;
				default:
					break;
			}
		}

		private void OnCloseButtonClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void OnConnectButtonClick(object sender, RoutedEventArgs e)
		{
			BrowserHolder.Visibility = Visibility.Visible;			
		}
	}
}
