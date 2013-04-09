using System;
using System.Collections.Generic;

namespace ReverseTransform
{
  public class RGPoint
  {
    public double R;

    public double G;

    public CPoint C
    {
      get
      {
        return new CPoint(R * R - G, -R, 1);
      }
    }

    private static double? _lambda;

    private static RGPoint ParabolaPt(double r, double a, double b, double l1)
    {
      var g = r;
      if (g - b == 0)
      {
        g = double.MaxValue;
      }
      else
      {
        g = ((r - a) / (r - b)) * Math.Pow(r + l1, 2);
      }
      return new RGPoint { R = r, G = g };
    }

    public static IEnumerable<RGPoint> Parabola(double alpha, double n, double a, double b, bool beforeTrans = true)
    {
      if (_lambda == null)
      {
        var l = Math.Pow(n, alpha - 1);
        _lambda = l;
      }
      var lambda = _lambda.Value;
      return Parabola(lambda, alpha,n,a,b,beforeTrans);
    }

    public static IEnumerable<RGPoint> Parabola(double lambda, double alpha, double n, double a, double b, bool beforeTrans = true)
    {
      return Parabola(lambda, alpha, n, a, b, -1000.0, 1000.0, 0.01, beforeTrans);
    }

    public static IEnumerable<RGPoint> Parabola(double lambda, double alpha, double n, double a, double b, double rmin, double rmax, double rstep, bool beforeTrans = true)
    {
      var l1 = Math.Pow(lambda, -1);
      if (!beforeTrans)
      {
        l1 = 1;
      }

      for (var r = rmin; r <= rmax; r += rstep)
      {
        yield return ParabolaPt(r, a, b, l1);
      }
    }

    public override string ToString()
    {
      return string.Format("({0};{1})", R, G);
    }
  }
}