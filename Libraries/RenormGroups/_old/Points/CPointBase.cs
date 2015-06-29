using System;
using System.Diagnostics.Contracts;
using Old.RenormGroups.Points.Interfaces;

namespace Old.RenormGroups.Points
{
  public abstract class CPointBase<T> : PointBase<T, double>, ICPoint<T>
    where T : IComparable
  {
    #region Constructors

    protected CPointBase()
      : base(3)
    {
    }

    protected CPointBase(T[] components)
      : base(components)
    {
      Contract.Requires(components.Length == 3);
    }

    #endregion

    #region Implementation of ICPoint<T>

    public T C0
    {
      get { return Components[0]; }
      set { Components[0] = value; }
    }

    public T C1
    {
      get { return Components[1]; }
      set { Components[1] = value; }
    }

    public T C2
    {
      get { return Components[2]; }
      set { Components[2] = value; }
    }

    #endregion
  }
}