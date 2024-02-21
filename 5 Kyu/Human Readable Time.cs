using System;

public static class TimeFormat
{
    public static string GetReadableTime(int seconds)
    {
      TimeSpan time = TimeSpan.FromSeconds(seconds);
      return $"{(time.Days * 24 + time.Hours):d2}:{time.Minutes:d2}:{time.Seconds:d2}";
    }
}
