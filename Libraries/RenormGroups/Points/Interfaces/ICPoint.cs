namespace RenormGroups
{
  public interface ICPoint<T>
  {
    T C0 { get; set; }

    T C1 { get; set; }

    T C2 { get; set; }

    T Norm { get; }

    IRGPoint<T> RGPoint { get; }

    ICPoint<T> Diametral { get; }

    ICPoint<T> Normalized { get; }

    T DistanceTo(ICPoint<T> pt);
  }
}