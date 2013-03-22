using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using ReverseTransform;

namespace rg
{
  class Program
  {
    private static double Alpha { get; set; }

    public static double N { get; set; }

    public static double Lambda { get; set; }

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

    private static readonly CPoint P1 = new CPoint(1, 0, 0);

    private static readonly CPoint P2 = new CPoint(0, 0, 1);

    private static readonly Brush BlackBrush = new SolidBrush(Color.Black);

    private static readonly Pen BlackPen = new Pen(Color.Black);

    private static readonly Pen RedPen = new Pen(Color.Red);

    private static readonly Pen WhitePen = new Pen(Color.White);

    private static readonly Color Red = Color.Red;

    private static readonly Color Blue = Color.Blue;

    private static readonly Color Black = Color.Black;

    private static readonly Color Green = Color.Green;

    private static readonly Color Yellow = Color.Yellow;

    private static readonly Color White = Color.White;

    private static readonly Color BgColor = Color.IndianRed;

    private static ParallelOptions POpts { get; set; }

    public static Color ColorMixer(Color c1, Color c2)
    {

      var r = Math.Min((c1.R + c2.R), 255);
      var g = Math.Min((c1.G + c2.G), 255);
      var b = Math.Min((c1.B + c2.B), 255);

      return Color.FromArgb(255, r, g, b);
    }

    public static Color Blend(Color color, Color backColor, double amount = 0.5)
    {
      var r = (byte)((color.R * amount) + backColor.R * (1 - amount));
      var g = (byte)((color.G * amount) + backColor.G * (1 - amount));
      var b = (byte)((color.B * amount) + backColor.B * (1 - amount));
      return Color.FromArgb(r, g, b);
    }

    private static Color GetColor(CPoint pt)
    {
      var rp = pt.RG;

      if (!Dynamics)
      {
        return rp.G < 0 ? Green : Blue;
      }

      var baseClr = rp.G <= 0 ? White : Black;

      var track = pt.ReverseTrack(P1, Alpha, N).ToList();
      if (track.Count == 100)
      {
        return baseClr;
      }
      var last = track.Last();
      var resClr = rp.G < 0 ? last.C1 < P1.C1 ? Yellow : Green : last.C1 < P1.C1 ? Blue : Red;
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

    private static void DrawParabola(double a, double b, Graphics gr, DashStyle dash = DashStyle.Dot, bool beforeTrans = true)
    {
      var rgParabola = RGPoint.Parabola(Alpha, N, a, b, beforeTrans);
      var l = -Math.Pow(Lambda, -1);
      var rgPoints = rgParabola as RGPoint[] ?? rgParabola.ToArray();
      var beforeB = rgPoints.Where(rg => rg.R <= b).ToList();
      var fromBtoL = rgPoints.Where(rg => rg.R >= b && rg.R <= l).ToList();
      var fromLtoB = rgPoints.Where(rg => rg.R >= l && rg.R <= b).ToList();
      var fromL = rgPoints.Where(rg => rg.R >= l).ToList();

      var cParabola1 = beforeB.Select(rg => rg.C).ToList();
      var cParabola2 = fromBtoL.Select(rg => rg.C).ToList();
      var cParabola3 = fromLtoB.Select(rg => rg.C).ToList();
      var cParabola4 = fromL.Select(rg => rg.C).ToList();

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
      Alpha = 1.7;
      N = 2;
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
            Alpha = double.Parse(val);
            break;
          case "n":
            N = double.Parse(val);
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
      Lambda = Math.Pow(N, Alpha - 1);
    }

    private static void CreateBitmap()
    {
      Image = new Bitmap(Sz, Sz);
      var gr = Graphics.FromImage(Image);
      gr.FillRectangle(new SolidBrush(BgColor), 0, 0, Sz, Sz);
      gr.Save();
    }

    private static void ProcessBitmap()
    {
      DrawDynamics();
      DrawParabolas();
    }

    private static void DrawParabolas()
    {
      if (SingleCurve)
        AMax = AMin;
      var clrMax = (int)Math.Abs(AMax - AMin);
      var cnt = (int)(clrMax / AStep + 1);
      var l = Math.Pow(N, Alpha - 1);
      var b = -(N - 1) / (N - l);
      var gr = Graphics.FromImage(Image);
      Parallel.For(0, cnt, POpts, p =>
      {
        var a = AMin + p * AStep;
        DrawParabola(a, b, gr);
        var a1 = Lambda * a;
        DrawParabola(a1, b, gr, DashStyle.Solid, false);
      });
      gr.Save();
    }

    private static void DrawDynamics()
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
            clr = GetColor(cpt);
          }
          else
          {
            clr = White;
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
      var fname = string.Format("img/a{0}n{1}({2};{3})({4};{5}) ({6}).png", Alpha, N, X, Y, X + W, Y + W, singlePart);
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
