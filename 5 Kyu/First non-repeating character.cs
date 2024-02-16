using System.Linq;

public class Kata
{
  public static string FirstNonRepeatingLetter(string s)
  {
    string sCopy = s.ToLower();
    foreach (char ch in sCopy.ToCharArray())
      if (sCopy.Count(x => x == ch) == 1)
        return s[sCopy.IndexOf(ch)].ToString();
    return "";
  }
}
