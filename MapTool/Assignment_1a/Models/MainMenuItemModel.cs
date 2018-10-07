using Assignment_1a.ViewModels;
using Assignment_1a.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Assignment_1a.Models
{
	public class MainMenuItemModel : ViewModelBase
	{
		public string Name { get; private set; }
		public string IconKind { get; private set; }
		public ICommand MenuItemCommand { get; private set; }

		PageBase _correspondingPage;
		public PageBase CorrespondingPage { get { return _correspondingPage; } }

		public MainMenuItemModel(string name, string iconKind, Action<string> action, PageBase correspondingPage)
		{
			Name = name;
			IconKind = iconKind;
			MenuItemCommand = new RelayCommand(action);
			_correspondingPage = correspondingPage;
			Deselect();		
		}

		public void Select()
		{
			_correspondingPage.ShowOrCollapse = Visibility.Visible;
		}

		public void Deselect()
		{
			_correspondingPage.ShowOrCollapse = Visibility.Collapsed;
		}

	


	}
}
