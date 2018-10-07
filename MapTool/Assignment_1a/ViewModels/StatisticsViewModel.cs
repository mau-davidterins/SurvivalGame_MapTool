using Assignment_1a.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.ViewModels
{
	class StatisticsViewModel : PageBase
	{

		List<LogFileModel> _logDataFiles;

		ObservableCollection<ChartModel> _averageStapleChart;
		ObservableCollection<ChartModel> _chart;



		public StatisticsViewModel()
		{
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
		}

		public ObservableCollection<ChartModel> PieChart { get { return _chart; } set { _chart = value; OnPropertyChanged(nameof(PieChart)); } }

		public ObservableCollection<ChartModel> AverageStapleChart { get { return _averageStapleChart; } set { _averageStapleChart = value; OnPropertyChanged(nameof(PieChart)); } }

		public void SetWorkingFiles(List<LogFileModel> newLogDataFiles)
		{
			_logDataFiles = newLogDataFiles;
			PieChart.Clear();
			//Chart.RemoveAt(1);
			//_chart = new ObservableCollection<ChartModel>
			//{
			//	new ChartModel("lol", 11),
			//	new ChartModel("boll", 15),
			//	new ChartModel("koll", 25)
			//};
			//Chart = _chart;
			//OnPropertyChanged(nameof(Chart));
		}

		
	}
}
