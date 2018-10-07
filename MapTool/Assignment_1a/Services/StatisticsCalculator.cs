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
			RetrieveStatuses(RetrieveGameSessions(_totalLogFiles), logType);

			GetAverageGameTime();
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
			return null;
		}
	}
}
