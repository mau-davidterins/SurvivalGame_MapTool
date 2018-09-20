using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment_1a.Models.Enums;
using System.Threading.Tasks;

namespace Assignment_1a.Models
{
	public abstract class BaseHouseModel
	{
		//Residential or commercial
		public string Category { get; set; }
		//Type of residential buildings - houses, villas, apartments, townhouses(row house)
		public int Width { get; set; }
		//Shops or warehouse, etc
		public int Height { get; set; }
		//Ownership, tenement or rental
		public string PrefabName { get; set; }
		//Object ID
		public string ID { get; }

		//Image
		public string Image { get; set; }

		public BaseHouseModel(string ID)
		{
			this.ID = ID;
		}
	}


}
