using System;
using System.Diagnostics.Contracts;
using RenormGroups._Points.Interfaces;

namespace RenormGroups._Points
{
  public class CPointDouble : CPointBase<double>, ICPointDouble
  {
    public override double DistanceTo(IPoint<double> pt)
    {
      // ReSharper disable InvocationIsSkipped
      Contract.Requires(pt is CPointDouble);
      // ReSharper restore InvocationIsSkipped
      var dPt = pt as CPointDouble;
      if (dPt != null)
      {
        var d0 = C0 - dPt.C0;
        var d1 = C1 - dPt.C1;
        var d2 = C2 - dPt.C2;
        return Math.Sqrt(d0 * d0 + d1 * d1 + d2 * d2);
      }
      throw new NotSupportedException();
    }

    public override IPoint Normalized
    {
      get
      {
        var norm = Math.Sqrt(C0 * C0 + C1 * C1 + C2 * C2);
        return norm > 0 ? new CPointDouble { C0 = C0 / norm, C1 = C1 / norm, C2 = C2 / norm } : this;
      }
    }
  }
}