using System;
using System.Collections.Generic;
using System.Linq;
using RenormGroups._Iterators.Interfaces;
using RenormGroups._Points.Interfaces;
using RenormGroups._Sets.Interfaces;

namespace RenormGroups._Iterators
{
  public abstract class IteratorBase<T> : IIterator<T>
    where T : IPoint
  {
    public T IterateDirect(T x)
    {
      return DirectIteration(x);
    }

    public T IterateDirect(T x, int n)
    {
      if (n == 0)
        return x;
      if (n == 1)
        return IterateDirect(x);
      var curIter = IterateDirect(x);
      return IterateDirect(curIter, n - 1);
    }

    public T IterateReverse(T x)
    {
      return ReverseIteration(x);
    }

    public T IterateReverse(T x, int n)
    {
      if (n == 0)
        return x;
      if (n == 1)
        return IterateReverse(x);
      var curIter = IterateReverse(x);
      return IterateReverse(curIter, n - 1);
    }

    public IEnumerable<T> IterateDirect(IEnumerable<T> arr, int n = 1)
    {
      return arr.Select(x => IterateDirect(x, n));
    }

    public IEnumerable<T> IterateReverse(IEnumerable<T> arr, int n = 1)
    {
      return arr.Select(x => IterateReverse(x, n));
    }

    public IEnumerable<T> IterateDirect(IIterableSet<T> set, int n = 1)
    {
      return set.Collection.Select(x => IterateDirect(x, n));
    }

    public IEnumerable<T> IterateReverse(IIterableSet<T> set, int n = 1)
    {
      return set.Collection.Select(x => IterateReverse(x, n));
    }

    public abstract Func<T, T> DirectIteration { get; }

    public abstract Func<T, T> ReverseIteration { get; }
  }
}
