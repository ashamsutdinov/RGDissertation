using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace rg
{
  class Program
  {
    private static double Alpha { get; set; }

    public static double N { get; set; }

    public static bool Curves { get; set; }

    public static double AMin { get; set; }

    public static double AMax { get; set; }

    public static double AStep { get; set; }

    public static int Sz { get; set; }

    private static Bitmap Image { get; set; }

    private static void ReadConfig(IEnumerable<string> args)
    {
      foreach (var s in args)
      {
        Alpha = 1.7;
        N = 2;
        Curves = false;
        AMin = -10;
        AMax = 10;
        AStep = 1;
        Sz = 10000;
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
        }
      }
    }

    private static void CreateBitmap()
    {
      Image = new Bitmap(Sz, Sz, PixelFormat.Format32bppArgb);
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
      SaveBitmap();
    }
  }
}
