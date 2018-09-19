using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Assignment_1a.ViewModels
{
	public class MapViewModel : ViewModelBase
	{
		Vector3D _dimensions;
		public Vector3D Dimensions { get => _dimensions; set { _dimensions = value; OnPropertyChanged(nameof(Dimensions)); } }

		public ObservableCollection<MapObjectViewModel> Items { get; set; }

		float _top = 20;
		public float Top { get => _top; set { _top = value; OnPropertyChanged(nameof(Top)); } }
		float _left = 20;
		public float Left { get => _left; set { _left = value; OnPropertyChanged(nameof(Left)); Console.WriteLine(Top + ", " + Left); } }

		public MapViewModel()
		{
			Items = new ObservableCollection<MapObjectViewModel>();
			Items.Add(new MapObjectViewModel());
			Items.Add(new MapObjectViewModel());
		}

	}
}
