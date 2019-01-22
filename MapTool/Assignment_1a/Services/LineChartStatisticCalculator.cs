using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Assignment_1a.Services
{
  public class LineChartStatisticCalculator
  {

    public int GetAvgTimeToCheckPoint(List<LogFileModel> logDataFiles, int checkPoint)
    {
      List<string> temp = new List<string>();

      foreach (LogFileModel file in logDataFiles)
      {
        foreach (GameSessionModel Session in file.Log)
        {
          temp.Add((from status in Session.StageChangeLogs
                    where status.CheckPoint == checkPoint
                    select status.TimeString).FirstOrDefault());
        }
      }

      float totalTimeSpent = 0;
      int avgTimeSpent = 0;

      List<float> times = new List<float>();
      float result = 0;
      int lol = 0;
      foreach (var t in temp)
      {
        var s = t;
        int.TryParse(t, out lol);
        float.TryParse(t, out result);
        times.Add(result);

        totalTimeSpent += result;
      }

      avgTimeSpent = ((int)(totalTimeSpent / times.Count)) / 60;

      return avgTimeSpent;
    }




    public ResourceValueCluster[] GetAvgResourcesOverTime(ref SeriesCollection refCollection, List<LogFileModel> logDataFiles)
    {
      List<List<LogStatus>> orderedStatusesOnGameTimeLength = new List<List<LogStatus>>();
      foreach (LogFileModel file in logDataFiles)
      {
        foreach (GameSessionModel Session in file.Log)
        {
          orderedStatusesOnGameTimeLength.Add(Session.RegularIntervalLogs);
        }
      }

      int longestSession = 0;
      orderedStatusesOnGameTimeLength = orderedStatusesOnGameTimeLength.OrderBy(o => o.Count).ToList();

      longestSession = orderedStatusesOnGameTimeLength[orderedStatusesOnGameTimeLength.Count - 1].Count;
      ResourceValueCluster[] AvgResourceValuesPerInterval = new ResourceValueCluster[longestSession];

      int index = 0;
      while (index < longestSession)
      {
        int logsWithSameTimeStamp = 0;
        int totalOil = 0, totalWater = 0, totalSteel = 0, totalPopulation = 0, totalWood = 0, totalCleanWater = 0, totalCleanFood = 0, totalFood = 0;
        for (int i = 0; i < orderedStatusesOnGameTimeLength.Count; i++)
        {
          if (index < orderedStatusesOnGameTimeLength[i].Count)
          {
            logsWithSameTimeStamp++;
            totalOil += orderedStatusesOnGameTimeLength[i][index].Oil;
            totalWater += orderedStatusesOnGameTimeLength[i][index].Water;
            totalSteel += orderedStatusesOnGameTimeLength[i][index].Steel;
            totalPopulation += orderedStatusesOnGameTimeLength[i][index].Population;
            totalWood += orderedStatusesOnGameTimeLength[i][index].Wood;
            totalCleanWater += orderedStatusesOnGameTimeLength[i][index].CleanWater;
            totalCleanFood += orderedStatusesOnGameTimeLength[i][index].CleanFood;
            totalFood += orderedStatusesOnGameTimeLength[i][index].Food;
          }
        }

        AvgResourceValuesPerInterval[index] = new ResourceValueCluster(
          totalOil / logsWithSameTimeStamp,
          totalWater / logsWithSameTimeStamp,
          totalSteel / logsWithSameTimeStamp,
          totalPopulation / logsWithSameTimeStamp,
          totalWood / logsWithSameTimeStamp,
          totalCleanWater / logsWithSameTimeStamp,
          totalCleanFood / logsWithSameTimeStamp,
          totalFood / logsWithSameTimeStamp);
        

        index++;
      }

      var woodSeries = new LineSeries();
      woodSeries.Values = new ChartValues<int>();
      woodSeries.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 221, 149, 82));
      woodSeries.Title = "Wood";

      var populationSeries = new LineSeries();
      populationSeries.Values = new ChartValues<int>();
      populationSeries.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 255));
      populationSeries.Title = "Population";

      var steelSeries = new LineSeries();
      steelSeries.Values = new ChartValues<int>();
      steelSeries.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 70, 69, 70));
      steelSeries.Title = "Steel";

      var waterSeries = new LineSeries();
      waterSeries.Values = new ChartValues<int>();
      waterSeries.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 44, 80, 91));
      waterSeries.Title = "Water";

      var oilSeries = new LineSeries();
      oilSeries.Values = new ChartValues<int>();
      oilSeries.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 0, 0));
      oilSeries.Title = "Oil";

      var foodSeries = new LineSeries();
      foodSeries.Values = new ChartValues<int>();
      foodSeries.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 114, 47, 56));
      foodSeries.Title = "Food";

      var cleanFoodSeries = new LineSeries();
      cleanFoodSeries.Values = new ChartValues<int>();
      cleanFoodSeries.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 240, 0, 46));
      cleanFoodSeries.Title = "Food(clean)";

      var cleanWaterSeries = new LineSeries();
      cleanWaterSeries.Values = new ChartValues<int>();
      cleanWaterSeries.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 95, 255));
      cleanWaterSeries.Title = "Water(clean)";


      for (int i = 0; i < AvgResourceValuesPerInterval.Length; i++)
      {
        woodSeries.Values.Add(AvgResourceValuesPerInterval[i].WoodValue);
        populationSeries.Values.Add(AvgResourceValuesPerInterval[i].PopulationValue);
        steelSeries.Values.Add(AvgResourceValuesPerInterval[i].SteelValue);
        waterSeries.Values.Add(AvgResourceValuesPerInterval[i].WaterValue);
        cleanWaterSeries.Values.Add(AvgResourceValuesPerInterval[i].CleanWaterValue);
        foodSeries.Values.Add(AvgResourceValuesPerInterval[i].FoodValue);
        cleanFoodSeries.Values.Add(AvgResourceValuesPerInterval[i].CleanFoodValue);
        oilSeries.Values.Add(AvgResourceValuesPerInterval[i].OilValue);
      }

      refCollection.Add(woodSeries);
      refCollection.Add(populationSeries);
      refCollection.Add(steelSeries);
      refCollection.Add(waterSeries);
      refCollection.Add(cleanWaterSeries);
      refCollection.Add(foodSeries);
      refCollection.Add(cleanFoodSeries);
      refCollection.Add(oilSeries);
       
      return AvgResourceValuesPerInterval;
    }
  }

  public struct ResourceValueCluster
  {
    public int OilValue { get; }
    public int WaterValue { get; }
    public int SteelValue { get; }
    public int PopulationValue { get; }
    public int WoodValue { get; }
    public int CleanFoodValue { get; }
    public int FoodValue { get; }
    public int CleanWaterValue { get; }

    public ResourceValueCluster(int oil, int water, int steel, int population, int wood, int cleanWater, int cleanFood, int food)
    {
      OilValue = oil;
      WaterValue = water;
      SteelValue = steel;
      PopulationValue = population;
      WoodValue = wood;
      FoodValue = food;
      CleanFoodValue = cleanFood;
      CleanWaterValue = cleanWater;
    }
  }



}
