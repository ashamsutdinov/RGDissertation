using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseTransform
{
  public class RGPoint
  {
    public double R;

    public double G;

    public CPoint CReversed
    {
      get
      {
        return new CPoint(R * R - G, -R, 1);
      }
    }

    public CPoint CDirect
    {
      get
      {
        return new CPoint(1, -R, R * R - G);
      }
    }

    private static RGPoint ParabolaPoint(double r, double a, double b, double l1)
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

    public static IEnumerable<RGPoint> ParabolaReversed(double a, double b, double rmin, double rmax, double rstep, bool beforeTrans = true)
    {
      var l1 = RGSettings.LambdaMinus1;
      if (!beforeTrans)
      {
        l1 = 1;
      }

      for (var r = rmin; r <= rmax; r += rstep)
      {
        yield return ParabolaPoint(r, a, b, l1);
      }
    }

    public static IEnumerable<RGPoint> ParabolaDirect(double a, double b, double rmin, double rmax, double rstep, bool beforeTrans = true)
    {
      var l1 = 1d;
      if (!beforeTrans)
      {
        l1 = RGSettings.Lambda;
      }

      for (var r = rmin; r <= rmax; r += rstep)
      {
        yield return ParabolaPoint(r, a, b, l1);
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetReversedTriangleNegative(double c1Lim, double xacc, double yacc)
    {
      xacc = xacc / 3;
      yacc = yacc / 3;
      for (var x = c1Lim; x <= 1.0; x += xacc)
      {
        for (var y = -1.0; y <= 0; y += yacc)
        {
          var c0 = x;
          var c1 = y;
          var rd = c0 * c0 + c1 * c1;
          var c2 = 1 - rd;
          if (rd <= 1)
          {
            c2 = Math.Sqrt(c2);
            var cpt = new CPoint(c0, c1, c2);
            var rg = cpt.RGReversed;
            if (rg.G >= 0)
            {
              yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
            }
          }
        }
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetReversedTrianglePositive(double c1Lim, double xacc, double yacc)
    {
      xacc = xacc / 3;
      yacc = yacc / 3;
      for (var x = c1Lim; x <= 1.0; x += xacc)
      {
        for (var y = 0.0; y <= 1.0; y += yacc)
        {
          var c0 = x;
          var c1 = y;
          var rd = c0 * c0 + c1 * c1;
          var c2 = 1 - rd;
          if (rd <= 1)
          {
            c2 = Math.Sqrt(c2);
            var cpt = new CPoint(c0, c1, c2);
            var rg = cpt.RGReversed;
            if (rg.G >= 0)
            {
              yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
            }
          }
        }
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetReversedTriangleArcNegative(double c1Lim, double xacc)
    {
      xacc = xacc / 100;
      for (var x = c1Lim; x <= 1.0; x += xacc)
      {
        var c0 = x;
        var c1 = -Math.Sqrt(1 - Math.Pow(c0, 2));
        var rd = c0 * c0 + c1 * c1;
        var c2 = 1 - rd;
        if (rd <= 1)
        {
          c2 = Math.Sqrt(c2);
          var cpt = new CPoint(c0, c1, c2);
          var rg = cpt.RGReversed;
          if (rg.G >= 0)
          {
            yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
          }
        }
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetReversedTriangleArcPositive(double c1Lim, double xacc)
    {
      xacc = xacc / 100;
      for (var x = c1Lim; x <= 1.0; x += xacc)
      {
        var c0 = x;
        var c1 = Math.Sqrt(1 - Math.Pow(c0, 2));
        var rd = c0 * c0 + c1 * c1;
        var c2 = 1 - rd;
        if (rd <= 1)
        {
          c2 = Math.Sqrt(c2);
          var cpt = new CPoint(c0, c1, c2);
          var rg = cpt.RGReversed;
          if (rg.G >= 0)
          {
            yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
          }
        }
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetDirectTriangleNegative(double c1Lim, double xacc, double yacc)
    {
      xacc = xacc / 3;
      yacc = yacc / 3;
      for (var x = -1.0; x <= 0; x += xacc)
      {
        for (var y = c1Lim; y <= 1.0; y += yacc)
        {
          var c1 = x;
          var c2 = y;
          var rd = c1 * c1 + c2 * c2;
          var c0 = 1 - rd;
          if (rd <= 1)
          {
            c0 = Math.Sqrt(c0);
            var cpt = new CPoint(c0, c1, c2);
            var rg = cpt.RGDirect;
            if (rg.G >= 0)
            {
              yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
            }
          }
        }
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetDirectTrianglePositive(double c1Lim, double xacc, double yacc)
    {
      xacc = xacc / 3;
      yacc = yacc / 3;
      for (var x = 0.0; x <= 1.0; x += xacc)
      {
        for (var y = c1Lim; y <= 1.0; y += yacc)
        {
          var c1 = x;
          var c2 = y;
          var rd = c1 * c1 + c2 * c2;
          var c0 = 1 - rd;
          if (rd <= 1)
          {
            c0 = Math.Sqrt(c0);
            var cpt = new CPoint(c0, c1, c2);
            var rg = cpt.RGDirect;
            if (rg.G >= 0)
            {
              yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
            }
          }
        }
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetDirectTriangleArcNegative(double c1Lim, double yacc)
    {
      yacc = yacc / 100;
      for (var y = c1Lim; y <= 1; y += yacc)
      {
        var c2 = y;
        var c1 = -Math.Sqrt(1 - Math.Pow(c2, 2));
        var rd = c1 * c1 + c2 * c2;
        var c0 = 1 - rd;
        if (rd <= 1)
        {
          c0 = Math.Sqrt(c0);
          var cpt = new CPoint(c0, c1, c2);
          var rg = cpt.RGDirect;
          if (rg.G >= 0)
          {
            yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
          }
        }
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetDirectTriangleArcPositive(double c1Lim, double yacc)
    {
      yacc = yacc / 100;
      for (var y = c1Lim; y <= 1; y += yacc)
      {
        var c2 = y;
        var c1 = Math.Sqrt(1 - Math.Pow(c2, 2));
        var rd = c1 * c1 + c2 * c2;
        var c0 = 1 - rd;
        if (rd <= 1)
        {
          c0 = Math.Sqrt(c0);
          var cpt = new CPoint(c0, c1, c2);
          var rg = cpt.RGDirect;
          if (rg.G >= 0)
          {
            yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
          }
        }
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> ReverseIterated(IEnumerable<CPoint> points)
    {
      return points.Select(cPoint => cPoint.ReverseIterated()).Select(cpt => new KeyValuePair<CPoint, RGPoint>(cpt, cpt.RGReversed));
    }

    public static IEnumerable<KeyValuePair<List<CPoint>, List<RGPoint>>> ReverseIteratedMany(IEnumerable<CPoint> points, int count)
    {
      var lst = new List<KeyValuePair<List<CPoint>, List<RGPoint>>>();
      var curIterated = ReverseIterated(points).ToList();
      lst.Add(
        new KeyValuePair<List<CPoint>, List<RGPoint>>(
          curIterated.Select(e => e.Key).ToList(),
          curIterated.Select(e => e.Value).ToList()
          )
        );
      for (var i = 2; i <= count; i++)
      {
        curIterated = ReverseIterated(curIterated.Select(c => c.Key)).ToList();
        lst.Add(
        new KeyValuePair<List<CPoint>, List<RGPoint>>(
          curIterated.Select(e => e.Key).ToList(),
          curIterated.Select(e => e.Value).ToList()
          )
        );
      }
      return lst;
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> DirectIterated(IEnumerable<CPoint> points)
    {
      return points.Select(cPoint => cPoint.DirectIterated()).Select(cpt => new KeyValuePair<CPoint, RGPoint>(cpt, cpt.RGDirect));
    }

    public static IEnumerable<KeyValuePair<List<CPoint>, List<RGPoint>>> DirectIteratedMany(IEnumerable<CPoint> points, int count)
    {
      var lst = new List<KeyValuePair<List<CPoint>, List<RGPoint>>>();
      var curIterated = DirectIterated(points).ToList();
      lst.Add(
        new KeyValuePair<List<CPoint>, List<RGPoint>>(
          curIterated.Select(e => e.Key).ToList(),
          curIterated.Select(e => e.Value).ToList()
          )
        );
      for (var i = 2; i <= count; i++)
      {
        curIterated = DirectIterated(curIterated.Select(c => c.Key)).ToList();
        lst.Add(
        new KeyValuePair<List<CPoint>, List<RGPoint>>(
          curIterated.Select(e => e.Key).ToList(),
          curIterated.Select(e => e.Value).ToList()
          )
        );
      }
      return lst;
    }

    public override string ToString()
    {
      return string.Format("({0};{1})", R, G);
    }
  }
}