using System.Drawing;

namespace RenormGroups.Processing
{
  public sealed class ProcessingFrame
  {
    public RectangleD Rectangle { get; set; }

    public Rectangle ClientRectangle { get; set; }
    
    public Bitmap Image { get; set; }

    public Bitmap ProcessedImage { get; set; }

    public Bitmap DrawedImage { get; set; }

    public Bitmap ActualImage
    {
      get { return ProcessedImage ?? Image; }
    }
  }
}