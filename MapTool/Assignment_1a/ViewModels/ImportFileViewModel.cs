using Assignment_1a.Models;
using Assignment_1a.Services;
using Assignment_1a.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Assignment_1a.ViewModels
{
	public class ImportFileViewModel : PageBase
	{
		LogFileDeserializer logFileDeserializer;

		public ImportFileViewModel()
		{
			logFileDeserializer = new LogFileDeserializer();
			_selectedFilePaths = new ObservableCollection<FileListItemModel>();
			_selectedFilePaths.Add(new FileListItemModel("test", DeleteWorkingLogFile));
			OpenFileCommand = new ActionCommand(OpenFileExplorer);
		}


		public event EventHandler<List<LogFileModel>> OnXMLImportedAndDeserialized;

		ObservableCollection<FileListItemModel> _selectedFilePaths;
		public ObservableCollection<FileListItemModel> SelectedFilePaths { get { return _selectedFilePaths; }  set { _selectedFilePaths = value; OnPropertyChanged(nameof(SelectedFilePaths)); } }

		public ICommand OpenFileCommand { get; }

		void OpenFileExplorer()
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Multiselect = true;
			var result = fileDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				for (int i = 0; i < fileDialog.FileNames.Length; i++)
				{
					string filename = fileDialog.FileNames[i];
					SelectedFilePaths.Add(new FileListItemModel(filename, DeleteWorkingLogFile));
				}
				List<LogFileModel> newFileList = logFileDeserializer.DeserializeFilesFromXML(SelectedFilePaths.ToList());
				OnXMLImportedAndDeserialized?.Invoke(this, newFileList);

				OnPropertyChanged(nameof(SelectedFilePaths));
			}
		}

		void DeleteWorkingLogFile(string fileName)
		{
			var itemToRemove = SelectedFilePaths.Where(o => o.FileName == fileName).ToList();
			SelectedFilePaths.Remove(itemToRemove[0]);
			OnPropertyChanged(nameof(SelectedFilePaths));
		}

		public void FilePaths()
		{

		}
	}
}
