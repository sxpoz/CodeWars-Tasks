using System;
using System.Collections.Generic;
 
public class Kata
{
  public static double FindArea(List<Point> points)
  {
    double result = 0;
    for (int i = 0; i < points.Count - 1; i++)
    {
      double x1 = points[i].X, x2 = points[i + 1].X, y1 = points[i].Y, y2 = points[i + 1].Y, y = y2 < y1 ? y2 : y1;
      result += (x2 - x1) * (0.5 * Math.Abs(y2 - y1) + y); //formula - just split area into triangles and rectangles and do some math
    }
    return result;
  }
}
