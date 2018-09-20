using Assignment_1a.Models;
using Assignment_1a.Models.Enums;
using Assignment_1a.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assignment_1a.ViewModels
{
	public class HouseRepresentationViewModel : ViewModelBase
	{
		public event EventHandler OnEditHouseHandler;
		public event EventHandler OnDeleteHouseHandler;
		public event EventHandler OnAddObjectToMapHandler;

		public string Category
		{
			get => HouseBase.Category; private set
			{
				HouseBase.Category = value;
				OnPropertyChanged(nameof(Category));
			}
		}

		public int Width
		{
			get => HouseBase.Width; private set
			{
				HouseBase.Width = value;
				OnPropertyChanged(nameof(Width));
			}
		}

		public int Height
		{
			get => HouseBase.Height; private set
			{
				HouseBase.Height = value;
				OnPropertyChanged(nameof(Height));
			}
		}

		public string PrefabName
		{
			get => HouseBase.PrefabName; private set
			{
				HouseBase.PrefabName = value; OnPropertyChanged(nameof(PrefabName));
			}
		}

		public string ImageFilePath { get { return HouseBase.Image; } private set { HouseBase.Image = value; OnPropertyChanged(nameof(ImageFilePath)); } }

		string _id;
		public string ID { get => _id; set { _id = value; OnPropertyChanged(nameof(ID)); } }

		public BaseHouseModel HouseBase { get; set; }

		public ICommand DeleteCommand { get; set; }
		public ICommand EditCommand { get; set; }
		public ICommand AddObjectCommand { get; set; }

		bool _editMode;
		public bool EditMode { get { return _editMode; } set { _editMode = value; OnPropertyChanged(nameof(EditMode)); } }


		public HouseRepresentationViewModel()
		{
			DeleteCommand = new ActionCommand(Delete);
			EditCommand = new ActionCommand(Edit);
			AddObjectCommand = new ActionCommand(AddObjectToMap);
		}

		public void EditValues(string id, string prefabName, int width, int height, string image, string category)
		{

			ImageFilePath = image;
			PrefabName = prefabName;
			Width = width;
			Height = height;
			Category = category;
		}

		public MapObjectViewModel ConvertToMapObject()
		{
			var mapObject = new MapObjectViewModel();
			mapObject.PosX = 0;
			mapObject.PosY = 0;
			mapObject.Width = Width;
			mapObject.Height = Height;
			mapObject.Image = ImageFilePath;
			return mapObject;
		}

		void Delete()
		{
			OnDeleteHouseHandler.Invoke(this, EventArgs.Empty);
		}
		void Edit()
		{
			EditMode = true;
			OnEditHouseHandler.Invoke(this, EventArgs.Empty);
		}
		void AddObjectToMap()
		{
			OnAddObjectToMapHandler.Invoke(this, EventArgs.Empty);
		}





	}
}
