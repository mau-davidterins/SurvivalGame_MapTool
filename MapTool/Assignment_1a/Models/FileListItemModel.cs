using Assignment_1a.ViewModels;
using Assignment_1a.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assignment_1a.Models
{
	public class FileListItemModel
	{
		public string FileName { get { return Path.GetFileName(LogFullPath); } }
		public string LogFullPath { get; set; }
		public ICommand DeleteItemCommand { get; private set; }

		public FileListItemModel(string logFullPath, Action<string> action)
		{
			LogFullPath = logFullPath;
			DeleteItemCommand = new RelayCommand(action);
		}
	}
}
