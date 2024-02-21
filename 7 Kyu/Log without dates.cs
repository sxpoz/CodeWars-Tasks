using System;
using System.Collections.Generic;
using System.Linq;

public static class Kata
{
  public static int CheckLogs(string[] dates)
  {
    if (dates.Length == 0) return 0;
    int day = 1;
    List<int> seconds = dates.Select(x => Convert.ToInt32(x.Split(':')[0]) * 3600
                                        + Convert.ToInt32(x.Split(':')[1]) * 60
                                        + Convert.ToInt32(x.Split(':')[2])).ToList();
    for (int i = 1; i < seconds.Count(); i++)
        if (seconds[i] <= seconds[i - 1]) 
          day++;
    return day;
  }
}
