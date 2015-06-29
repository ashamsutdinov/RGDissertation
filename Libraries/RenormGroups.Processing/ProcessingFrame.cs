using System.Drawing;

namespace RenormGroups.Processing
{
  public sealed class ProcessingFrame
  {
    public RectangleD Rectangle { get; set; }

    public Rectangle ClientRectangle { get; set; }
    
    public Bitmap Image { get; set; }
  }
}