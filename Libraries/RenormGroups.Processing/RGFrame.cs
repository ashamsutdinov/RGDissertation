using System.Drawing;

namespace RenormGroups.Processing
{
  public class RGFrame
  {
    public RectangleF PlainRect { get; set; }

    public Image Image { get; set; }
  }

  public class CPointEx : CPoint
  {
    public Color Color { get; set; }
  }
}