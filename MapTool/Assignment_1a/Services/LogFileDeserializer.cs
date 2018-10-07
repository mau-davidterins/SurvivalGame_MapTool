using Assignment_1a.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assignment_1a.Services
{
	public class LogFileDeserializer
	{
		public List<LogFileModel> DeserializeFilesFromXML(List<FileListItemModel> filePaths)
		{
			List<LogFileModel> logFiles = new List<LogFileModel>();
			LogFileModel logFile = null;
			XmlSerializer serializer = new XmlSerializer(typeof(LogFileModel));
			foreach (FileListItemModel fileModel in filePaths)
			{
				using (StreamReader reader = new StreamReader(fileModel.LogFullPath))
				{
					logFile = (LogFileModel)serializer.Deserialize(reader);
					reader.Close();
				}
				logFiles.Add(logFile);
			}
			return logFiles;
		}

		//LogSession CombineFilesToSession()
		//{
		//	return null;
		//}
	}
}
