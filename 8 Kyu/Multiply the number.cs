using System;

public class Kata
{
  public static int Multiply(int number) => number * (int)Math.Pow(5, Math.Abs(number).ToString().Length);
}
