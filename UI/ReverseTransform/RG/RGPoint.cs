using System;
using System.Collections.Generic;

namespace ReverseTransform
{
  public class RGPoint<T>
  {
    public T R { get; set; }

    public T G { get; set; }
  }

  public class RGPoint :
#if DECIMAL
    RGPoint<decimal>
#else
 RGPoint<double>
#endif
  {
    public CPoint C
    {
      get
      {
        return new CPoint(R * R - G, -R, 1).Normalized;
      }
    }

#if DECIMAL
    private static decimal? _lambda;
#else
    private static double? _lambda;
#endif

    public static IEnumerable<RGPoint> Parabola(
#if DECINAL
      decimal alpha, decimal n, decimal a, decimal b
#else
      double alpha, double n, double a, double b
#endif
      )
    {
      if (_lambda == null)
      {
        var l =
#if DECIMAL
 (decimal)
#endif
 Math.Pow((double)n, (double)alpha - 1);
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
          g = ((r - a)/(r - b))*Math.Pow(r + l1, 2);
        }
        yield return new RGPoint {R = r, G = g};
      }
    }
  }
}