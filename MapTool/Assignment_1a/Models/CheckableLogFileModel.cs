using Assignment_1a.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.Models
{
    public class CheckableLogFileModel : ViewModelBase
    {
        bool _isChecked;
        public bool IsChecked { get { return _isChecked; }
            set { _isChecked = value; OnPropertyChanged(nameof(IsChecked)); } }
        string _logFileName;
        public string LogFileName { get { return _logFileName; } set { _logFileName = value; OnPropertyChanged(nameof(LogFileName)); } }

        public CheckableLogFileModel(string logFileName, bool isChecked)
        {
            _logFileName = logFileName;
            _isChecked = isChecked;
        }
    }
}
