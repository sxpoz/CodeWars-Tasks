using System;
using System.Linq;
using System.Collections.Generic;

public class StripCommentsSolution
{
    public static string StripComments(string text, string[] commentSymbols)
    {
      List<string> list = text.Split('\n').ToList();
      char[] separators = commentSymbols.Select(x => Convert.ToChar(x)).ToArray();
      string output = "";
      foreach (string line in list)
        output += String.Concat<string>(line.Split(separators).Select((x, i) => (i == 0 && x != "") ? x : "")).TrimEnd(' ') + '\n';
      return output.Substring(0, output.Length - 1);
    }
}
