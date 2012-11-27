using System;
using System.Drawing;

namespace RenormGroups
{
  /// <summary>
  /// “очка с координатами двойной точности
  /// </summary>
  public sealed class PointD
  {
    public double X { get; set; }

    public double Y { get; set; }

    public PointD()
    {

    }

    public PointD(Point pt)
    {
      X = pt.X;
      Y = pt.Y;
    }

    public PointD(PointF pt)
    {
      X = pt.X;
      Y = pt.Y;
    }

    public PointD(IRGPoint<double> pt)
    {
      X = pt.R;
      Y = pt.G;
    }

    public PointD(ICPoint<double> pt)
    {
      throw new NotImplementedException();
    }

    public Point Point
    {
      get { return new Point((int)X, (int)Y); }
    }

    public PointF PointF
    {
      get { return new PointF((float)X, (float)Y); }
    }

    public IRGPoint<double> RGPoint
    {
      get { return new RGPoint { R = X, G = Y }; }
    }

    public ICPoint<double> CPoint
    {
      get
      {
        var cPt = new CPoint { C1 = X, C2 = Y, C0 = Math.Sqrt(1 - X * X - Y * Y) };
        return cPt;
      }
    }
  }
}