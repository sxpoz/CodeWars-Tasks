using System;
using System.Linq;
using System.Collections.Generic;

namespace myjinxin
{
    public class Kata
    {
        public int RepeatAdjacent(string s)
        {
          List<int> list = new List<int>();
          for(int i = 1; i < s.Length; i++)
          {
            if (s[i] == s[i - 1]) list.Add(1);
            else list.Add(0);
          }
          return String.Concat<int>(list).Split("00").Select(x => x.Trim('0')).Count(x => x.Contains("101"));
        }
    }
}
