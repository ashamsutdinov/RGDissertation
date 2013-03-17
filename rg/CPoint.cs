using System;
using System.Collections.Generic;

namespace ReverseTransform
{
  public class CPoint
  {
    public double C0;

    public double C1;

    public double C2;

    private void Build(double c0, double c1, double c2)
    {
      C0 = c0;
      C1 = c1;
      C2 = c2;
      var norm = Norm;
      if (!(norm > 0))
        return;
      C0 = C0 / norm;
      C1 = C1 / norm;
      C2 = C2 / norm;
    }

    private void ToUpperSphere()
    {
      if (!(C2 < 0)) 
        return;
      C0 = -C0;
      C1 = -C1;
      C2 = -C2;
    }

    public CPoint(CPoint src)
      : this(src.C0, src.C1, src.C2)
    {
    }

    public CPoint(double c0, double c1, double c2)
    {
      Build(c0, c1, c2);
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

    public double DistanceTo(CPoint pt)
    {
      var c0S = C0 - pt.C0;
      var c1S = C1 - pt.C1;
      var c2S = C2 - pt.C2;
      var d1 = c0S * c0S + c1S * c1S + c2S * c2S;
      var dist1 = Math.Sqrt(d1);

      var ptd = pt.Diametral;
      c0S = C0 - ptd.C0;
      c1S = C1 - ptd.C1;
      c2S = C2 - ptd.C2;
      var d2 = c0S * c0S + c1S * c1S + c2S * c2S;
      var dist2 = Math.Sqrt(d2);

      var dist = Math.Min(dist1, dist2);
      return dist;
    }

    public override string ToString()
    {
      return string.Format("({0};{1};{2})", C0, C1, C2);
    }

    public static double Lambda = 0;

    public static double LambdaMinus1 = 0;

    public static double LambdaMinus2 = 0;

    public static double NLambdaMinus2 = 0;

    public CPoint DirectIterated(double alpha, double n)
    {
      var c1MinusC0 = C1 - C0;
      var c2MunusC1 = C2 - C1;
      var c0C2MinusC1Sq = C0 * C2 - C1 * C1;
      var n1 = 1 / n;

      var c0 = (c1MinusC0 * c1MinusC0 + n1 * c0C2MinusC1Sq);
      var c1 = Lambda * (c1MinusC0 * c2MunusC1 + n1 * c0C2MinusC1Sq);
      var c2 = Lambda * Lambda * (c2MunusC1 * c2MunusC1 + n1 * c0C2MinusC1Sq);

      var res = new CPoint(c0, c1, c2);
      return res;
    }

    public CPoint ReverseIterated(double alpha, double n)
    {
      var c0C2MinusC1 = C0 * C2 - C1 * C1;
      var c0MinusLambdaMinus1C1 = C0 - LambdaMinus1 * C1;
      var c1MinusLambdaMinus1C2 = C1 - LambdaMinus1 * C2;

      var c0 = c0MinusLambdaMinus1C1 * c0MinusLambdaMinus1C1 + NLambdaMinus2 * c0C2MinusC1;
      var c1 = LambdaMinus1 * c0MinusLambdaMinus1C1 * c1MinusLambdaMinus1C2 + NLambdaMinus2 * c0C2MinusC1;
      var c2 = LambdaMinus2 * c1MinusLambdaMinus1C2 * c1MinusLambdaMinus1C2 + NLambdaMinus2 * c0C2MinusC1;

      var res = new CPoint(c0, c1, c2);
      return res;
    }

    public void IterateReverse(double alpha, double n)
    {
      var c0C2MinusC1 = C0 * C2 - C1 * C1;
      var c0MinusLambdaMinus1C1 = C0 - LambdaMinus1 * C1;
      var c1MinusLambdaMinus1C2 = C1 - LambdaMinus1 * C2;

      var c0 = c0MinusLambdaMinus1C1 * c0MinusLambdaMinus1C1 + NLambdaMinus2 * c0C2MinusC1;
      var c1 = LambdaMinus1 * c0MinusLambdaMinus1C1 * c1MinusLambdaMinus1C2 + NLambdaMinus2 * c0C2MinusC1;
      var c2 = LambdaMinus2 * c1MinusLambdaMinus1C2 * c1MinusLambdaMinus1C2 + NLambdaMinus2 * c0C2MinusC1;

      Build(c0, c1, c2);
    }

    public IEnumerable<CPoint> DirectTrack(CPoint crit, double alpha, double n, double acc, int k)
    {
      var pt = this;
      const double max = double.MaxValue;
      var d = max;
      var i = 0;
      var res = new List<CPoint>();
      while (d > acc && i < k)
      {
        res.Add(pt);
        pt = pt.DirectIterated(alpha, n);
        d = pt.DistanceTo(crit);
        i++;
      }
      return res;
    }

    public List<CPoint> ReverseTrack(CPoint crit, double alpha, double n, double acc, int k)
    {
      var pt = this;
      const double max = double.MaxValue;
      var d = max;
      var i = 0;
      var res = new List<CPoint>();
      while (d > acc && i < k)
      {
        pt.ToUpperSphere();
        res.Add(pt);
        pt = pt.ReverseIterated(alpha, n);
        d = pt.DistanceTo(crit);
        i++;
      }
      return res;
    }

    public CPoint ReverseTrackEndPoint(CPoint crit, double alpha, double n, out int cnt, double acc, int k)
    {
      var pt = this;
      const double max = double.MaxValue;
      var d = max;
      var i = 0;
      while (d > acc && i < k)
      {
        pt.IterateReverse(alpha, n);
        d = pt.DistanceTo(crit);
        i++;
      }
      cnt = i;
      return pt;
    }
  }
}