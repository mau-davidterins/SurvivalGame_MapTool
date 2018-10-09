using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.Models.Global
{
	public static class Statics
	{

	}

	public static class ChartType
	{
		public static string Doughnut = "Doughnut chart";
		public static string Staple = "Staple chart";
		public static string Pie = "Pie chart";

		public static List<string> TypeList = new List<string>
		{
			Doughnut, Staple, Pie
		};

	}
}
