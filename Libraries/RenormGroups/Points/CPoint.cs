using System;
using System.Collections.Generic;
using System.Linq;
using RenormGroups.Configuration;

namespace RenormGroups
{
  public class CPoint :
    ICPoint<double>
  {
    #region Overrides of CPoint<double>

    public double C0 { get; set; }

    public double C1 { get; set; }

    public double C2 { get; set; }

    public bool IsFake { get; set; }

    public double Norm
    {
      get
      {
        var n = Math.Sqrt(C0 * C0 + C1 * C1 + C2 * C2);
        return n;
      }
    }

    public IRGPoint<double> RGPoint
    {
      get
      {
        var r = -C1 / C0;
        var g = (C1 * C1 - C0 * C2) / (C0 * C0);
        return new RGPoint { R = r, G = g };
      }
    }

    public ICPoint<double> Diametral
    {
      get { return new CPoint { C0 = -C0, C1 = -C1, C2 = -C2 }; }
    }

    public ICPoint<double> Normalized
    {
      get
      {
        var norm = Norm;
        if (norm > 0)
        {
          var c = new CPoint { C0 = C0 / norm, C1 = C1 / norm, C2 = C2 / norm };
          return c;
        }
        return this;
      }
    }

    public double DistanceTo(ICPoint<double> pt)
    {
      var c0S = C0 - pt.C0;
      var c1S = C1 - pt.C1;
      var c2S = C2 - pt.C2;
      var d = c0S * c0S + c1S * c1S + c2S * c2S;
      var dist = Math.Sqrt(d);
      return dist;
    }

    #endregion

    public override string ToString()
    {
      return string.Format(Config.CFormat, C0, C1, C2);
    }

    public bool IsLeftHandOf(CPoint pt, IEnumerable<CPoint> track)
    {
      var orientir = track.FirstOrDefault() ?? pt;
      return C1 - orientir.C1 > 0;
    }

    public bool IsRightHandOf(CPoint pt, IEnumerable<CPoint> track)
    {
      var orientir = track.FirstOrDefault() ?? pt;
      return C1 - orientir.C1 < 0;
    }
  }
}
