using System;
using System.Collections.Generic;

public class SnailSolution
{
   public static int[] Snail(int[][] array)
   {
     if (array[0].Length == 0) return new int[0];
     List<int> exit = new List<int>();
     List<string> indexes = new List<string>();
     int i = 0, iMin = 0, iMax = array.Length;
     int j = 0, jMin = 0, jMax = iMax;
     int turns = 0;
     int turnIncrement = 0;
     if (array.GetLength(0) % 2 != 0) turns = array.GetLength(0) / 2 + 1;
     if (array.GetLength(0) % 2 == 0) turns = array.GetLength(0) / 2;
     while (turns != 0)
     {
       while (j != jMax - turnIncrement)
       {
         indexes.Add($"{i},{j}");
         j++;
       }
      i++; j--;
       while (i != iMax - turnIncrement)
       {
         indexes.Add($"{i},{j}");
         i++;
       }
       i--; j--;
       while (j >= jMin + turnIncrement)
       {
         indexes.Add($"{i},{j}");
         j--;
       }
       j++; i--;
       while (i >= iMin + turnIncrement + 1)
       {
         indexes.Add($"{i},{j}");
         i--;
       }
       i++; j++;
       turnIncrement++;
       turns--;
     }

     foreach (string s in indexes) { exit.Add(array[Convert.ToInt32(s.Split(',')[0])][Convert.ToInt32(s.Split(',')[1])]); }
     return exit.ToArray();
   }
}
