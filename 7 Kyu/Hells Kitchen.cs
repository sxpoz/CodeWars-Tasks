using System.Linq;
using System.Collections.Generic;

public class Kata
{
  public static string Gordon(string str)
  {
    string outcome = "";
    char[] vowels = { 'I', 'U', 'E', 'O'};
    foreach (string s in str.Split(' ').Select(x => x.ToUpper() + "!!!!").ToList())
      outcome += s.Contains('A') ? s.Replace('A', '@') + ' ' : s + ' ';
    foreach (char c in vowels)
      outcome = outcome.Replace(c, '*');
    return outcome.TrimEnd(' ');
  }
}
