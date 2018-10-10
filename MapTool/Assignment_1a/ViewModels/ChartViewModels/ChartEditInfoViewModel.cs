using Assignment_1a.Services;
using Assignment_1a.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assignment_1a.ViewModels.ChartViewModels
{
	public class ChartEditInfoViewModel : ViewModelBase
	{
		ChartBase _parent;
		List<LogFileModel> _logDataFiles;
		StatisticsCalculator _statisticsCalculator;

		public ChartEditInfoViewModel(ChartBase parent, ref List<LogFileModel> logDataFiles, ref StatisticsCalculator statsCalculator)
		{
			_parent = parent;
			_logDataFiles = logDataFiles;
			_statisticsCalculator = statsCalculator;
			ApplyChangesCommand = new ActionCommand(ApplyChanges);
		}

		public ICommand ApplyChangesCommand { get; }

		string _selectedCheckPoint = "GameOver";
		public string SelectedCheckPoint { get { return _selectedCheckPoint; } set { _selectedCheckPoint = value.Substring(value.IndexOf(':') + 1); _parent.CheckPoint = _selectedCheckPoint; OnPropertyChanged(nameof(SelectedCheckPoint)); } }

		public List<string> TotalCheckPoints { get { return GetTotalCheckPointRange(); } }

		public List<string> ContainingLogFileNames { get { return GetLogFileNames(); } }

		List<string> GetTotalCheckPointRange()
		{
			List<string> range = new List<string>();

			int maxCheckPoint = 0;
			foreach(LogFileModel file in _logDataFiles)
			{
				foreach(GameSessionModel gameSession in file.Log)
				{
					if(gameSession.TotalCheckPoints > maxCheckPoint)
					{
						maxCheckPoint = gameSession.TotalCheckPoints;
					}
				}
			}
			for (int i = 1; i <= maxCheckPoint; i++)
			{
				range.Add(i.ToString());
			}
			range.Add("GameOver");
			return range;
		}

		List<string> GetLogFileNames()
		{
			List<string> nameList = new List<string>();
			foreach (LogFileModel file in _logDataFiles)
			{
				nameList.Add(file.LogFileName);
			}
			return nameList;
		}

	  void ApplyChanges()
		{
			//Recalculate based on SelectedCheckPoint.
			Console.WriteLine("Apply");
		
		
			_parent.ChartDataModelCollection = _statisticsCalculator.GetAverageResourceChartAtCheckPoint(_selectedCheckPoint);
		}


	}
}
