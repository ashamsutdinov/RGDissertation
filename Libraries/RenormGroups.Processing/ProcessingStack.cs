using System;
using System.Collections.Generic;
using System.Drawing;

namespace RenormGroups.Processing
{
  public abstract class ProcessingStack :
    Stack<ProcessingFrame>
  {
    public CSphereProjection Projection { get; protected set; }

    public ProcessingFrame CurrentFrame
    {
      get
      {
        return Count > 0 ? Peek() : null;
      }
    }

    public abstract void SelectCPointArea(ICPoint<double> point, double epsilon, Color color);

    public abstract void SelectCPoint(ICPoint<double> point, Color color);

    public abstract void DrawLine(ICPoint<double> point1, ICPoint<double> point2, Color color);

    protected void SelectCPointAreaBase(double xc, double yc, double epsilon, Color color)
    {
      var x1 = xc - epsilon;
      var x2 = xc + epsilon;
      var y1 = yc - epsilon;
      var y2 = yc + epsilon;

      var frame = CurrentFrame;
      var rect = frame.Rectangle;
      var image = frame.Image;

      var cx = Math.Abs(rect.X - x1) / rect.AccuracyX;
      var cw = Math.Abs(x2 - x1) / rect.AccuracyX;
      var cy = Math.Abs(rect.Y - y1) / rect.AccuracyY;
      var ch = Math.Abs(y2 - y1) / rect.AccuracyY;

      var gr = Graphics.FromImage(image);
      gr.DrawEllipse(new Pen(color), (float)cx, (float)cy, (float)cw, (float)ch);
      gr.Save();
    }

    protected void SelectCPointBase(double xc, double yc, Color color)
    {
      var frame = CurrentFrame;
      var rect = frame.Rectangle;
      var image = frame.Image;

      var cx = Math.Abs(rect.X - xc) / rect.AccuracyX;
      var cy = Math.Abs(rect.Y - yc) / rect.AccuracyY;

      image.SetPixel((int)cx, (int)cy, color);
    }

    protected void DrawLineBase(double x1, double x2, double y1, double y2, Color color)
    {
      var frame = CurrentFrame;
      var rect = frame.Rectangle;
      var image = frame.Image;

      var cx1 = Math.Abs(rect.X - x1) / rect.AccuracyX;
      var cy1 = Math.Abs(rect.Y - y1) / rect.AccuracyY;
      var cx2 = Math.Abs(rect.X - x2) / rect.AccuracyX;
      var cy2 = Math.Abs(rect.Y - y2) / rect.AccuracyY;

      var gr = Graphics.FromImage(image);
      gr.DrawLine(new Pen(color), (float)cx1, (float)cy1, (float)cx2, (float)cy2);
      gr.Save();
    }
  }
}