public static class Kata
{
  public static string Maskify(string str)
  {
    return str.Length > 4 ? new string('#', str.Length - 4) + str.Substring(str.Length - 4, 4) : str;
  }
}
