using RenormGroups._Points.Interfaces;

namespace RenormGroups._Points
{
  public abstract class PointBase<T> : IPoint<T>
  {
    public abstract T DistanceTo(IPoint<T> pt);

    public abstract IPoint Normalized { get; }
  }
}