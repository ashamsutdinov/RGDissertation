using System;
using System.Collections.Generic;
using System.Drawing;
using RenormGroups.Processing;

namespace RenormGroups.Drawing
{
  [Obsolete]
  public class TestRGDrawer
  {
    public const double RGCircleRadius = 1;

    public const float Epsilon = 0.000001f;

    public Size AreaSize { get; private set; }

    public PointF AreaCenter { get; private set; }

    public RectangleF CircleRect { get; private set; }

    public RGFrame Frame { get; private set; }

    public Stack<RGFrame> Frames { get; private set; }

    readonly Brush _whiteBrush = new SolidBrush(Color.White);

    readonly Pen _blackPen = new Pen(Color.Black);

    readonly Brush _yellowBrush = new SolidBrush(Color.Yellow);

    public Image DrawInitialGraph(Size size)
    {
      GC.Collect();

      AreaSize = size;
      Frames = new Stack<RGFrame>();

      Image res = new Bitmap(size.Width, size.Height);
      var gr = Graphics.FromImage(res);

      gr.FillRectangle(_whiteBrush, 0, 0, size.Width, size.Height);

      var radius = Math.Min(size.Width, size.Height) / 2.0f;
      radius = radius * 0.9f;

      var centerX = (float)(size.Width / (RGCircleRadius * 2));
      var centerY = (float)(size.Height / (RGCircleRadius * 2));
      AreaCenter = new PointF(centerX, centerY);
      CircleRect = new RectangleF(centerX - radius, centerY - radius, radius * 2, radius * 2);

      gr.FillEllipse(_yellowBrush, CircleRect);

      AddRGFeatures(res as Bitmap);

      gr.DrawLine(_blackPen, 0, centerY, size.Width, centerY);
      gr.DrawLine(_blackPen, centerX, 0, centerX, size.Height);

      gr.Save();

      var onePixelWeight = (float)(RGCircleRadius / radius);
      var xLen = size.Width * onePixelWeight;
      var yLen = size.Height * onePixelWeight;
      var x1 = -xLen / 2;
      var y1 = -yLen / 2;

      Frame = new RGFrame { Image = res, PlainRect = new RectangleF(x1, y1, xLen, yLen) };
      Frames.Push(Frame);
      return res;
    }

    public void AddRGFeatures(Bitmap image, double x1 = -1, double x2 = 1, double y1 = -1, double y2 = 1)
    {
      var gr = Graphics.FromImage(image);

      var interestedPixelsX = CircleRect.Width;
      var interestedPixelsY = CircleRect.Height;
      var xLen = (float)(x2 - x1);
      var yLen = (float)(y2 - y1);
      var accuracyX = xLen / interestedPixelsX;
      var accuracyY = yLen / interestedPixelsY;
      for (var c1 = -1f; c1 <= 1; c1 += accuracyX)
      {
        for (var c2 = -1f; c2 <= 1; c2 += accuracyY)
        {
          #region Check is in sphere

          if ((c1 * c1 + c2 * c2) > 1)
            continue;

          #endregion

          var c0 = (float)Math.Sqrt(RGCircleRadius - c1 * c1 - c2 * c2);

          var cPoint = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
          var rgPoint = cPoint.RGPoint;

          var xLocal = (c1 + Math.Abs(x1)) / accuracyX;
          var xGlobal = CircleRect.X + xLocal;

          var yLocal = (c2 + Math.Abs(y1)) / accuracyY;
          var yGlobal = CircleRect.Y + (CircleRect.Height - yLocal);

          if (rgPoint.G < 0)
          {
            image.SetPixel((int)xGlobal, (int)yGlobal, Color.Green);
          }

        }
      }

      gr.Save();
    }

    public void ZoomIn(Rectangle selectedArea)
    {
      var areaRange = (AreaSize.Width * 1.0 / AreaSize.Height);
      var rectRange = (selectedArea.Width * 1.0 / selectedArea.Height);
      if (rectRange > 1)
      {
        if (areaRange > rectRange)
        {
          selectedArea.Width = (int)(selectedArea.Width * areaRange);
        }
        else
        {
          selectedArea.Width = (int)(selectedArea.Width / areaRange);
        }
      }
      else
      {
        if (areaRange < rectRange)
        {
          selectedArea.Height = (int)(selectedArea.Height / areaRange);
        }
        else
        {
          selectedArea.Height = (int)(selectedArea.Height * areaRange);
        }
      }

      var onePixelXWidth = Frame.PlainRect.Width / AreaSize.Width;
      var onePixelYWidth = Frame.PlainRect.Height / AreaSize.Height;

      var xl = Frame.PlainRect.X + selectedArea.X * onePixelXWidth;
      var xW = selectedArea.Width * onePixelXWidth;

      var yd = selectedArea.Y * onePixelYWidth + selectedArea.Height * onePixelYWidth;
      var yl = Frame.PlainRect.Y + Frame.PlainRect.Height - yd;
      var yW = selectedArea.Height * onePixelYWidth;

      var frame = new RGFrame { PlainRect = new RectangleF(xl, yl, xW, yW) };
      var image = new Bitmap(AreaSize.Width, AreaSize.Height);

      var accuracyX = frame.PlainRect.Width / AreaSize.Width;
      var accuracyY = frame.PlainRect.Height / AreaSize.Height;

      var i = 0;
      for (var c1 = frame.PlainRect.X; c1 <= frame.PlainRect.X + frame.PlainRect.Width; c1 += accuracyX)
      {
        var j = AreaSize.Height - 1;
        for (var c2 = frame.PlainRect.Y; c2 <= frame.PlainRect.Y + frame.PlainRect.Height; c2 += accuracyY)
        {
          if (i < 0 || j < 0 || i >= AreaSize.Width || j >= AreaSize.Height)
            continue;

          if ((c1 * c1 + c2 * c2) > 1)
          {
            image.SetPixel(i, j, Color.White);
          }
          else
          {
            image.SetPixel(i, j, Color.Yellow);

            var c0 = (float)Math.Sqrt(RGCircleRadius - c1 * c1 - c2 * c2);
            var cPoint = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
            var rgPoint = cPoint.RGPoint;
            if (rgPoint.G < 0)
            {
              image.SetPixel(i, j, Color.Green);
            }

            if (Math.Abs(c1 - 0) < Epsilon || Math.Abs(c2 - 0) < Epsilon)
            {
              image.SetPixel(i, j, Color.Black);
            }
          }
          j--;
        }
        i++;
      }

      frame.Image = image;
      Frame = frame;
      Frames.Push(frame);
    }

    public void ZoomOut()
    {
      Frames.Pop();
      var peek = Frames.Peek();
      Frame = peek;
    }
  }
}
