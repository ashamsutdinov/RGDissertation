using System;
using System.Drawing;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
  public sealed class C0C1ProjectionProcessingStack :
    ProcessingStack
  {
    public C0C1ProjectionProcessingStack()
    {
      Projection = CSphereProjection.C0C1;
    }

    #region Overrides of ProcessingStack

    public override void SelectCPointArea(ICPoint<double> point, double epsilon, Color color)
    {
      var xc = point.C0;
      var yc = point.C1;
      SelectCPointAreaBase(xc, yc, epsilon, color);
    }

    public override void SelectCPoint(ICPoint<double> point, Color color)
    {
      if (point.C2 < 0)
        point = point.Diametral;
      var xc = point.C0;
      var yc = point.C1;
      SelectCPointBase(xc, yc, color);
    }

    public override void DrawLine(ICPoint<double> point1, ICPoint<double> point2, Color colorLine, Color colorPoint)
    {
      if (point1.C2 < 0)
        point1 = point1.Diametral;
      if (point2.C2 < 0)
        point2 = point2.Diametral;
      var x1 = point1.C0;
      var x2 = point2.C0;
      var y1 = point1.C1;
      var y2 = point2.C1;
      DrawLineBase(x1, x2, y1, y2, colorLine, colorPoint);
    }

    public override void DrawPoint(ICPoint<double> pt, Color color)
    {
      if (pt.C2 < 0)
        pt = pt.Diametral;
      var x = pt.C0;
      var y = pt.C1;
      DrawPointBase(x, y, color);
    }

    public override void DrawInitialFigure()
    {
      var frame = CurrentFrame;
      var rect = frame.Rectangle;

      var circleRadius = Config.CircleRadius;

      var i = Geometry.ClientCircleX;
      for (var x = -circleRadius; x <= circleRadius; x += rect.AccuracyX)
      {
        var j = Geometry.ClientCircleY - 1;
        for (var y = -circleRadius; y <= circleRadius; y += rect.AccuracyY)
        {
          lock (frame.Image)
          {
            j++;
            var c0 = x;
            var c1 = y;

            if (c0*c0 + c1*c1 > circleRadius)
              continue;

            var c2 = Math.Sqrt(1 - c0*c0 - c1*c1);
            var c = new CPoint {C0 = c0, C1 = c1, C2 = c2};
            var rg = c.RGPoint;

            if (rg.G < 0)
            {
              frame.Image.SetPixel((int) i, (int) j, Config.GNegativeFillColor);
            }
          }
        }
        i++;
      }
    }

    public override CPoint GetCPoint(Point pt)
    {
      var cpt = new CPoint();
      var frame = CurrentFrame;
      if (frame != null)
      {
        var x = pt.X * frame.Rectangle.AccuracyX + frame.Rectangle.X;
        var y = pt.Y * frame.Rectangle.AccuracyY + frame.Rectangle.Y;
        var sum = x * x + y * y;
        cpt.C0 = x;
        cpt.C1 = y;
        if (sum <= Config.CircleRadius)
        {
          cpt.C2 = Math.Sqrt(Config.CircleRadius - sum);
        }
      }
      return cpt;
    }

    protected override Color GetPointColor(double x, double y)
    {
      var c0 = x;
      var c1 = y;

      var sum = c0 * c0 + c1 * c1;
      if (sum > Config.CircleRadius)
        return Config.BackgroundColor;

      var c2 = Math.Sqrt(Config.CircleRadius - sum);

      var cPoint = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
      return GetPointColor(cPoint);
    }

    public override CPoint GetCPoint(double x, double y)
    {
      var c0 = x;
      var c1 = y;

      var sum = c0 * c0 + c1 * c1;
      if (sum > Config.CircleRadius)
      {
        return new CPoint {IsFake = true};
      }

      var c2 = Math.Sqrt(Config.CircleRadius - sum);

      var cPoint = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
      return cPoint;
    }

    #endregion
  }
}