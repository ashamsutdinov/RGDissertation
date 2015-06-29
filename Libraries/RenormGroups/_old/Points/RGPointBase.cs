using System;
using System.Diagnostics.Contracts;
using Old.RenormGroups.Points.Interfaces;

namespace Old.RenormGroups.Points
{
  public abstract class RGPointBase<T> : PointBase<T, double>, IRGPoint<T>
    where T: IComparable
  {
    protected RGPointBase() : base(2)
    {
    }

    protected RGPointBase(T[] components) : base(components)
    {
      Contract.Requires(components.Length == 2);
    }

    #region Implementation of IRGPoint<T>

    public T R
    {
      get { return Components[0]; }
      set { Components[0] = value; }
    }

    public T G
    {
      get { return Components[1]; }
      set { Components[1] = value; }
    }

    #endregion
  }
}