using System;

public class Kata
{
  public static string CreatePhoneNumber(int[] numbers)
  {
    long num = Convert.ToInt64(String.Concat<int>(numbers));
    if (numbers[0] != 0) return string.Format($"{num:(###) ###-####}");
    else return string.Format($"(0{num:##) ###-####}");
  }
}
