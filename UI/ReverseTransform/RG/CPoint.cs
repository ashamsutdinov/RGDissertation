using System;
using System.Collections.Generic;

namespace ReverseTransform
{
  public class CPoint
  {
#if DECIMAL
    public decimal C0 { get; set; }

    public decimal C1 { get; set; }

    public decimal C2 { get; set; }
#else
    public double C0 { get; set; }

    public double C1 { get; set; }

    public double C2 { get; set; }
#endif

    public CPoint(
#if DECIMAL
decimal c0, decimal c1, decimal c2
#else
double c0, double c1, double c2
#endif
)
    {
      C0 = c0;
      C1 = c1;
      C2 = c2;
    }

#if DECIMAL
    public decimal Norm
#else
    public double Norm
#endif
    {
      get
      {
        var sum = C0 * C0 + C1 * C1 + C2 * C2;
#if DECIMAL
        var n = sum.SqrtB();
#else
        var n = Math.Sqrt(sum);
#endif
        return n;
      }
    }


    public RGPoint RG
    {
      get
      {
#if DECIMAL
        const decimal max = decimal.MaxValue;
#else
        const double max = double.MaxValue;
#endif
        try
        {
          var r = C0 > 0 || C0 < 0 ? -C1 / C0 : max;
          var g = C0 > 0 || C0 < 0 ? (C1 * C1 - C0 * C2) / (C0 * C0) : max;
          return new RGPoint { R = r, G = g };
        }
        catch (Exception)
        {
          return new RGPoint { R = max, G = max };
        }
      }
    }

    public CPoint Diametral
    {
      get { return new CPoint(-C0, -C1, -C2); }
    }

    public CPoint Normalized
    {
      get
      {
        var norm = Norm;
        if (norm > 0)
        {
          var c = new CPoint(C0 / norm, C1 / norm, C2 / norm);
          return c;
        }
        return this;
      }
    }

#if DECIMAL
    public decimal
#else
    public double
#endif
 DistanceTo(CPoint pt)
    {
      var c0S = C0 - pt.C0;
      var c1S = C1 - pt.C1;
      var c2S = C2 - pt.C2;
      var d = c0S * c0S + c1S * c1S + c2S * c2S;
#if DECIMAL
      var dist = d.SqrtB();
#else
      var dist = Math.Sqrt(d);
#endif
      return dist;
    }

    public override string ToString()
    {
      return string.Format("({0};{1};{2})", C0, C1, C2);
    }

    public bool IsNormal
    {
      get { return Norm <= 1; }
    }

#if DECIMAL
    private static decimal? _lambda;
#else
    private static double? _lambda;
#endif

    public CPoint DirectIterated(
#if DECIMAL
decimal alpha, decimal n
#else
double alpha, double n
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

      var c1MinusC0 = C1 - C0;
      var c2MunusC1 = C2 - C1;
      var c0C2MinusC1Sq = C0 * C2 - C1 * C1;
      var n1 = 1 / n;

      var c0 = (c1MinusC0 * c1MinusC0 + n1 * c0C2MinusC1Sq);
      var c1 = lambda * (c1MinusC0 * c2MunusC1 + n1 * c0C2MinusC1Sq);
      var c2 = lambda * lambda * (c2MunusC1 * c2MunusC1 + n1 * c0C2MinusC1Sq);

      var res = new CPoint(c0, c1, c2);
      var nres = res.Normalized;
      return nres;
    }

    public CPoint ReverseIterated(
#if DECIMAL
decimal alpha, decimal n
#else
double alpha, double n
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
      var lambdaMinus1 = 1 / lambda;
      var lambdaMinus2 = lambdaMinus1 / lambda;
      var nLambdaMinus2 = n * lambdaMinus2;
      var c0C2MinusC1 = C0 * C2 - C1 * C1;
      var c0MinusLambdaMinus1C1 = C0 - lambdaMinus1 * C1;
      var c1MinusLambdaMinus1C2 = C1 - lambdaMinus1 * C2;

      var c0 = c0MinusLambdaMinus1C1 * c0MinusLambdaMinus1C1 + nLambdaMinus2 * c0C2MinusC1;
      var c1 = lambdaMinus1 * c0MinusLambdaMinus1C1 * c1MinusLambdaMinus1C2 + nLambdaMinus2 * c0C2MinusC1;
      var c2 = lambdaMinus2 * c1MinusLambdaMinus1C2 * c1MinusLambdaMinus1C2 + nLambdaMinus2 * c0C2MinusC1;

      var res = new CPoint(c0, c1, c2);
      var nres = res.Normalized;
      return nres;
    }

    public IEnumerable<CPoint> DirectTrack(CPoint crit,
#if DECIMAL
 decimal alpha, decimal n, decimal acc = 0.0000001M,
#else
 double alpha, double n, double acc = (double)0.0000001,
#endif
 int k = 100)
    {
      var pt = this;
#if DECIMAL
      const decimal max = decimal.MaxValue;
#else
      const double max = double.MaxValue;
#endif
      var d = max;
      var i = 0;
      while (d > acc && i < k)
      {
        yield return pt;
        pt = pt.DirectIterated(alpha, n);
        d = pt.DistanceTo(crit);
        i++;
      }
    }

    public IEnumerable<CPoint> ReverseTrack(CPoint crit,
#if DECIMAL
 decimal alpha, decimal n, decimal acc = 0.0000001M,
#else
 double alpha, double n, double acc = (double)0.0000001,
#endif
 int k = 100)
    {
      var pt = this;
#if DECIMAL
      const decimal max = decimal.MaxValue;
#else
      const double max = double.MaxValue;
#endif
      var d = max;
      var i = 0;
      while (d > acc && i < k)
      {
        yield return pt;
        pt = pt.ReverseIterated(alpha, n);
        d = pt.DistanceTo(crit);
        i++;
      }
    }
  }
}