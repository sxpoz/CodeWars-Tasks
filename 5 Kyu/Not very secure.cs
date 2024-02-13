using System;
using System.Linq;

public class Kata
{
  public static bool Alphanumeric(string str)
  {
    if (str == "") return false;
    return str.ToLower().Select(x => (int)x).Count(x => (97 <= x && x <= 122) || (48 <= x && x <= 57)) == str.Length ? true : false;
  }
}
