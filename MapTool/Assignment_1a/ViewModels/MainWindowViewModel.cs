using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Assignment_1a.Models;
using Assignment_1a.Models.Enums;
using Assignment_1a.ViewModels.Commands;
using System.Windows.Input;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace Assignment_1a.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		string _category;
		public string Category
		{
			get => _category;
			set
			{
				_category = Helper.ConvertComobboxItemTotext(value);
				OnPropertyChanged(nameof(Category));
			}
		}

		int _height;
		public int Heigth
		{
			get => _height;
			set
			{
				_height = value;
				OnPropertyChanged(nameof(Heigth));
			}
		}

		int _width;
		public int Width
		{
			get => _width;
			set
			{
				_width = value;
				OnPropertyChanged(nameof(Width));
			}
		}

		string __prefabName;
		public string PrefabName
		{
			get => __prefabName;
			set
			{
				__prefabName = value;
				OnPropertyChanged(nameof(PrefabName));
			}
		}

		string _id;
		public string ID
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged(nameof(ID));
			}
		}

		string _searchFilter;
		public string SearchFilter
		{
			get => _searchFilter;
			set
			{
				_searchFilter = value;
				houseCollection.View.Refresh();
				OnPropertyChanged(nameof(SearchFilter));
			}
		}

		string _imageFilePath;
		public string ImageFilePath { get { return _imageFilePath; } set { _imageFilePath = value; OnPropertyChanged(nameof(ImageFilePath)); } }
		string _city;
		public string City { get { return _city; } set { _city = value; OnPropertyChanged(nameof(City)); } }
		int? _zip;
		public int? Zip { get { return _zip; } set { _zip = value; OnPropertyChanged(nameof(Zip)); } }
		Country _country;
		public Country Country_ { get { return _country; } set { _country = value; OnPropertyChanged(nameof(Country_)); } }
		string _street;
		public string Street { get { return _street; } set { _street = value; OnPropertyChanged(nameof(Street)); } }
		public List<Country> Countries { get { return Enum.GetValues(typeof(Country)).Cast<Country>().ToList(); } }


		HouseRepresentationViewModel _houseViewModel;
		public HouseRepresentationViewModel HouseViewModel
		{
			get => _houseViewModel;
			set
			{
				_houseViewModel = value;
				OnPropertyChanged(nameof(HouseViewModel));
			}
		}

		BaseHouseModel _selectedHouse;
		public BaseHouseModel SelectedHouse
		{
			get => _selectedHouse;
			set
			{
				_selectedHouse = value;
				Console.WriteLine(_selectedHouse.ID);
				OnPropertyChanged(nameof(SelectedHouse));
			}
		}

		MapViewModel _mapViewModel;
		public MapViewModel MapViewModel { get { return _mapViewModel; } }

		private CollectionViewSource houseCollection;

		public MainWindowViewModel()
		{
			_mapViewModel = new MapViewModel();

			houses = new HouseViewModelCollection();
			HouseRepresentationViewModel h = new HouseRepresentationViewModel();
			h.HouseBase = new House("ID_123")
			{

				Category = "Building",
				Width = 40,
				Height = 40,
				PrefabName = "TestObject"
			};

			houses.Add(h);

			houseCollection = new CollectionViewSource();
			houseCollection.Source = houses;

			houses.OnCollectionItemEdited += Houses_OnCollectionItemEdited;
			houses.OnAddedObjectToMap += Houses_OnAddedObjectToMap;
			houseCollection.Filter += usersCollection_Filter;
			AddImageCommand = new ActionCommand(AddImage);
			AddHouseCommand = new ActionCommand(AddHouse);
			FinishEditCommand = new ActionCommand(FinishEdit);
		}

		private void Houses_OnAddedObjectToMap(object sender, EventArgs e)
		{
			var mapObject = (HouseRepresentationViewModel)sender;
			MapViewModel.Items.Add(mapObject.ConvertToMapObject());
		}

		private void Houses_OnCollectionItemEdited(object sender, EventArgs e)
		{
			var itemToEdit = (HouseRepresentationViewModel)sender;
			HouseViewModel = itemToEdit;
			ID = itemToEdit.HouseBase.ID;
			Category = itemToEdit.Category;
			Width = itemToEdit.Width;
			PrefabName = itemToEdit.PrefabName;
			Heigth = itemToEdit.Height;
		}

		List<string> searchWords = new List<string>();
		private void usersCollection_Filter(object sender, FilterEventArgs e)
		{

			if (string.IsNullOrEmpty(_searchFilter))
			{
				e.Accepted = true;
				return;
			}
			string[] words = _searchFilter.Split(' ');



			var viewModelItem = e.Item as HouseRepresentationViewModel;
			string totalItemString = viewModelItem.Category +
								viewModelItem.Height + viewModelItem.ID +
								viewModelItem.PrefabName + viewModelItem.Width;
			foreach (var word in words)
			{
				Console.WriteLine(word);
				if (totalItemString.ToUpper().Contains(word.ToUpper()) && word != string.Empty)
				{
					e.Accepted = true;
				
				}
				else
				{
					e.Accepted = false;
				}
			}
		
		}

		public ICommand FinishEditCommand { get; set; }
		public ICommand AddHouseCommand { get; set; }
		public ICommand AddImageCommand { get; set; }

		public ICollectionView CollectionView { get => houseCollection.View; }

		private HouseViewModelCollection houses;
		public HouseViewModelCollection Houses
		{
			get => houses;
			set => houses = value;
		}

		void AddImage()
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			Nullable<bool> result = fileDialog.ShowDialog();
			if (result == true)
			{
				ImageFilePath = fileDialog.FileName;
			}
		}
		void FinishEdit()
		{
			Console.WriteLine(HouseViewModel.HouseBase.Category);
			HouseViewModel.EditValues(_id, __prefabName, _height, _width, _imageFilePath, _category);
			HouseViewModel.EditMode = false;
		}

		void AddHouse()
		{
			var h = new HouseRepresentationViewModel();
			h.HouseBase = new House(_id)
			{

				Image = _imageFilePath,
				Category = _category,
				Height = _height,
				Width = _width,
				PrefabName = __prefabName,
			};
			Houses.Add(h);
		}
	}
}
