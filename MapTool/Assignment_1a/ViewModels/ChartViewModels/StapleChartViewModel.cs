using Assignment_1a.Models;
using Assignment_1a.Services;
using LiveCharts;
using LiveCharts.Wpf;
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


  public class LineChartViewModel : ChartBase
  {
    // public Func<double, string> YFormatter { get; set; }
    LineChartStatisticCalculator calculator;
    public SeriesCollection SeriesCollection { get; set; }
    public string[] TimeStamps { get; set; }
    public int[] ResourceStamps { get; set; }
    public string YAxis { get; set; }
    public string XAxis { get; set; }


    /// <summary>
    /// Create a resourceLineChart over timeintervals
    /// </summary>
    /// <param name="logDataFiles"></param>
    public LineChartViewModel(List<LogFileModel> logDataFiles, string resultType) : base(logDataFiles)
    {
      calculator = new LineChartStatisticCalculator();
      SeriesCollection = new SeriesCollection();
      ChartType = "Line chart";


      if (resultType == "Avg on stageChange")
      {
        TimeStamps = new[] { "Start", "Stage 1", "Stage 2", "Stage 3", "Stage 4" };
        ResourceStamps = new[] { 0, 5, 10, 15, 20, 25, 30, 35 };
        XAxis = "Stages";
        YAxis = "Minutes";

        var oilSeries = new LineSeries
        {
          Title = "Average time",
          Values = new ChartValues<int> { 0,
          calculator.GetAvgTimeToCheckPoint(logDataFiles, 1),
          calculator.GetAvgTimeToCheckPoint(logDataFiles, 2),
          calculator.GetAvgTimeToCheckPoint(logDataFiles, 3),
          calculator.GetAvgTimeToCheckPoint(logDataFiles, 4)},
        };

        SeriesCollection.Add(oilSeries);
      }
      else if (resultType == "Avg Resources over time")
      {
        XAxis = "Time";
        YAxis = "Resource value";
        TimeStamps = new[] {
          "0", "3 min", "6 min", "9 min", "12 min", "15 min", "18 min", "21", "24 min", "27 min", "30 min",
          "33 min", "36 min", "39 min","42", "45 min", "48 min", "51 min", "54 min", "57 min", "60 min" };
        ResourceStamps = new[] { 25, 50, 75, 100, 125, 150, 175, 200 };
        var tempSeriesCollection = new SeriesCollection();
        calculator.GetAvgResourcesOverTime(ref tempSeriesCollection, logDataFiles);
      
        SeriesCollection = tempSeriesCollection;

      }


    }
  }

}
