using Assignment_1a.Models;
using Assignment_1a.Services;
using Assignment_1a.ViewModels.ChartViewModels;
using Assignment_1a.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assignment_1a.ViewModels
{
	class StatisticsViewModel : PageBase
	{
		List<LogFileModel> _logDataFiles;

		public StatisticsViewModel()
		{

			chartCollection = new ChartCollection();
			_selectedLogs = new ObservableCollection<string>();

			chartCollection.OnItemEdit += ChartCollection_OnItemEdit;
			
			CreateChartCommand = new ActionCommand(CreateChart);
		}

		public LogType SelectedLogType { get; set; }

		ChartCollection chartCollection;
		public ChartCollection ChartCollection { get { return chartCollection; } }

		ObservableCollection<string> _selectedLogs;
		public ObservableCollection<string> SelectedLogs { get { return _selectedLogs; } set { _selectedLogs = value;OnPropertyChanged(nameof(SelectedLogs)); } }

		bool _hasLogData;
		public bool HasLogData { get { return _hasLogData; } set { _hasLogData = value; OnPropertyChanged(nameof(HasLogData)); } }

		string _selectedChartType;
		public string SelectedChartType { get { return _selectedChartType; } set { _selectedChartType = value; OnPropertyChanged(nameof(SelectedChartType)); } }

		string _newChartTitle;
		public string NewChartTitle { get { return _newChartTitle; } set { _newChartTitle = value; OnPropertyChanged(nameof(NewChartTitle)); } }

		string _newChartSubTitle;
		public string NewChartSubTitle { get { return _newChartSubTitle; } set { _newChartSubTitle = value; OnPropertyChanged(nameof(NewChartSubTitle)); } }

		string _totalLogs = "0";
		public string TotalLogs { get { return _totalLogs; } set { _totalLogs = value; OnPropertyChanged(nameof(TotalLogs)); } }

		string _totalGamesPlayed = "0";
		public string TotalGamesPlayed { get { return _totalGamesPlayed; } set { _totalGamesPlayed = value; OnPropertyChanged(nameof(TotalGamesPlayed)); } }



		public string SelectedAllOrCustomPlayers { get; set; }
		public bool OnAllPlayers { get; set; }
		int _averageGameTime = 0;
		public int AverageGameTime { get { return _averageGameTime; } set { _averageGameTime = value; OnPropertyChanged(nameof(AverageGameTime)); } }

		public ICommand CreateChartCommand { get; }

		public void SetWorkingFiles(List<LogFileModel> newLogDataFiles)
		{
			_logDataFiles = newLogDataFiles;
			TotalLogs = newLogDataFiles.Count.ToString();
			HasLogData = true;
			int totalGames = 0;
			foreach (LogFileModel logFile in newLogDataFiles)
			{
				SelectedLogs.Add(logFile.FileName);
			  totalGames += logFile.Log.Count;
			}
			TotalGamesPlayed = totalGames.ToString();
		}

		private void ChartCollection_OnItemEdit(object sender, ChartBase e)
		{
			NewChartTitle = e.Title;
			NewChartSubTitle = e.SubTitle;
			ChartCollection.Replace(e, new DoughnutChartViewModel(_logDataFiles));
		}

		void CreateChart()
		{

			if (SelectedChartType == "System.Windows.Controls.ComboBoxItem: Staple chart")
			{
				ChartCollection.Add(new StapleChartViewModel(_logDataFiles)
				{
					Title = NewChartTitle,
					SubTitle = NewChartSubTitle,
				});
			}
			else if (SelectedChartType == "System.Windows.Controls.ComboBoxItem: Doughnut chart")
			{
				ChartCollection.Add(new DoughnutChartViewModel(_logDataFiles)
				{
					Title = NewChartTitle,
					SubTitle = NewChartSubTitle,
				});
			}

			NewChartTitle = "";
			NewChartSubTitle = "";

		}

	}
}
