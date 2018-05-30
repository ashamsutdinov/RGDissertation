using System;
using System.Collections.Generic;

namespace RenormGroups
{
  public class CIterator
  {
    public CPoint IterateDirect(CPoint pt, double n, double alpha)
    {
      var lambda = Math.Pow(n, alpha - 1);

      var c1MinusC0 = pt.C1 - pt.C0;
      var c2MunusC1 = pt.C2 - pt.C1;
      var c0C2MinusC1Sq = pt.C0 * pt.C2 - pt.C1 * pt.C1;
      var n1 = 1 / n;

      var c0 = c1MinusC0 * c1MinusC0 + n1 * c0C2MinusC1Sq;
      var c1 = lambda * (c1MinusC0 * c2MunusC1 + n1 * c0C2MinusC1Sq);
      var c2 = lambda * lambda * (c2MunusC1 * c2MunusC1 + n1 * c0C2MinusC1Sq);

      var res = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
      res = res.Normalized as CPoint;
      if (res != null)
      {
        //var diam = res.Diametral as CPoint;
        //var dr = pt.DistanceTo(res);
        //var dd = pt.DistanceTo(diam);
        //if (dd < dr)
        //{
        //  res = diam;
        //}
        return res;
      }
      throw new ArgumentException();
    }

    public CPoint IterateDirect(CPoint pt, double n, double alpha, int iterations)
    {
      var res = pt;
      for (var i = 0; i <= iterations; i++)
      {
        res = IterateDirect(res, n, alpha);
      }
      return res;
    }

    public CPoint IterateReverse(CPoint point, double n, double alpha)
    {
      var lambda = Math.Pow(n, alpha - 1);
      var lambdaMinus1 = 1 / lambda;
      var lambdaMinus2 = lambdaMinus1 / lambda;
      var nLambdaMinus2 = n * lambdaMinus2;
      var c0C2MinusC1 = point.C0 * point.C2 - point.C1 * point.C1;
      var c0MinusLambdaMinus1C1 = point.C0 - lambdaMinus1 * point.C1;
      var c1MinusLambdaMinus1C2 = point.C1 - lambdaMinus1 * point.C2;

      var c0 = c0MinusLambdaMinus1C1 * c0MinusLambdaMinus1C1 + nLambdaMinus2 * c0C2MinusC1;
      var c1 = lambdaMinus1 * c0MinusLambdaMinus1C1 * c1MinusLambdaMinus1C2 + nLambdaMinus2 * c0C2MinusC1;
      var c2 = lambdaMinus2 * c1MinusLambdaMinus1C2 * c1MinusLambdaMinus1C2 + nLambdaMinus2 * c0C2MinusC1;

      var res = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
      res = res.Normalized as CPoint;
      return res;
    }

    public CPoint IterateReverse(CPoint pt, double n, double alpha, int iterations)
    {
      var res = pt;
      for (var i = 0; i <= iterations; i++)
      {
        res = IterateReverse(res, n, alpha);
      }
      return res;
    }

    public IEnumerable<CPoint> TrackPointDirect(CPoint point, int iterations, double n, double alpha)
    {
      var pt = point.Normalized as CPoint;
      for (var i = 0; i <= iterations; i++)
      {
        yield return pt;
        pt = IterateDirect(pt, n, alpha);
      }
    }

    public IEnumerable<CPoint> TrackPointReverse(CPoint point, int iterations, double n, double alpha)
    {
      var pt = point;
      for (var i = 0; i <= iterations; i++)
      {
        yield return pt;
        pt = IterateReverse(pt, n, alpha);
      }
    }
  }
}
