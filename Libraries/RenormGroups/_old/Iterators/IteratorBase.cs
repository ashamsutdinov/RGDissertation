using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Old.RenormGroups.Iterators.Interfaces;
using Old.RenormGroups.Points.Interfaces;

namespace Old.RenormGroups.Iterators
{
  public class IteratorBase<T, TD> : IIterator<T, TD>
    where T : IComparable
    where TD : IComparable
  {
    #region Properties

    public Func<IPoint<T, TD>, IPoint<T, TD>> DirectIteration { get; protected set; }

    public Func<IPoint<T, TD>, IPoint<T, TD>> ReverseIteration { get; protected set; }

    #endregion

    #region Implementation of IIterator<T,TD>

    public IPoint<T, TD> DoRecursiveCall(
        IPoint<T, TD> arg,
        int iterations,
        Func<IPoint<T, TD>, int, IPoint<T, TD>> iterator,
        Func<IPoint<T, TD>, IPoint<T, TD>> iteration
      )
    {
      Contract.Requires(iterator != null);
      Contract.Requires(arg != null);
      Contract.Requires(iterations >= 0);

      if (iteration == null)
        throw new NotImplementedException();
      if (iterations == 0)
        return arg;
      if (iterations == 1)
        return iteration(arg);
      var iterated = iteration(arg);
      return iterator(iterated, iterations - 1);
    }

    public IPoint<T, TD> IterateDirect(IPoint<T, TD> arg, int iterations = 0)
    {
      return DoRecursiveCall(arg, iterations, IterateDirect, DirectIteration);
    }

    public IPoint<T, TD> IterateReverse(IPoint<T, TD> arg, int iterations = 0)
    {
      return DoRecursiveCall(arg, iterations, IterateReverse, DirectIteration);
    }

    public IEnumerable<IPoint<T, TD>> IterateDirect(IEnumerable<IPoint<T, TD>> arg, int iterations = 0)
    {
      Contract.Requires(arg != null);
      return arg.Select(a => IterateDirect(a, iterations));

    }

    public IEnumerable<IPoint<T, TD>> IterateReverse(IEnumerable<IPoint<T, TD>> arg, int iterations = 0)
    {
      Contract.Requires(arg != null);
      return arg.Select(a => IterateReverse(a, iterations));
    }

    #endregion
  }

  public class CDoubleIterator : IteratorBase<double, double>
  {
    public CDoubleIterator()
    {
      DirectIteration = (pt) =>
      {
        return pt;
      };

      ReverseIteration = (pt) =>
      {
        return pt;
      };
    }
  }

}
