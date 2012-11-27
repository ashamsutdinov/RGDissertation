namespace RenormGroups
{
  public interface IRGPoint<T>
  {
    T R { get; set; }

    T G { get; set; }

    ICPoint<T> CPoint { get; }
  }
}