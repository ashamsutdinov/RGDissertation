using System;
using System.Collections.Generic;

namespace ReverseTransform
{
  public class CPoint
  {
    public decimal C0 { get; set; }

    public decimal C1 { get; set; }

    public decimal C2 { get; set; }

    public CPoint(decimal c0, decimal c1, decimal c2)
    {
      C0 = c0;
      C1 = c1;
      C2 = c2;
    }

    public decimal Norm
    {
      get
      {
        var n = (C0 * C0 + C1 * C1 + C2 * C2).SqrtB();
        return n;
      }
    }

    public RGPoint RG
    {
      get
      {
        try
        {
          var r = C0 > 0 || C0 < 0 ? -C1/C0 : decimal.MaxValue;
          var g = C0 > 0 || C0 < 0 ? (C1*C1 - C0*C2)/(C0*C0) : decimal.MaxValue;
          return new RGPoint {R = r, G = g};
        }
        catch (Exception ex)
        {
          return new RGPoint {R = decimal.MaxValue, G = decimal.MaxValue};
        }
      }
    }

    public CPoint Diametral
    {
      get { return new CPoint (-C0, -C1, -C2); }
    }

    public CPoint Normalized
    {
      get
      {
        var norm = Norm;
        if (norm > 0)
        {
          var c = new CPoint (C0 / norm, C1 / norm, C2 / norm);
          return c;
        }
        return this;
      }
    }

    public decimal DistanceTo(CPoint pt)
    {
      var c0S = C0 - pt.C0;
      var c1S = C1 - pt.C1;
      var c2S = C2 - pt.C2;
      var d = c0S * c0S + c1S * c1S + c2S * c2S;
      var dist = d.SqrtB();
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

    private static decimal? _lambda;

    public CPoint DirectIterated(decimal alpha, decimal n)
    {
      if (_lambda == null)
      {
        var l = (decimal)Math.Pow((double) n, (double) alpha - 1);
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

      var res = new CPoint (c0, c1, c2);
      var nres = res.Normalized;
      return nres;
    }

    public CPoint ReverseIterated(decimal alpha, decimal n)
    {
      if (_lambda == null)
      {
        var l = (decimal)Math.Pow((double)n, (double)alpha - 1);
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

      var res = new CPoint (c0, c1, c2);
      var nres = res.Normalized;
      return nres;
    }

    public IEnumerable<CPoint> DirectTrack(CPoint crit, decimal alpha, decimal n, decimal acc = (decimal)0.0000001, int N = 100)
    {
      var pt = this;
      var d = decimal.MaxValue;
      var i = 0;
      while (d > acc && i < N)
      {
        yield return pt;
        pt = pt.DirectIterated(alpha, n);
        d = pt.DistanceTo(crit);
        i++;
      }
    }

    public IEnumerable<CPoint> ReverseTrack(CPoint crit, decimal alpha, decimal n, decimal acc = (decimal)0.0000001, int N = 100)
    {
      var pt = this;
      var d = decimal.MaxValue;
      var i = 0;
      while (d > acc && i < N)
      {
        yield return pt;
        pt = pt.ReverseIterated(alpha, n);
        d = pt.DistanceTo(crit);
        i++;
      }
    }
  }
}