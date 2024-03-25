using System;
using System.Collections.Generic;
using System.Linq;

namespace myjinxin
{
    public class Kata
    {
        public int DigitsProduct(int product)
        {
          int currentNumber = 10;
          while (true)
          {
            if (product * product + 10 < currentNumber)
                return -1;
            List<int> digits = currentNumber.ToString().Select(x => (int)char.GetNumericValue(x)).ToList();
            int multiply = 1;
            digits.ForEach(x => multiply *= x);
            if (multiply == product)
                return currentNumber;
            else currentNumber++;
          }
        }
    }
}
