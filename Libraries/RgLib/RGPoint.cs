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

    private static RGPoint ParabolaPtReversed(double r, double a, double b, double l1)
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

    public static IEnumerable<RGPoint> ParabolaReversed(double a, double b, bool beforeTrans = true)
    {
      return ParabolaReversed(a, b, -1000.0, 1000.0, 0.01, beforeTrans);
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
        yield return ParabolaPtReversed(r, a, b, l1);
      }
    }

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetPositiveNumbersGreaterThanC1ProjC0C1Negative(double c1Lim, double xacc, double yacc)
    {
      xacc = xacc / 1.2;
      yacc = yacc / 1.2;
      for (var x = c1Lim; x <= 1.1; x += xacc)
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

    public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetPositiveNumbersGreaterThanC1ProjC0C1Positive(double c1Lim, double xacc, double yacc)
    {
      xacc = xacc / 1.2;
      yacc = yacc / 1.2;
      for (var x = c1Lim; x <= 1.1; x += xacc)
      {
        for (var y = 0.0; y <= 1.1; y += yacc)
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

    public override string ToString()
    {
      return string.Format("({0};{1})", R, G);
    }
  }
}