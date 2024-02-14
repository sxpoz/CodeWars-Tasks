using System;
using System.Linq;
using System.Collections.Generic;

public static class Kata
{
  public static bool IsTuringEquation(string str)
  {
    char[] separators = { '+', '=' };
    List<int> list = str.Split(separators).Select(x => Convert.ToInt32(String.Concat<char>(x.Reverse()))).ToList();
    return list[0] + list[1] == list[2] ? true : false;
  }
}
