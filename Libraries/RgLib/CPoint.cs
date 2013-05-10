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

    public void ToDiametral()
    {
      C0 = -C0;
      C1 = -C1;
      C2 = -C2;
    }

    public void ToUpperSphereC1C2()
    {
      if (!(C0 < 0))
        return;
      ToDiametral();
    }

    public void ToUpperSphereC0C2()
    {
      if (!(C1 < 0))
        return;
      ToDiametral();
    }

    private void ToUpperSphereC0C1()
    {
      if (!(C2 < 0))
        return;
      ToDiametral();
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

    public RGPoint RGReversed
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

    public RGPoint RGDirect
    {
      get
      {
        const double max = double.MaxValue;
        try
        {
          var r = C0 > 0 || C0 < 0 ? -C2 / C0 : max;
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

    public CPoint DirectIterated()
    {
      var c1MinusC0 = C1 - C0;
      var c2MunusC1 = C2 - C1;
      var c0C2MinusC1Sq = C0 * C2 - C1 * C1;
      var n1 = RGSettings.OneDivN;

      var c0 = (c1MinusC0 * c1MinusC0 + n1 * c0C2MinusC1Sq);
      var c1 = RGSettings.Lambda * (c1MinusC0 * c2MunusC1 + n1 * c0C2MinusC1Sq);
      var c2 = RGSettings.Lambda2 * (c2MunusC1 * c2MunusC1 + n1 * c0C2MinusC1Sq);

      var res = new CPoint(c0, c1, c2);
      res.ToUpperSphereC1C2();
      return res;
    }

    public CPoint ReverseIterated()
    {
      var c0C2MinusC1 = C0 * C2 - C1 * C1;
      var c0MinusLambdaMinus1C1 = C0 - RGSettings.LambdaMinus1 * C1;
      var c1MinusLambdaMinus1C2 = C1 - RGSettings.LambdaMinus1 * C2;

      var c0 = c0MinusLambdaMinus1C1 * c0MinusLambdaMinus1C1 + RGSettings.NLambdaMinus2 * c0C2MinusC1;
      var c1 = RGSettings.LambdaMinus1 * c0MinusLambdaMinus1C1 * c1MinusLambdaMinus1C2 + RGSettings.NLambdaMinus2 * c0C2MinusC1;
      var c2 = RGSettings.LambdaMinus2 * c1MinusLambdaMinus1C2 * c1MinusLambdaMinus1C2 + RGSettings.NLambdaMinus2 * c0C2MinusC1;

      var res = new CPoint(c0, c1, c2);
      res.ToUpperSphereC0C1();
      return res;
    }

    public void IterateReverse()
    {
      var c0C2MinusC1 = C0 * C2 - C1 * C1;
      var c0MinusLambdaMinus1C1 = C0 - RGSettings.LambdaMinus1 * C1;
      var c1MinusLambdaMinus1C2 = C1 - RGSettings.LambdaMinus1 * C2;

      var c0 = c0MinusLambdaMinus1C1 * c0MinusLambdaMinus1C1 + RGSettings.NLambdaMinus2 * c0C2MinusC1;
      var c1 = RGSettings.LambdaMinus1 * c0MinusLambdaMinus1C1 * c1MinusLambdaMinus1C2 + RGSettings.NLambdaMinus2 * c0C2MinusC1;
      var c2 = RGSettings.LambdaMinus2 * c1MinusLambdaMinus1C2 * c1MinusLambdaMinus1C2 + RGSettings.NLambdaMinus2 * c0C2MinusC1;

      Build(c0, c1, c2);
    }

    public IEnumerable<CPoint> DirectTrack(CPoint crit)
    {
      var pt = this;
      const double max = double.MaxValue;
      var d = max;
      var i = 0;
      var res = new List<CPoint>();
      while (d > Config.Acc && i < Config.Count)
      {
        res.Add(pt);
        pt = pt.DirectIterated();
        d = pt.DistanceTo(crit);
        i++;
      }
      return res;
    }

    public IEnumerable<CPoint> ReverseTrack(CPoint crit)
    {
      var pt = this;
      const double max = double.MaxValue;
      var d = max;
      var i = 0;
      var res = new List<CPoint>();
      while (d > Config.Acc && i < Config.Count)
      {
        pt.ToUpperSphereC0C1();
        res.Add(pt);
        pt = pt.ReverseIterated();
        d = pt.DistanceTo(crit);
        i++;
      }
      return res;
    }

    public CPoint ReverseTrackEndPoint(CPoint crit, out int cnt)
    {
      var pt = this;
      const double max = double.MaxValue;
      var d = max;
      var i = 0;
      while (d > Config.Acc && i < Config.Count)
      {
        pt.IterateReverse();
        pt.ToUpperSphereC0C1();
        d = pt.DistanceTo(crit);
        i++;
      }
      cnt = i;
      return pt;
    }
  }
}