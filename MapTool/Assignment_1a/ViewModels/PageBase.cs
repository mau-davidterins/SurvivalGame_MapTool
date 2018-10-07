using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment_1a.ViewModels
{
	public abstract class PageBase : ViewModelBase
	{
		protected Visibility _showOrCollapse;
		public Visibility ShowOrCollapse { get { return _showOrCollapse; } set { _showOrCollapse = value; OnPropertyChanged(nameof(ShowOrCollapse)); } }
	}
}
