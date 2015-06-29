using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;

namespace RenormGroups.Configuration
{
  public static class Config
  {
    private static readonly Dictionary<string, object> Values = new Dictionary<string, object>();

    private static T GetValue<T>(string key, Func<string, T> transformer)
    {
      lock (Values)
      {
        if (!Values.ContainsKey(key))
        {
          var rawValue = ConfigurationManager.AppSettings[key];
          Values.Add(key, transformer(rawValue));
        }
        return (T)Values[key];
      }
    }

    private static Color ParseColor(string s)
    {
      try
      {
        var parts = s.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        var alpha = int.Parse(parts[0]);
        var red = int.Parse(parts[1]);
        var green = int.Parse(parts[2]);
        var blue = int.Parse(parts[3]);
        return Color.FromArgb(alpha, red, green, blue);
      }
      catch
      {
        return default(Color);
      }
    }

    public static long DefaultImageQuality
    {
      get { return GetValue("DefaultImageQuality", long.Parse); }
    }

    public static string DefaultImageMimeType
    {
      get { return GetValue("DefaultImageMimeType", s => s); }
    }

    public static string CFormat
    {
      get { return GetValue("CFormat", s => s); }
    }

    public static double CircleFillFactor
    {
      get { return GetValue("CircleFillFactor", s => double.Parse(s, NumberStyles.AllowDecimalPoint)); }
    }

    public static double LimitAccuracy
    {
      get { return GetValue("LimitAccuracy", s => double.Parse(s, NumberStyles.AllowDecimalPoint)); }
    }

    public static double CircleRadius
    {
      get { return GetValue("CircleRadius", double.Parse); }
    }

    public static Color GPositiveFillColor
    {
      get { return GetValue("GPositiveFillColor", ParseColor); }
    }

    public static Color GNegativeFillColor
    {
      get { return GetValue("GNegativeFillColor", ParseColor); }
    }

    public static Color C0PointCircleBorderColor
    {
      get { return GetValue("C0PointCircleBorderColor", ParseColor); }
    }

    public static Color C1PointCircleBorderColor
    {
      get { return GetValue("C1PointCircleBorderColor", ParseColor); }
    }

    public static Color C2PointCircleBorderColor
    {
      get { return GetValue("C2PointCircleBorderColor", ParseColor); }
    }

    public static Color BackgroundColor
    {
      get { return GetValue("BackgroundColor", ParseColor); }
    }

    public static Color LegendColor
    {
      get { return GetValue("LegendColor", ParseColor); }
    }

    public static Color TrackColor
    {
      get { return GetValue("TrackColor", ParseColor); }
    }

    public static Color PointColor
    {
      get { return GetValue("PointColor", ParseColor); }
    }

    public static Color AreaBorderColor
    {
      get { return GetValue("AreaBorderColor", ParseColor); }
    }

    public static long CriticalIterationsCount
    {
      get { return GetValue("CriticalIterationsCount", long.Parse); }
    }

    public static Color RightHandColor
    {
      get { return GetValue("RightHandColor", ParseColor); }
    }

    public static Color LeftHandColor
    {
      get { return GetValue("LeftHandColor", ParseColor); }
    }
  }
}