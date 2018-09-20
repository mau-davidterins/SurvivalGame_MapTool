using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment_1a.ViewModels
{
	public class MapObjectViewModel : ViewModelBase
	{
		double _width, _height;
		public double Width { get => _width; set { _width = value; }  }
		public double Height { get => _height; set { _height = value; } }

		double _posX = 20;
		public double PosX { get => _posX; set { _posX = value; OnPropertyChanged(nameof(PosX)); } }
		double _posY = 20;
		public double PosY { get => _posY; set { _posY = value; OnPropertyChanged(nameof(PosY)); Console.WriteLine(PosX + ", " + PosY); } }

		double _top = 20;
		public double Top { get => _top; set { _top = value; OnPropertyChanged(nameof(Top)); } }
		double _left = 20;
		public double Left { get => _left; set { _left = value; OnPropertyChanged(nameof(Left)); Console.WriteLine(Top + ", " + Left); } }

		public string Image { get; set; }
	}
}
