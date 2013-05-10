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
  }
}