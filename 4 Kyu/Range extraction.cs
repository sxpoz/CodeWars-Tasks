using System;
using System.Linq;
public class RangeExtraction
{
    public static string Extract(int[] args)
    {
      int value = args.Min();
      string str = String.Concat<string>(args.Select(x => Convert.ToString(x) + ",")).TrimEnd(',');
      string strCheck = "";
      string strOut = "";
      for (int i = 0; i < args.Length; i++)
      {
        value = args[i] + 2;
        strCheck += args[i] + "," + (args[i] + 1) + "," + (args[i] + 2);
        if (str.Contains(strCheck))
        {
          while (true)
          {
            value++;
            strCheck += "," + value;
            if (str.Contains(strCheck)) continue;
            else break;
          }
          strOut += args[i] + "-" + (value - 1) + ",";
          i = Array.IndexOf(args, value - 1);
        }
        else strOut += args[i] + ",";
        strCheck = "";
      }
      return strOut.TrimEnd(',');
    }
}
