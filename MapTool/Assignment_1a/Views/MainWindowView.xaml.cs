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

namespace Assignment_1a.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

		private void OpenMenu_Click(object sender, RoutedEventArgs e)
		{
			CloseMenu.Visibility = Visibility.Visible;
			OpenMenu.Visibility = Visibility.Collapsed;
		}

		private void CloseMenu_Click(object sender, RoutedEventArgs e)
		{
			OpenMenu.Visibility = Visibility.Visible;
			CloseMenu.Visibility = Visibility.Collapsed;
		}
	}
}
