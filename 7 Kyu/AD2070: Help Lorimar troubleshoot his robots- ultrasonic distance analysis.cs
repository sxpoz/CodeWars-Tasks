using System;
using System.Linq;

public class Kata
{
  public static double[] SensorAnalysis(object[][] sensorData)
  {
    double mean = 0;
    double deviation = 0;
    foreach (var obj in sensorData)
    {
      mean += (double)obj.First(x => x.GetType() == 0d.GetType()) / sensorData.Length;
    }
    foreach (var obj in sensorData)
    {
      deviation += Math.Pow((double)obj.First(x => x.GetType() == 0d.GetType()) - mean, 2);
    }
    return new double[2] { Math.Round(mean, 4), Math.Round(Math.Sqrt(deviation / (sensorData.Length - 1) ), 4)};
  }
}
