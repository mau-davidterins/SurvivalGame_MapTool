using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.Models.Global
{

	public static class ChartType
	{
		public static string Doughnut = "Doughnut chart";
		public static string Staple = "Staple chart";
		public static string Pie = "Pie chart";
    public static string Line = "Line chart";



    public static Dictionary<string, List<string>> ChartTypes = new Dictionary<string, List<string>>()
    {
      {Doughnut, new List<string>() },
      {Staple, new List<string>() },
      {Pie, new List<string>() },
      {Line, new List<string>()
      {
        "Avg on stageChange",
        "Avg Resources over time"
      } },

    };

    public static List<string> TypeList = new List<string>
		{
			Doughnut, Staple, Pie, Line
		};

	}
}
