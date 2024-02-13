namespace Solution
{
  using System;
  using System.Numerics;
  using System.Linq;
  
  class LastDigit
  {
    public static int GetLastDigit(BigInteger n1, BigInteger n2)
    {
      if (n2 == 0) return 1;
      switch (Convert.ToString(n1).LastOrDefault())
      {
          case '0': return 0;
          case '1': return 1;
          case '2': return n2 % 4 == 1 ? 2 : n2 % 4 == 2 ? 4 : n2 % 4 == 3 ? 8 : 6;
          case '3': return n2 % 4 == 1 ? 3 : n2 % 4 == 2 ? 9 : n2 % 4 == 3 ? 7 : 1;
          case '4': return n2 % 2 == 0 ? 6 : 4;
          case '5': return 5;
          case '6': return 6;
          case '7': return n2 % 4 == 1 ? 7 : n2 % 4 == 2 ? 9 : n2 % 4 == 3 ? 3 : 1;
          case '8': return n2 % 4 == 1 ? 8 : n2 % 4 == 2 ? 4 : n2 % 4 == 3 ? 2 : 6;
          case '9': return n2 % 2 == 0 ? 1 : 9;
      }
      return 0;
    }
  }
}
