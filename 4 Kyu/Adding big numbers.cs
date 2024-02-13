using System;
using System.Linq;
using System.Collections.Generic;

public class Kata
{
  public static List<int> Calculate(string str1, string str2) 
  {
    List<int> list = new List<int>();
    int j = str2.Length - 1;
    int num = str2[j] - '0';
    int additionalNum = 0;
    for (int i = str1.Length - 1; i >= 0; i--)
    {
        int sum = (str1[i] - '0') + num + additionalNum;
        if (sum >= 10)
        {
            additionalNum = sum / 10;
            sum %= 10;
        }
        else additionalNum = 0;
        list.Add(sum);
        if (j > 0)
        {
            j--;
            num = str2[j] - '0';
        }
        else num = 0;
    }
    if (additionalNum != 0) list.Add(additionalNum);
    list.Reverse();
    return list;
  }
  public static string Add(string str1, string str2)
  {
    return str1.Length >= str2.Length ? String.Concat<int>(Calculate(str1, str2)) : String.Concat<int>(Calculate(str2, str1));
  }
}
