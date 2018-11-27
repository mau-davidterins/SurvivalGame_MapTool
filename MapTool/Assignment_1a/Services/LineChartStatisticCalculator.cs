using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      foreach (var t in temp)
      {
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
        int totalOil = 0, totalWater = 0, totalSteel = 0, totalPopulation = 0, totalWood = 0;
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
          }
        }

        AvgResourceValuesPerInterval[index] = new ResourceValueCluster(
          totalOil / logsWithSameTimeStamp,
          totalWater / logsWithSameTimeStamp,
          totalSteel / logsWithSameTimeStamp,
          totalPopulation / logsWithSameTimeStamp,
          totalWood / logsWithSameTimeStamp);

        index++;
      }

      var woodSeries = new LineSeries();
      woodSeries.Values = new ChartValues<int>();
      woodSeries.Title = "Wood";
      var populationSeries = new LineSeries();
      populationSeries.Values = new ChartValues<int>();
      populationSeries.Title = "Population";
      var steelSeries = new LineSeries();
      steelSeries.Values = new ChartValues<int>();
      steelSeries.Title = "Steel";
      var waterSeries = new LineSeries();
      waterSeries.Values = new ChartValues<int>();
      waterSeries.Title = "Water";
      var oilSeries = new LineSeries();
      oilSeries.Values = new ChartValues<int>();
      oilSeries.Title = "Oil";

      for (int i = 0; i < AvgResourceValuesPerInterval.Length; i++)
      {
        woodSeries.Values.Add(AvgResourceValuesPerInterval[i].WoodValue);
        populationSeries.Values.Add(AvgResourceValuesPerInterval[i].PopulationValue);
        steelSeries.Values.Add(AvgResourceValuesPerInterval[i].SteelValue);
        waterSeries.Values.Add(AvgResourceValuesPerInterval[i].WaterValue);
        oilSeries.Values.Add(AvgResourceValuesPerInterval[i].OilValue);
      }

      refCollection.Add(woodSeries);
      refCollection.Add(populationSeries);
      refCollection.Add(steelSeries);
      refCollection.Add(waterSeries);
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

    public ResourceValueCluster(int oil, int water, int steel, int population, int wood)
    {
      OilValue = oil;
      WaterValue = water;
      SteelValue = steel;
      PopulationValue = population;
      WoodValue = wood;
    }
  }



}
