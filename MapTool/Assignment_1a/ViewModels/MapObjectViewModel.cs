using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.ViewModels
{
	public class MapObjectViewModel : ViewModelBase
	{
		public double Width { get => 20 ;  }
		public double Height { get => 20; }

		double _posX = 20;
		public double PosX { get => _posX; set { _posX = value; OnPropertyChanged(nameof(PosX)); } }
		double _posY = 20;
		public double PosY { get => _posY; set { _posY = value; OnPropertyChanged(nameof(PosY)); Console.WriteLine(PosX + ", " + PosY); } }

		double _top = 20;
		public double Top { get => _top; set { _top = value; OnPropertyChanged(nameof(Top)); } }
		double _left = 20;
		public double Left { get => _left; set { _left = value; OnPropertyChanged(nameof(Left)); Console.WriteLine(Top + ", " + Left); } }

	}
}
