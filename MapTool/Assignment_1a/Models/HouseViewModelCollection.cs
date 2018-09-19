using Assignment_1a.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.Models
{
	public class HouseViewModelCollection : ObservableCollection<HouseRepresentationViewModel>
	{
		public event EventHandler OnCollectionItemEdited;

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			base.OnCollectionChanged(e);
		

				if (e.Action == NotifyCollectionChangedAction.Add)
				{
					var itemChanged = (HouseRepresentationViewModel)e.NewItems[0];
					itemChanged.OnEditHouseHandler += OnEditHouseEvent;
					itemChanged.OnDeleteHouseHandler += OnDeleteHouseEvent;
				}
			if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				var itemRemoved = (HouseRepresentationViewModel)e.OldItems[0];
				itemRemoved.OnEditHouseHandler -= OnEditHouseEvent;
			}
		}

		private void OnDeleteHouseEvent(object sender, EventArgs e)
		{
			var item = (HouseRepresentationViewModel)sender;
			item.OnDeleteHouseHandler -= OnDeleteHouseEvent;
			Remove(item);
		}

		private void OnEditHouseEvent(object sender, EventArgs e)
		{
			OnCollectionItemEdited.Invoke(sender, e);
			var item = (HouseRepresentationViewModel)sender;
		}
	}


}
