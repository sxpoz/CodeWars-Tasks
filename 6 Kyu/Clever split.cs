//shitty code but w/e; refactor it someday

using System;
using System.Linq;
using System.Collections.Generic;

public static class Kata
{
  public static string[] CleverSplit(string str)
  {
    List<string> temp = str.Split('[', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Contains(']') ? '[' + x : x).ToList();
    List<string> output = new List<string>();
    foreach (string s in temp)
    {
      if (s.Contains('[') && s.Last() == ']')
         output.Add(s.Trim(' '));
      else if (s.Contains('['))
      {
        List<string> temp1 = s.Split(']', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Contains('[') ? x + ']' : x).ToList();
        foreach(string s1 in temp1)
        {
          if (s1.Contains('['))
            output.Add(s1.Trim(' '));
          else
            s1.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => output.Add(x));
        }
      }
      else 
        s.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x => output.Add(x));
    }
    return output.ToArray();
  }
}
