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

namespace CloudSync.Windows
{
	/// <summary>
	/// Interaction logic for PromptDialogBox.xaml
	/// </summary>
	public partial class PromptDialogBox : Window
	{
		public enum Result
		{
			None,
			Ok,
			Cancel
		}

		public Result ChooseCondition { get; set; } = Result.None;
		public PromptDialogBox()
		{
			InitializeComponent();
		}

		public static PromptDialogBox ShowPrompt()
		{
			var m = new PromptDialogBox();
			m.ShowDialog();
			return m;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			ChooseCondition = Result.Ok;
			Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			ChooseCondition = Result.Cancel;
			Close();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ValueHolder.Focus();
		}
	}
}
