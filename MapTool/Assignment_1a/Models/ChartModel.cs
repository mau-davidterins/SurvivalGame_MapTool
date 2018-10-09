using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.Models
{
	public class ChartModel : INotifyPropertyChanged
	{
		public string Title { get; set; }
		public int Value { get; set; }

		public ChartModel(string title, int value)
		{
			Title = title;
			Value = value;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
