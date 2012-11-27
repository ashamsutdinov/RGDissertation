using System;
using System.Diagnostics.Contracts;
using Old.RenormGroups.Points.Interfaces;

namespace Old.RenormGroups.Points
{
  public class RGDoublePoint : RGPointBase<double>, IRGDoublePoint
  {
    #region Overrides of PointBase<double,double>

    public override IPoint<double, double> Normalized
    {
      get
      {
        var rgp = new RGDoublePoint {R = R/Norm, G = G/Norm};
        return rgp;
      }
    }

    public override double Norm
    {
      get
      {
        var r2 = R*R;
        var g2 = G*G;
        var s = r2 + g2;
        var res = Math.Sqrt(s);
        return res;
      }
    }

    public override double DistanceTo(IPoint<double, double> pt)
    {
      Contract.Requires(pt is RGDoublePoint);
      var rgPt = pt as RGDoublePoint;
      return 0;
    }

    public override double ScalarWith(IPoint<double, double> pt)
    {
      throw new NotImplementedException();
    }

    public override double AngleWith(IPoint<double, double> pt)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}