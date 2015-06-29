using System.Collections.Generic;
using RenormGroups._Points.Interfaces;

namespace RenormGroups._Sets.Interfaces
{
  public interface IIterableSet
  {
  }

  public interface IIterableSet<out T>
     where T : IPoint
  {
    IEnumerable<T> Collection { get; }
    bool InSet(IPoint point);
  }

  public interface IIterableSet<out T, TD> : IIterableSet<T>
      where T : IPoint<TD>
  {
  }
}
