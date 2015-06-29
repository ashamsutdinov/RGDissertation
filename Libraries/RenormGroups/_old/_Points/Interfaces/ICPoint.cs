namespace RenormGroups._Points.Interfaces
{
  public interface ICPoint<T> : IPoint<T>
  {
    T C0 { get; }
    T C1 { get; }
    T C2 { get; }
  }
}