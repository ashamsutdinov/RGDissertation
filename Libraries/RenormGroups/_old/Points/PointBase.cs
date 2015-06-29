using System;
using Old.RenormGroups.Points.Interfaces;

namespace Old.RenormGroups.Points
{
  public abstract class PointBase<T, TD> : IPoint<T, TD>
    where T : IComparable
    where TD : IComparable
  {
    #region Private

    #endregion

    #region Constructors

    protected PointBase(int sz)
    {
      Components = new T[sz];
    }

    protected PointBase(T[] components)
    {
      Components = components;
    }

    #endregion

    #region Implementation of IPoint<T,TD>

    public T[] Components { get; private set; }

    public abstract IPoint<T, TD> Normalized { get; }

    public abstract TD Norm { get; }

    public abstract TD DistanceTo(IPoint<T, TD> pt);

    public abstract TD ScalarWith(IPoint<T, TD> pt);

    public abstract TD AngleWith(IPoint<T, TD> pt);

    public virtual bool EqualsWith(IPoint<T, TD> pt)
    {
      var r = true;
      for (var i = 0; i < Components.Length; i++)
      {
        r = r && Components[i].Equals(pt.Components[i]);
      }
      return r;
    }

    #endregion
  }
}
