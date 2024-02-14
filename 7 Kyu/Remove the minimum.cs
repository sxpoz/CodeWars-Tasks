using System;
using System.Collections.Generic;
using System.Linq;

public class Remover
{
  public static List<int> RemoveSmallest(List<int> numbers) 
  {
    if (numbers.Count == 0) return new List<int>();
    List<int> output = new List<int>();
    Enumerable.Range(0, numbers.Count).ToList().ForEach(x => output.Add(numbers[x]));
    output.RemoveAt(numbers.IndexOf(numbers.Min(x => x)));
    return output;
  }
}
