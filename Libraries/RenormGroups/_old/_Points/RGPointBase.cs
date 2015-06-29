using RenormGroups._Points.Interfaces;

namespace RenormGroups._Points
{
  public abstract class RGPointBase<T> : PointBase<T>, Interfaces.IRGPoint<T>
  {
    public override IPoint Normalized
    {
      get { return this; }
    }

    public T R { get; set; }

    public T G { get; set; }
  }
}