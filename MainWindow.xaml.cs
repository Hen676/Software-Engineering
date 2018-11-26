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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		private void Button1(object sender, RoutedEventArgs e)
		{
			list.Items.Add("1");
		}

		private void Button2(object sender, RoutedEventArgs e)
		{
			try
			{
				string index = list.SelectedItem.ToString();
				list.Items.Remove(list.SelectedItems[0]);
			}
			catch
			{
				MessageBox.Show("No item selected for deleation");
			}
		}
	}
}