using System;
using System.Collections.Generic;
using System.Linq;

public class Quest
{
    public static int[] SolomonsQuest(int[][] ar)
    {
        int currentLayer = 0;
        int x = 0, y = 0;
        List<int> directions = new List<int>() { 1, 1, -1, -1 }; //indexes: 0 = North, 1 = East, 2 = South, 3 = West
        foreach (var a in ar)
        {
            currentLayer += a[0];
            x += (a[1] % 2 != 0) ? directions.ElementAt(a[1]) * a[2] * (int)Math.Pow(2, currentLayer) : 0;
            y += (a[1] % 2 == 0) ? directions.ElementAt(a[1]) * a[2] * (int)Math.Pow(2, currentLayer) : 0;
        }
      return new[] {x, y};
    }
}
