using System.Drawing;

namespace ReverseTransform
{
  public static class RG
  {
    public static Color Blend(Color color, Color backColor, double amount = 0.5)
    {
      var r = (byte)((color.R * amount) + backColor.R * (1 - amount));
      var g = (byte)((color.G * amount) + backColor.G * (1 - amount));
      var b = (byte)((color.B * amount) + backColor.B * (1 - amount));
      return Color.FromArgb(r, g, b);
    }

    public static Color GetColor(CPoint pt)
    {
      var rp = pt.RG;
      var cnt = 0;
      var end = pt.ReverseTrackEndPoint(Config.P1, Config.Alpha, Config.N, out cnt, Config.Acc, Config.Count);
      if (cnt >= Config.Count)
      {
        return Config.Black;
      }
      var last = end;
      var clr = rp.G < 0 ? (last.C1 < Config.P1.C1 ? Config.Yellow : Config.Green) : (last.C1 < Config.P1.C1 ? Config.Red : Config.Blue);
      var resClr = clr;
      return resClr;
    }
  }
}