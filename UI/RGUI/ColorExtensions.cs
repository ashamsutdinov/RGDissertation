using System.Drawing;

namespace RGUI
{
  public static class ColorExtensions
  {
    public static Color Interted(this Color color)
    {
      var a = color.A;
      var r = 255 - color.R;
      var g = 255 - color.G;
      var b = 255 - color.B;
      return Color.FromArgb(a, r, g, b);
    }
  }
}