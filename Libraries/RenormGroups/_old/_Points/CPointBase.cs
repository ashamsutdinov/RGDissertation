using RenormGroups._Points.Interfaces;

namespace RenormGroups._Points
{
  public abstract class CPointBase<T> : PointBase<T>, Interfaces.ICPoint<T>
  {
    public override IPoint Normalized
    {
      get { return this; }
    }

    public T C0 { get; set; }

    public T C1 { get; set; }

    public T C2 { get; set; }
  }
}