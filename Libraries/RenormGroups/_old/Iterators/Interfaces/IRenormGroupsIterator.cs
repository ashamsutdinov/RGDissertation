using System;

namespace Old.RenormGroups.Iterators.Interfaces
{
  public interface IRenormGroupsIterator<T> : IIterator<T, double>
    where T : IComparable
  {
  }
}