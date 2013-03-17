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

    public static IEnumerable<RGPoint> Parabola(double alpha, double n, double a, double b)
    {
      if (_lambda == null)
      {
        var l = Math.Pow(n, alpha - 1);
        _lambda = l;
      }
      var lambda = _lambda.Value;
      var l1 = Math.Pow(lambda, -1);

      for (var r = -100.0; r <= 100.0; r += 0.01)
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
        yield return new RGPoint { R = r, G = g };
      }
    }

    public override string ToString()
    {
      return string.Format("({0};{1})", R, G);
    }
  }
}