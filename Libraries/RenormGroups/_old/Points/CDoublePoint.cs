using System;
using System.Diagnostics.Contracts;
using Old.RenormGroups.Points.Interfaces;

namespace Old.RenormGroups.Points
{
  public class CDoublePoint : CPointBase<double>, ICDoublePoint
  {
    #region Overrides of PointBase<double,double>

    public override IPoint<double, double> Normalized
    {
      get
      {
        var n = Norm;
        return new CDoublePoint { C0 = C0 / n, C1 = C1 / n, C2 = C2 / n };
      }
    }

    public override double Norm
    {
      get
      {
        var c0 = C0 * C0;
        var c1 = C1 * C1;
        var c2 = C2 * C2;
        var sm = c0 + c1 + c2;
        var norm = Math.Sqrt(sm);
        return norm;
      }
    }

    public override double DistanceTo(IPoint<double, double> pt)
    {
      Contract.Requires(pt is CDoublePoint);
      var cpt = pt as CDoublePoint;
      if (cpt != null)
      {
        var c0 = C0 - cpt.C0;
        var c1 = C1 - cpt.C1;
        var c2 = C2 - cpt.C2;
        var sm = c0 * c0 + c1 * c1 + c2 * c2;
        var dist = Math.Sqrt(sm);
        return dist;
      }
      return double.MaxValue;
    }

    public override double ScalarWith(IPoint<double, double> pt)
    {
      Contract.Requires(pt is CDoublePoint);
      var cpt = pt as CDoublePoint;
      if (cpt != null)
      {
        var c0 = C0 * cpt.C0;
        var c1 = C1 * cpt.C1;
        var c2 = C2 * cpt.C2;
        var sm = c0 + c1 + c2;
        var sc = Math.Sqrt(sm);
        return sc;
      }
      return double.MaxValue;
    }

    public override double AngleWith(IPoint<double, double> pt)
    {
      return 0;
    }

    #endregion
  }
}