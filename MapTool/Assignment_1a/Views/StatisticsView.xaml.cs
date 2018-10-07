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

namespace Assignment_1a.Views
{
	/// <summary>
	/// Interaction logic for StatisticsView.xaml
	/// </summary>
	public partial class StatisticsView : UserControl
	{
		public StatisticsView()
		{
			InitializeComponent();
			DataContextChanged += StatisticsView_DataContextChanged;
		}

		private void StatisticsView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var s = DataContext;
		}
	}
}
