using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.ViewModels.ChartViewModels
{
	public class ChartEditInfoViewModel : ViewModelBase
	{
		ChartBase _parent;
		List<LogFileModel> _logDataFiles;

		public ChartEditInfoViewModel(ref List<LogFileModel> logDataFiles)
		{
			_logDataFiles = logDataFiles;
		}

		public List<string> ContainingLogFileNames { get { return GetLogFileNames(); } }

		List<string> GetLogFileNames()
		{
			List<string> nameList = new List<string>();
			foreach(LogFileModel file in _logDataFiles)
			{
				nameList.Add(file.LogFileName);
			}
			return nameList;
		}


	}
}
