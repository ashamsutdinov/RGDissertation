using System;
using RenormGroups._Points;

namespace RenormGroups._Iterators
{
  public class RGDoubleIterator : RGIteratorBase<double>
  {
    public override Func<RGPointBase<double>, RGPointBase<double>> DirectIteration
    {
      get { throw new NotImplementedException(); }
    }

    public override Func<RGPointBase<double>, RGPointBase<double>> ReverseIteration
    {
      get { throw new NotImplementedException(); }
    }
  }
}