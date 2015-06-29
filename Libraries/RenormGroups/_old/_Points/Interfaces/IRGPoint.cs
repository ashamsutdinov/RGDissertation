namespace RenormGroups._Points.Interfaces
{
  public interface IRGPoint<T> : IPoint<T>
  {
    T R { get; }
    T G { get; }
  }
}