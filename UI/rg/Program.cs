using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace rg
{
  class Program
  {
    private static double Alpha { get; set; }

    public static double N { get; set; }

    public static bool Curves { get; set; }

    public static bool Dynamics { get; set; }

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

    private static readonly Color BgColor = Color.Indigo;

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
      if (!pt.IsNormal)
      {
        return White;
      }

      var rp = pt.RG;
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

    private static void DrawParabola(Color clr, double a, double b, Graphics gr)
    {
      var rgParabola = RGPoint.Parabola(Alpha, N, a, b);
      var cParabola = rgParabola.Select(rg => rg.C).ToList();
      var pen = new Pen(clr);

      for (var i = 0; i < cParabola.Count - 1; i++)
      {
        var cp1 = cParabola[i];
        var cp2 = cParabola[i + 1];

        var i1 = (cp1.C0 - X) / OnePixelX;
        var i2 = (cp2.C0 - X) / OnePixelX;
        var j1 = (cp1.C1 - Y) / OnePixelY;
        var j2 = (cp2.C1 - Y) / OnePixelY;

        var check = Sz / 2;

        if (!(Math.Abs(i1 - i2) <= check))
          continue;

        /*
         * if (!(i1 >= 0) || !(i1 < Sz) || !(i2 >= 0) || !(i2 <= Sz) || !(j1 >= 0) || !(j1 <= Sz) || !(j2 >= 0) ||
            !(j2 <= Sz)) continue;
         * */

        lock (gr)
        {
          gr.DrawLine(pen, (float)i1, (float)j1, (float)i2, (float)j2);
        }
      }
    }

    private static void ReadConfig(IEnumerable<string> args)
    {
      Alpha = 1.7;
      N = 2;
      Curves = false;
      AMin = -10;
      AMax = 10;
      AStep = 1;
      Sz = 10000;
      X = -1;
      Y = -1;
      W = 2;
      H = 2;
      Dynamics = true;
      POpts = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
      foreach (var s in args)
      {
        var parts = s.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
        var cfg = parts[0];
        var val = parts.Length == 2 ? parts[1] : string.Empty;
        switch (cfg)
        {
          case "curves":
            Curves = true;
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
        }
      }
      OnePixelX = W / Sz;
      OnePixelY = H / Sz;
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
      if (Dynamics)
      {
        DrawDynamics();
      }
      if (Curves)
      {
        DrawParabolas();
      }
    }

    private static void DrawParabolas()
    {
      var clrMax = (int)Math.Abs(AMax - AMin);
      var cnt = (int)(clrMax / AStep + 1);
      var step = 255.0 / cnt;
      var l = Math.Pow(N, Alpha - 1);
      var b = -(N - 1) / (N - l);
      var gr = Graphics.FromImage(Image);
      Parallel.For(0, cnt, POpts, p =>
      {
        var a = AMin + p * AStep;
        var iclr = (int)(p * step);
        var clr = Color.FromArgb(255, iclr, iclr, iclr);
        DrawParabola(clr, a, b, gr);
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
          if (rd <= 1)
          {
            c2 = Math.Sqrt(c2);
          }
          var cpt = new CPoint(c0, c1, c2);
          var clr = GetColor(cpt);
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
      var fname = string.Format("a{0}-n{1}.bmp", Alpha, N);
      Image.Save(fname);
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
