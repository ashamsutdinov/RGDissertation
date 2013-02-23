using System;
using System.Collections.Generic;

namespace rg
{
  public class CPoint<T>
  {
    public T C0 { get; set; }

    public T C1 { get; set; }

    public T C2 { get; set; }
  }

  public class CPoint :
    CPoint<double>
  {
    public CPoint(double c0, double c1, double c2)
    {
      C0 = c0;
      C1 = c1;
      C2 = c2;
    }

    public double Norm
    {
      get
      {
        var sum = C0 * C0 + C1 * C1 + C2 * C2;
        var n = Math.Sqrt(sum);
        return n;
      }
    }


    public RGPoint RG
    {
      get
      {
        const double max = double.MaxValue;
        try
        {
          var r = C2 > 0 || C2 < 0 ? -C1 / C2 : max;
          var g = C2 > 0 || C2 < 0 ? (C1 * C1 - C0 * C2) / (C2 * C2) : max;
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

    public double DistanceTo(CPoint pt)
    {
      var c0S = C0 - pt.C0;
      var c1S = C1 - pt.C1;
      var c2S = C2 - pt.C2;
      var d = c0S * c0S + c1S * c1S + c2S * c2S;
      var dist = Math.Sqrt(d);
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

    private static double? _lambda;

    public CPoint DirectIterated(double alpha, double n)
    {
      if (_lambda == null)
      {
        var l = Math.Pow(n, alpha - 1);
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

    public CPoint ReverseIterated(double alpha, double n)
    {
      if (_lambda == null)
      {
        var l = Math.Pow(n, alpha - 1);
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

    public IEnumerable<CPoint> DirectTrack(CPoint crit, double alpha, double n, double acc = 0.0000001, int k = 100)
    {
      var pt = this;
      const double max = double.MaxValue;
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

    public IEnumerable<CPoint> ReverseTrack(CPoint crit, double alpha, double n, double acc = 0.0000001, int k = 100)
    {
      var pt = this;
      const double max = double.MaxValue;
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