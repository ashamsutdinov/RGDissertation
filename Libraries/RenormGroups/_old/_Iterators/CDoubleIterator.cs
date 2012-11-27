using System;
using RenormGroups._Points;

namespace RenormGroups._Iterators
{
  public class CDoubleIterator : CIteratorBase<double>
  {
    private Func<CPointBase<double>, CPointBase<double>> _directIteration;

    public override Func<CPointBase<double>, CPointBase<double>> DirectIteration
    {
      get
      {
        return _directIteration ?? (_directIteration = pt => new CPointDouble
        {
          C0 = (pt.C1 - pt.C0) + (1 / N) * (pt.C0 * pt.C2 - pt.C1 * pt.C1),
          C1 = Lambda * ((pt.C1 - pt.C0) * (pt.C2 - pt.C1) + (1 / N) * (pt.C0 * pt.C2 - pt.C1 * pt.C1)),
          C2 = (Lambda * Lambda) * ((pt.C2 - pt.C1) * (pt.C2 - pt.C1) + (1 / N) * (pt.C0 * pt.C2 - pt.C1 * pt.C1))
        }.Normalized as CPointDouble);
      }
    }

    public override Func<CPointBase<double>, CPointBase<double>> ReverseIteration
    {
      get { throw new NotImplementedException(); }
    }
  }
}