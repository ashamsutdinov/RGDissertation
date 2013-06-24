using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using ReverseTransform;
using RgLib;

namespace rg
{
  static class Program
  {
    public static bool Dynamics { get; set; }

    public static bool SingleCurve { get; set; }

    public static double AMin { get; set; }

    public static double AMax { get; set; }

    public static double AStep { get; set; }

    public static int Sz { get; set; }

    private static Bitmap Image { get; set; }

    private static double OnePixelX { get; set; }

    private static double OnePixelY { get; set; }

    private static double X { get; set; }

    private static double Y { get; set; }

    private static double W { get; set; }

    private static double H
    {
      get;
      set;
    }

    private static ParallelOptions POpts { get; set; }

    private static Color GetColorReversed(CPoint pt)
    {
      var rp = pt.RG(CProjection.C0C1);

      if (!Dynamics)
      {
        return rp.G < 0 ? Config.Green : Config.Blue;
      }

      var baseClr = rp.G <= 0 ? Config.White : Config.Black;

      var track = pt.ReverseTrack(Config.ReserverInterestedPoint, CProjection.UpC0C1).ToList();
      if (track.Count == 100)
      {
        return baseClr;
      }
      var last = track.Last();
      var resClr = rp.G < 0 ? last.C1 < Config.ReserverInterestedPoint.C1 ? Config.Yellow : Config.Green : last.C1 < Config.ReserverInterestedPoint.C1 ? Config.Blue : Config.Red;
      return resClr;
    }

    private static double Correct(double value, double max)
    {
      if (value < 0)
        return 0;
      if (value >= max)
        return max - 1;
      return value;
    }

    private static void DrawLine(Pen pen, CPoint cp1, CPoint cp2, Graphics gr, bool verify = true)
    {
      var i1 = Correct((cp1.C0 - X) / OnePixelX, Sz);
      var i2 = Correct((cp2.C0 - X) / OnePixelX, Sz);
      var j1 = Correct((cp1.C1 - Y) / OnePixelY, Sz);
      var j2 = Correct((cp2.C1 - Y) / OnePixelY, Sz);

      var di = Math.Abs(i2 - i1);
      var dj = Math.Abs(j2 - j1);
      var d = Math.Sqrt(di * di + dj * dj);

      var check = Sz / 10;
      if (d >= check && verify)
      {
        return;
      }

      lock (gr)
      {
        gr.DrawLine(pen, (float)i1, (float)j1, (float)i2, (float)j2);
      }
    }

    private static void FillPoint(Color clr, CPoint cp1, Graphics gr, int radius = 3)
    {
      var i1 = Correct((cp1.C0 - X) / OnePixelX, Sz);
      var j1 = Correct((cp1.C1 - Y) / OnePixelY, Sz);

      var brush = new SolidBrush(clr);

      lock (gr)
      {
        gr.FillEllipse(brush, (float)(i1 - radius), (float)(j1 - radius), 2 * radius, 2 * radius);
      }
    }

    private static void DrawParabolaReversed(double a, double b, Graphics gr, DashStyle dash = DashStyle.Dot, bool beforeTrans = true)
    {
      var rgParabola = RGPoint.ParabolaReversed(a, b, -1000, 1000, 0.1, beforeTrans);
      var rgPoints = rgParabola as RGPoint[] ?? rgParabola.ToArray();
      var beforeB = rgPoints.Where(rg => rg.R <= b).ToList();
      var fromBtoL = rgPoints.Where(rg => rg.R >= b && rg.R <= RgSettings.LambdaMinus1).ToList();
      var fromLtoB = rgPoints.Where(rg => rg.R >= RgSettings.LambdaMinus1 && rg.R <= b).ToList();
      var fromL = rgPoints.Where(rg => rg.R >= RgSettings.LambdaMinus1).ToList();

      var cParabola1 = beforeB.Select(rg => rg.CReversed).ToList();
      var cParabola2 = fromBtoL.Select(rg => rg.CReversed).ToList();
      var cParabola3 = fromLtoB.Select(rg => rg.CReversed).ToList();
      var cParabola4 = fromL.Select(rg => rg.CReversed).ToList();

      var pen1 = new Pen(Color.Red, 2) { DashStyle = dash };
      for (var i = 0; i < cParabola1.Count - 1; i++)
      {
        var cp1 = cParabola1[i];
        var cp2 = cParabola1[i + 1];
        var realPen = pen1;
        DrawLine(realPen, cp1, cp2, gr);
      }
      if (cParabola1.Any())
      {
        var first = cParabola1.First();
        var last = cParabola1.Last();
        FillPoint(Color.Red, first, gr);
        FillPoint(Color.Red, last, gr);
      }

      var pen2 = new Pen(Color.LemonChiffon, 2) { DashStyle = dash };
      for (var i = 0; i < cParabola2.Count - 1; i++)
      {
        var cp1 = cParabola2[i];
        var cp2 = cParabola2[i + 1];
        var realPen = pen2;
        DrawLine(realPen, cp1, cp2, gr);
      }
      if (cParabola2.Any())
      {
        var first = cParabola2.First();
        var last = cParabola2.Last();
        FillPoint(Color.LemonChiffon, first, gr);
        FillPoint(Color.LemonChiffon, last, gr);
      }

      var pen3 = new Pen(Color.Yellow, 2) { DashStyle = dash };
      for (var i = 0; i < cParabola3.Count - 1; i++)
      {
        var cp1 = cParabola3[i];
        var cp2 = cParabola3[i + 1];
        var realPen = pen3;
        DrawLine(realPen, cp1, cp2, gr);
      }
      if (cParabola3.Any())
      {
        var first = cParabola3.First();
        var last = cParabola3.Last();
        FillPoint(Color.Yellow, first, gr);
        FillPoint(Color.Yellow, last, gr);
      }

      var pen4 = new Pen(Color.DarkViolet, 2) { DashStyle = dash };
      for (var i = 0; i < cParabola4.Count - 1; i++)
      {
        var cp1 = cParabola4[i];
        var cp2 = cParabola4[i + 1];
        var realPen = pen4;
        DrawLine(realPen, cp1, cp2, gr);
      }
      if (cParabola4.Any())
      {
        var first = cParabola4.First();
        var last = cParabola4.Last();
        FillPoint(Color.DarkViolet, first, gr);
        FillPoint(Color.DarkViolet, last, gr);
      }
    }

    private static void ReadConfig(IEnumerable<string> args)
    {
      var alpha = 1.7;
      var n = 2d;
      AMin = -10;
      AMax = 10;
      AStep = 1;
      Sz = 10000;
      X = -1;
      Y = -1;
      W = 2;
      H = 2;
      Dynamics = true;
      SingleCurve = false;
      POpts = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
      foreach (var s in args)
      {
        var parts = s.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
        var cfg = parts[0];
        var val = parts.Length == 2 ? parts[1] : string.Empty;
        switch (cfg)
        {
          case "singlecurve":
            SingleCurve = true;
            break;
          case "a":
            alpha = double.Parse(val);
            break;
          case "n":
            n = double.Parse(val);
            break;
          case "amin":
            AMin = double.Parse(val);
            break;
          case "amax":
            AMax = double.Parse(val);
            break;
          case "astep":
            AStep = double.Parse(val);
            break;
          case "sz":
            Sz = int.Parse(val);
            break;
          case "p":
            POpts = new ParallelOptions { MaxDegreeOfParallelism = int.Parse(val) };
            break;
          case "nodynamics":
            Dynamics = false;
            break;
          case "x":
            X = double.Parse(val);
            break;
          case "y":
            Y = double.Parse(val);
            break;
          case "w":
            W = double.Parse(val);
            break;
          case "h":
            H = double.Parse(val);
            break;
        }
      }
      OnePixelX = W / Sz;
      OnePixelY = H / Sz;
      RgSettings.Build(alpha, n);
    }

    private static void CreateBitmap()
    {
      Image = new Bitmap(Sz, Sz);
      var gr = Graphics.FromImage(Image);
      gr.FillRectangle(new SolidBrush(Config.BgColor), 0, 0, Sz, Sz);
      gr.Save();
    }

    private static void ProcessBitmap()
    {
      DrawDynamicsReversed();
      //DrawParabolasReversed();
    }

    private static void DrawParabolasReversed()
    {
      if (SingleCurve)
        AMax = AMin;
      var clrMax = (int)Math.Abs(AMax - AMin);
      var cnt = (int)(clrMax / AStep + 1);
      var l = RgSettings.Lambda;
      var b = -(RgSettings.N - 1) / (RgSettings.N - l);
      var gr = Graphics.FromImage(Image);
      Parallel.For(0, cnt, POpts, p =>
      {
        var a = AMin + p * AStep;
        DrawParabolaReversed(a, b, gr);
        var a1 = l * a;
        DrawParabolaReversed(a1, b, gr, DashStyle.Solid, false);
      });
      gr.Save();
    }

    private static void DrawDynamicsReversed()
    {
      Parallel.For(0, Sz, POpts, i =>
      {
        var x = X + i * OnePixelX;
        for (var j = 0; j < Sz; j++)
        {
          var y = Y + j * OnePixelY;
          var c0 = x;
          var c1 = y;
          var rd = c0 * c0 + c1 * c1;
          var c2 = 1 - rd;
          Color clr;
          if (rd <= 1)
          {
            c2 = Math.Sqrt(c2);
            var cpt = new CPoint(c0, c1, c2);
            clr = GetColorReversed(cpt);
          }
          else
          {
            clr = Config.White;
          }
          lock (Image)
          {
            Image.SetPixel(i, j, clr);
          }
        }
      });
    }

    private static void SaveBitmap()
    {
      Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
      var singlePart = "";
      if (SingleCurve)
      {
        singlePart = string.Format("single_{0}", AMin);
      }
      var fname = string.Format("img/a{0}n{1}({2};{3})({4};{5}) ({6}).png", RgSettings.Alpha, RgSettings.N, X, Y, X + W, Y + W, singlePart);
      Image.Save(fname, ImageFormat.Png);
      Image.Dispose();
    }

    static void Main(string[] args)
    {
      ReadConfig(args);
      CreateBitmap();
      ProcessBitmap();
      SaveBitmap();
    }
  }
}
