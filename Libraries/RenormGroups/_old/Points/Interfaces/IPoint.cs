using System;

namespace Old.RenormGroups.Points.Interfaces
{
  public interface IPoint<T, TD>
    where T : IComparable
    where TD : IComparable
  {
    T[] Components { get; }
    IPoint<T, TD> Normalized { get; }
    TD Norm { get; }
    TD DistanceTo(IPoint<T, TD> pt);
    TD ScalarWith(IPoint<T, TD> pt);
    TD AngleWith(IPoint<T, TD> pt);
    bool EqualsWith(IPoint<T, TD> pt);
  }
}
