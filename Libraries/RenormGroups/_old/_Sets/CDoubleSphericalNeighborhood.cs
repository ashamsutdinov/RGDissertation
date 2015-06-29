using System.Collections.Generic;
using System.Diagnostics.Contracts;
using RenormGroups._Points;
using RenormGroups._Points.Interfaces;

namespace RenormGroups._Sets
{
  public class CDoubleSphericalNeighborhood : CSphericalNeighborhoodBase<double>
  {
    public override bool InSet(IPoint point)
    {
      // ReSharper disable InvocationIsSkipped
      Contract.Requires(point is ICPointDouble);
      // ReSharper restore InvocationIsSkipped
      return Center.DistanceTo(point as ICPointDouble) <= Radius;
    }

    public override void Initialize()
    {
      var lst = new List<CPointDouble>();
      var c01 = Center.C0 - Radius;
      var c02 = Center.C0 + Radius;
      var c11 = Center.C1 - Radius;
      var c12 = Center.C1 + Radius;
      var c21 = Center.C2 - Radius;
      var c22 = Center.C2 + Radius;
      for (var i0 = c01; i0 <= c02; i0 += Accuracy)
      {
        for (var i1 = c11; i1 <= c12; i1 += Accuracy)
        {
          for (var i2 = c21; i2 <= c22; i2 += Accuracy)
          {
            var pt = new CPointDouble { C0 = i0, C1 = i1, C2 = i2 };
            if (InSet(pt))
            {
              lst.Add(pt);
            }
          }
        }
      }
      Collection = lst;
    }
  }
}