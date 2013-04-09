using System;
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

    public static Color GetBgColor(CPoint pt)
    {
      var rp = pt.RG;
      {
        return rp.G < 0 ? Color.Green : Color.Blue;
      }
    }

    public static Image GetBg(int width, int height)
    {
      var img = new Bitmap(width, height);

      var onePtWidth = 2.0 / width;
      var onePtHeight = 2.0 / height;
      const double x1 = -1.0;
      const double y1 = -1.0;

      for (var i = 0; i < width; i++)
      {
        for (var j = 0; j < height; j++)
        {
          var x = x1 + i * onePtWidth;
          var y = y1 + j * onePtHeight;
          var c0 = x;
          var c1 = y;
          var rd = c0 * c0 + c1 * c1;
          var c2 = 1 - rd;
          Color clr;
          if (rd <= 1)
          {
            c2 = Math.Sqrt(c2);
            var cpt = new CPoint(c0, c1, c2);
            clr = GetBgColor(cpt);
          }
          else
          {
            clr = Color.White;
          }
          img.SetPixel(i, j, clr);
        }
      }

      return img;
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
      var p = Config.P1;
      Color clr;
      if (p.C0 > 0)
      {
        clr = rp.G < 0
                    ? (last.C1 < p.C1 ? Config.Yellow : Config.Green)
                    : (last.C1 < p.C1 ? Config.Red : Config.Blue);
      }
      else
      {
        clr = rp.G < 0
                    ? (last.C1 > p.C1 ? Config.Yellow : Config.Green)
                    : (last.C1 > p.C1 ? Config.Red : Config.Blue);
      }
      var resClr = clr;
      return resClr;
    }

    public static double Correct(double value, double max)
    {
      if (value < 0)
        return 0;
      if (value >= max)
        return max - 1;
      return value;
    }

    public static void DrawLine(double x, double y, double xsz, double ysz, double sz, Pen pen, CPoint cp1, CPoint cp2, Graphics gr, bool verify = true)
    {
      var i1 = Correct((cp1.C0 - x) / xsz, sz);
      var i2 = Correct((cp2.C0 - x) / xsz, sz);
      var j1 = Correct((cp1.C1 - y) / ysz, sz);
      var j2 = Correct((cp2.C1 - y) / ysz, sz);

      var di = Math.Abs(i2 - i1);
      var dj = Math.Abs(j2 - j1);
      var d = Math.Sqrt(di * di + dj * dj);

      var check = sz / 3;
      if (d >= check && verify)
      {
        return;
      }

      lock (gr)
      {
        gr.DrawLine(pen, (float)i1, (float)j1, (float)i2, (float)j2);
      }
    }

    public static void FillPoint(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, int radius = 3)
    {
      var i1 = Correct((cp1.C0 - x) / xsz, sz);
      var j1 = Correct((cp1.C1 - y) / ysz, sz);

      var brush = new SolidBrush(clr);

      lock (gr)
      {
        gr.FillEllipse(brush, (float)(i1 - radius), (float)(j1 - radius), 2 * radius, 2 * radius);
      }
    }
  }
}