using Assignment_1a.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1a.ViewModels.ChartViewModels
{
	public class StapleChartViewModel : ChartBase
	{
		public StapleChartViewModel(List<LogFileModel> logDataFiles) : base(logDataFiles)
		{
			//chartDataModelCollection = chartDataModels;
			ChartType = "Staple chart";
		}
	}

	public class DoughnutChartViewModel : ChartBase
	{
		public DoughnutChartViewModel(List<LogFileModel> logDataFiles) : base(logDataFiles)
		{
			//chartDataModelCollection = chartDataModels;
			ChartType = "Doughnut chart";
		}
	}
}
