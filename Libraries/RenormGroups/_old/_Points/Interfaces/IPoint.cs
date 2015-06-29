namespace RenormGroups._Points.Interfaces
{
  public interface IPoint
  {
    IPoint Normalized { get; }
  }

  public interface IPoint<T> : IPoint
  {
    T DistanceTo(IPoint<T> pt);
  }
}
