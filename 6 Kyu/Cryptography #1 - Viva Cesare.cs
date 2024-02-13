using System;
using System.Linq;

class CaesarCrypto
{
    public static string Encode(string text, int shift)
    {
      if (text == null) return "";
      string output = "";
      string alphabet = "abcdefghijklmnopqrstuvwxyz";
      alphabet += alphabet.ToUpper();
      while (Math.Abs(shift) > alphabet.Length) 
        shift = (shift > 0) ? shift - alphabet.Length : shift + alphabet.Length;
      int length = alphabet.Length;
      foreach (char c in text)
      {
          int charIndex = alphabet.IndexOf(c); 
          if (shift >= 0 && charIndex + shift > length - 1)
             shift -= length;
          if (shift < 0 && charIndex + shift < 0)
              shift += length;
          output += (alphabet.Contains(c)) ? alphabet[charIndex + shift] : c;
      }
      return (text == "" || text.Length == text.Count(x => x == ' ')) ? "" : output;
    }
}
