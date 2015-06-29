using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
  public abstract class ProcessingStack :
    Stack<ProcessingFrame>
  {
    #region Public properties

    public ClientGeometry Geometry { get; set; }

    public CSphereProjection Projection { get; protected set; }

    public List<ProcessingAction> ProcessedActions { get; private set; }

    public ProcessingFrame CurrentFrame
    {
      get
      {
        return Count > 0 ? Peek() : null;
      }
    }

    #endregion

    #region Constructors

    protected ProcessingStack()
    {
      ProcessedActions = new List<ProcessingAction>();
    }

    #endregion

    #region Public members

    public abstract void SelectCPointArea(ICPoint<double> point, double epsilon, Color color);

    public abstract void SelectCPoint(ICPoint<double> point, Color color);

    public abstract void DrawLine(ICPoint<double> point1, ICPoint<double> point2, Color colorLine, Color colorPoint);

    public abstract void DrawPoint(ICPoint<double> pt, Color color);

    public abstract void DrawInitialFigure();

    public abstract CPoint GetCPoint(Point pt);

    public Image DrawRectangle(Rectangle rect, Color color)
    {
      var image = CurrentFrame.ActualImage.Clone() as Image;
      if (image != null)
      {
        var gr = Graphics.FromImage(image);
        gr.DrawRectangle(new Pen(color), rect);
        gr.Save();
      }
      return image;
    }

    public void ZoomRectangle(Rectangle rect)
    {
      CreateNewFrame(rect, false);
      ZoomToCurrentRectangle();
      DoAllActions();
    }

    public void DoAllActions()
    {
      if (ProcessedActions.Any())
      {
        CurrentFrame.ProcessedImage = CurrentFrame.Image.Clone() as Bitmap;
      }
      foreach (var processingAction in ProcessedActions)
      {
        processingAction.Apply(this);
      }
    }

    public void DoAllSimpleActions()
    {
      foreach (var processingAction in ProcessedActions.Where(pa => pa.IsSimple))
      {
        processingAction.Apply(this);
      }
    }

    public ProcessingFrame ZoomOut()
    {
      if (CurrentFrame != null && Count > 1)
      {
        Pop();
      }
      DoAllSimpleActions();
      return CurrentFrame;
    }

    public ProcessingFrame CreateNewFrame(Rectangle selectedRectangle, bool isNew = true)
    {
      var frame = new ProcessingFrame { Image = new Bitmap(Geometry.Rectangle.Width, Geometry.Rectangle.Height) };
      var resultedRectD = new RectangleD();
      if (isNew)
      {
        var centerX = selectedRectangle.Width / 2.0;
        var centerY = selectedRectangle.Height / 2.0;
        var clientRadius = Math.Min(centerX, centerY) * Config.CircleFillFactor;
        var onePixel = Config.CircleRadius / clientRadius;
        var x = -centerX * onePixel;
        var w = selectedRectangle.Width * onePixel;
        var y = -centerY * onePixel;
        var h = selectedRectangle.Height * onePixel;
        resultedRectD.X = x;
        resultedRectD.Y = y;
        resultedRectD.Width = w;
        resultedRectD.Height = h;
        Geometry.ClientCircleRadius = clientRadius;
        Geometry.ClientCircleX = centerX - clientRadius;
        Geometry.ClientCircleY = centerY - clientRadius;
        resultedRectD.AccuracyX = resultedRectD.Width / selectedRectangle.Width;
        resultedRectD.AccuracyY = resultedRectD.Height / selectedRectangle.Height;
      }
      else
      {
        var currentRect = CurrentFrame.Rectangle;
        var clientXOffset = selectedRectangle.X;
        var clientYOffset = selectedRectangle.Y;
        var clientWidth = selectedRectangle.Width;
        var clientHeight = selectedRectangle.Height;
        resultedRectD.X = currentRect.X + clientXOffset * currentRect.AccuracyX;
        resultedRectD.Y = currentRect.Y + clientYOffset * currentRect.AccuracyY;
        resultedRectD.Width = clientWidth * currentRect.AccuracyX;
        resultedRectD.Height = clientHeight * currentRect.AccuracyY;
        resultedRectD.AccuracyX = resultedRectD.Width / Geometry.Rectangle.Width;
        resultedRectD.AccuracyY = resultedRectD.Height / Geometry.Rectangle.Height;
      }
      frame.Rectangle = resultedRectD;
      frame.ClientRectangle = Geometry.Rectangle;
      Push(frame);
      return frame;
    }

    #endregion

    #region Protected members

    protected abstract Color GetPointColor(double x, double y);

    protected virtual Color GetPointColor(CPoint point)
    {
      var rgPoint = point.RGPoint;
      if (rgPoint.G < 0)
        return Config.GNegativeFillColor;

      return Config.GPositiveFillColor;
    }

    protected void ZoomToCurrentRectangle()
    {
      var frame = CurrentFrame;
      var rect = frame.Rectangle;
      var image = frame.Image;
      var clientRect = frame.ClientRectangle;

      var i = 0;
      for (var x = rect.X; x <= rect.X + rect.Width; x += rect.AccuracyX)
      {
        var j = 0;
        for (var y = rect.Y; y <= rect.Y + rect.Height; y += rect.AccuracyY)
        {
          lock (image)
          {
            var color = GetPointColor(x, y);
            if (i < clientRect.Width && j < clientRect.Height)
              image.SetPixel(i, j, color);
            j++;
          }
        }
        i++;
      }
    }

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

    public abstract CPoint GetCPoint(double x, double y);

    protected void DrawPointBase(double x, double y, Color color)
    {
      var frame = CurrentFrame;
      var rect = frame.Rectangle;
      var image = frame.ActualImage;

      lock (image)
      {
        var cx = Math.Abs(rect.X - x) / rect.AccuracyX;
        var cy = Math.Abs(rect.Y - y) / rect.AccuracyY;
        var gr = Graphics.FromImage(image);
        try
        {
          gr.FillEllipse(new SolidBrush(color), (float)(cx - 2), (float)(cy - 2), 4, 4);
        }
        catch
        {

        }
        gr.Save();
      }
    }

    protected void DrawLineBase(double x1, double x2, double y1, double y2, Color color, Color colorPoint)
    {
      var frame = CurrentFrame;
      var rect = frame.Rectangle;
      var image = frame.Image;

      lock (image)
      {
        var cx1 = Math.Abs(rect.X - x1) / rect.AccuracyX;
        var cy1 = Math.Abs(rect.Y - y1) / rect.AccuracyY;
        var cx2 = Math.Abs(rect.X - x2) / rect.AccuracyX;
        var cy2 = Math.Abs(rect.Y - y2) / rect.AccuracyY;

        var gr = Graphics.FromImage(image);
        gr.DrawLine(new Pen(color), (float)cx1, (float)cy1, (float)cx2, (float)cy2);
        try
        {
          gr.FillEllipse(new SolidBrush(colorPoint), (float)(cx1 - 2), (float)(cy1 - 2), 4, 4);
        }
        catch
        {

        }
        try
        {
          gr.FillEllipse(new SolidBrush(colorPoint), (float)(cx2 - 2), (float)(cy2 - 2), 4, 4);
        }
        catch
        {

        }
        gr.Save();
      }
    }

    #endregion
  }
}