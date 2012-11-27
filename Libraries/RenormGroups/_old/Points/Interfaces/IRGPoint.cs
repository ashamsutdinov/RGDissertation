using System;

namespace Old.RenormGroups.Points.Interfaces
{
  public interface IRGPoint<T> : IPoint<T, double>
    where T : IComparable
  {
    T R { get; }
    T G { get; }
  }
}