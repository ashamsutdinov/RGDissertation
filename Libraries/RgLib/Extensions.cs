using System;
using System.Drawing;

namespace ReverseTransform
{
  public static class Extensions
  {
    public static void TryDraw(this Graphics gr, Action<Graphics> act)
    {
      try
      {
        act(gr);
      }
      catch (Exception)
      {
      }
    }

    public static void SetPixelSafe(this Bitmap bmp, int i, int j, Color clr)
    {
      var w = bmp.Width;
      var h = bmp.Height;
      if (i >= 0 && i < w && j >= 0 && j < h)
        bmp.SetPixel(i, j, clr);
    }
  }
}