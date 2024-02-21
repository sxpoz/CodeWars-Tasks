using System;
using System.Linq;
using System.Collections.Generic;

public class RomanNumerals
{
    static Dictionary<string, int> pairs = new Dictionary<string, int>()
    {
        { "M", 1000 },
        { "CM", 900 },
        { "D", 500 },
        { "CD", 400 },
        { "C", 100 },
        { "XC", 90 },
        { "L", 50 },
        { "XL", 40 },
        { "X", 10 },
        { "IX", 9 },
        { "V", 5 },
        { "IV", 4 },
        { "I", 1 },
    };
  
    public static int FromRoman(string number)
    {
      int output = 0, previousValue = 0;
      foreach (char c in String.Concat<char>(number.Reverse())) 
      {
        if (pairs.TryGetValue(c.ToString(), out int currentValue))
          output += currentValue >= previousValue ? currentValue : (-1) * currentValue;
        previousValue = currentValue;
      }
      return output;
    }
  
    public static string ToRoman(int number)
    {
      string output = "";
      string numberString = number.ToString();
      int stringLength = numberString.Length, tempInt = 0;
      List<int> parts = new List<int>();
      foreach (char c in numberString)
        parts.Add(Convert.ToInt32(c + new string('0', stringLength-- - 1)));
      foreach (int part in parts)
      {
        if (part == 0) 
          continue;
        tempInt = part;
        int partLength = part.ToString().Length;
        int n = Convert.ToInt32(part.ToString().Substring(0, 1));
        if (pairs.ContainsValue(part))
          output += pairs.First(x => x.Value == part).Key;
        else
        {
          if (n > 5)
          {
            n -= 5;
            output += pairs.First(x => x.Value == 5 * Math.Pow(10, partLength - 1)).Key;
            tempInt -= 5 * (int)Math.Pow(10, partLength - 1);
          }
          string letter = pairs.First(x => x.Value == tempInt / n).Key;
          while (n-- > 0)
            output += letter;
        }
      }
      return output;
    }
  }
