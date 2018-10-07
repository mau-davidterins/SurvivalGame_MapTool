using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Assignment_1a.Models;
using Assignment_1a.Models.Enums;
using Assignment_1a.ViewModels.Commands;
using System.Windows.Input;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows;

namespace Assignment_1a.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		Dictionary<string, MainMenuItemModel> _slideMenuItems;
		public Dictionary<string, MainMenuItemModel> SlideMenuItems { get { return _slideMenuItems; } set { _slideMenuItems = value; } }
		MainMenuItemModel _activeMenuItem;
		public MainMenuItemModel ActiveMenuItem { get { return _activeMenuItem; } set { _activeMenuItem = value; OnPropertyChanged(nameof(ActiveMenuItem)); } }

		public MainMenuItemModel ImportFileMenuItem { get { return _slideMenuItems["ImportFileMenu"]; } }
		public MainMenuItemModel ChartMenuItem { get { return _slideMenuItems["ChartMenu"]; } }

		public MainWindowViewModel()
		{
			_slideMenuItems = new Dictionary<string, MainMenuItemModel>();
			var importFileVM = new ImportFileViewModel();

			var importFileMenuItem = new MainMenuItemModel("ImportFileMenu", "FileImport", ChangeMenuView, importFileVM);
			var chartMenuItem = new MainMenuItemModel("ChartMenu", "ChartBar", ChangeMenuView, new StatisticsViewModel());

			_slideMenuItems.Add(importFileMenuItem.Name, importFileMenuItem);
			_slideMenuItems.Add(chartMenuItem.Name, chartMenuItem);

			currentSelectedMenuItem = "ChartMenu";
			_slideMenuItems[currentSelectedMenuItem].Select();


			importFileVM.OnXMLImportedAndDeserialized += HandleXMLFileDeserialized;
			
		}

		private void HandleXMLFileDeserialized(object sender, List<LogFileModel> logFiles)
		{
			var statsVM = (StatisticsViewModel)ChartMenuItem.CorrespondingPage;
			statsVM.SetWorkingFiles(logFiles);
			ChangeMenuView("ChartMenu");
		}

		string currentSelectedMenuItem;

		void ChangeMenuView(string itemName)
		{
			Console.WriteLine("CLick!");
			if (itemName != currentSelectedMenuItem)
			{
				if(itemName == "ChartMenu")
				{
					var s = (StatisticsViewModel)_slideMenuItems[itemName].CorrespondingPage;
					var kl = s.PieChart;
				}
				Console.WriteLine("Collapsing " + currentSelectedMenuItem);
				_slideMenuItems[currentSelectedMenuItem].Deselect();

				Console.WriteLine("Showing " + itemName);
				_slideMenuItems[itemName].Select();
				currentSelectedMenuItem = itemName;

			}
		}

		void AddImage()
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			Nullable<bool> result = fileDialog.ShowDialog();
			if (result == true)
			{

			}
		}



	}
}
