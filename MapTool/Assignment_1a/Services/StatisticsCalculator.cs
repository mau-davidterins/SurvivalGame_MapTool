using Assignment_1a.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.Services
{
	public class StatisticsCalculator
	{
		LogType _logType;
		string _selectedAllOrCustomPlayers;

		List<LogFileModel> _totalLogFiles;
		List<GameSessionModel> _totalGameSessions;
		List<LogStatus> _totalStatuses;

		public int AveragePlayTime = 0;

		public StatisticsCalculator()
		{
			_totalGameSessions = new List<GameSessionModel>();
			_totalStatuses = new List<LogStatus>();
		}

		public void StatParameters(LogType logType, string selectedAllOrCustomPlayers, List<LogFileModel> logDataFiles)
		{
			_logType = logType;
			_selectedAllOrCustomPlayers = selectedAllOrCustomPlayers;
			_totalLogFiles = logDataFiles;

			if(_totalLogFiles != null)
			{
				RetrieveStatuses(RetrieveGameSessions(_totalLogFiles), logType);
			}		
		}

		List<GameSessionModel> RetrieveGameSessions(List<LogFileModel> logDataFiles)
		{
			foreach (LogFileModel logFileModel in logDataFiles)
			{
				_totalGameSessions.AddRange(logFileModel.Log);
			}
			return _totalGameSessions;
		}

		void RetrieveStatuses(List<GameSessionModel> gameSessions, LogType logType)
		{
			foreach (GameSessionModel gameSession in gameSessions)
			{
				if (logType == LogType.Regular)
				{
					_totalStatuses.AddRange(gameSession.GameSession);
				}
				else if (logType == LogType.Death)
				{//Find all statuses when the player has lost which will be the last status of that gameSession.
					_totalStatuses.Add(gameSession.GameSession[gameSession.GameSession.Count - 1]);
				}
			}
		}


		public int GetAverageGameTime()
		{
			int combinedTime = 0;
			int[] gameTimes = new int[_totalStatuses.Count];
			for(int i = 0; i< _totalStatuses.Count; i++)
			{
				int.TryParse(_totalStatuses[i].TimeString[0].ToString(), out gameTimes[i]);
				combinedTime += gameTimes[i];
			}
			int result = combinedTime / _totalStatuses.Count;
			
			return result;
		}

		public ObservableCollection<ChartModel> GetAverageResourceChart()
		{
			int _oil = 0, _wood = 0, _water = 0, _steel = 0, _population = 0;
			int allStatuses = _totalStatuses.Count;
			foreach (LogStatus status in _totalStatuses)
			{
				_oil += status.Oil;
				_wood += status.Wood;
				_water += status.Water;
				_steel += status.Steel;
				_population += status.Population;
			}
             _oil = _oil / allStatuses;
            _wood = _wood / allStatuses;
            _water = _water / allStatuses;
            _steel = _steel / allStatuses;
            _population = _population / allStatuses;

			ObservableCollection<ChartModel> temp = new ObservableCollection<ChartModel>
			{
				new ChartModel("Oil", _oil),
				new ChartModel("Wood", _wood),
				new ChartModel("Water", _water),
				new ChartModel("Steel", _steel),
				new ChartModel("Population", _population)
			};

			return temp;
		}
	}
}
