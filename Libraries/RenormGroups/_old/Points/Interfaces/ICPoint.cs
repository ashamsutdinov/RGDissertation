using System;

namespace Old.RenormGroups.Points.Interfaces
{
  public interface ICPoint<T> : IPoint<T, double>
    where T : IComparable
  {
    T C0 { get; }
    T C1 { get; }
    T C2 { get; }
  }
}