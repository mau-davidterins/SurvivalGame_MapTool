using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

/// <summary>
/// The logmodel basically the whole Logfile which contains multiple
///  gameSessions
/// </summary>
[Serializable]
public class LogFileModel
{
	public string LogFileName;
	public int TotalGameSessions;
	public List<GameSessionModel> Log = new List<GameSessionModel>();

	public LogFileModel() { }

	public void AddGameSessionToFile(GameSessionModel gameSession)
	{
		Log.Add(gameSession);
	}

	public void SerializeLogToXML(string folderPath)
	{
		using (var stream = new FileStream(folderPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(LogFileModel));
			serializer.Serialize(stream, this);
		}
	}
}

/// <summary>
/// GameSessionModel is a single gameplay session with statuses regularly
///  updated and creating during gameplay
/// </summary>
[Serializable]
public class GameSessionModel
{

  public int TotalCheckPoints { get; set; }
	public string PlayerName { get; set; }
	public int SessionNumber { get; set; }
	public string SessionDate { get; set; }
  public List<LogStatus> StageChangeLogs = new List<LogStatus>();
  public List<LogStatus> RegularIntervalLogs = new List<LogStatus>();

 // public void NewLogStatus(LogStatus status)
	//{
	//	GameSession.Add(status);
	//}
}

