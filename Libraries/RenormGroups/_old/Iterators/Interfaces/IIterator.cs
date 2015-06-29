using System;
using System.Collections.Generic;
using Old.RenormGroups.Points.Interfaces;

namespace Old.RenormGroups.Iterators.Interfaces
{
  public interface IIterator<T, TD>
    where T : IComparable
    where TD : IComparable
  {
    IPoint<T, TD> IterateDirect(IPoint<T, TD> arg, int iterations = 0);
    IPoint<T, TD> IterateReverse(IPoint<T, TD> arg, int iterations = 0);
    IEnumerable<IPoint<T, TD>> IterateDirect(IEnumerable<IPoint<T, TD>> arg, int iterations = 0);
    IEnumerable<IPoint<T, TD>> IterateReverse(IEnumerable<IPoint<T, TD>> arg, int iterations = 0);
  }
}
