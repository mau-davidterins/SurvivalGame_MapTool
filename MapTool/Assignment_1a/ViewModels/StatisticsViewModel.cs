using Assignment_1a.Models;
using Assignment_1a.Services;
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
		private readonly StatisticsCalculator _statCalculator;

		List<LogFileModel> _logDataFiles;

		ObservableCollection<ChartModel> _averageStapleChart;
		ObservableCollection<ChartModel> _chart;

		public StatisticsViewModel()
		{
			_statCalculator = new StatisticsCalculator();
			_chart = new ObservableCollection<ChartModel>
			{
				new ChartModel("titulo", 57),
				new ChartModel("test", 11),
				new ChartModel("lol", 15)
			};
			_averageStapleChart = new ObservableCollection<ChartModel>
			{
				new ChartModel("Food",32),
				new ChartModel("Oil",23),
				new ChartModel("Metal",52),
				new ChartModel("Population",3),
				new ChartModel("Water",11)
			};

			GetStatsCommand = new ActionCommand(GetStats);
		}
		public LogType SelectedLogType { get; set; }
		public string SelectedAllOrCustomPlayers { get; set; }
		public bool OnAllPlayers { get; set; }
		int _averageGameTime = 0;
		public int AverageGameTime { get { return _averageGameTime; } set { _averageGameTime = value; OnPropertyChanged(nameof(AverageGameTime)); } }

		public ICommand GetStatsCommand { get; }

		public ObservableCollection<ChartModel> PieChart { get { return _chart; } set { _chart = value; OnPropertyChanged(nameof(PieChart)); } }

		public ObservableCollection<ChartModel> AverageStapleChart { get { return _averageStapleChart; } set { _averageStapleChart = value; OnPropertyChanged(nameof(PieChart)); } }

		public void SetWorkingFiles(List<LogFileModel> newLogDataFiles)
		{
			_logDataFiles = newLogDataFiles;
		}

		void GetStats()
		{
			_statCalculator.StatParameters(LogType.Death, SelectedAllOrCustomPlayers, _logDataFiles);

			AverageGameTime = _statCalculator.GetAverageGameTime();

			//AverageStapleChart = _statCalculator.GetAverageResourceChart();

			Console.WriteLine("GetStats");
		}
		
	}
}
