using System;
using RenormGroups._Points.Interfaces;

namespace RenormGroups._Iterators
{
  public abstract class RenormGroupsIteratorBase<T> : IteratorBase<T>
    where T : IPoint
  {
    public double N { get; set; }

    public double Alpha { get; set; }

    public double Lambda
    {
      get
      {
        return Math.Pow(N, Alpha - 1);
      }
    }
  }
}