namespace ReverseTransform
{
  public class RGPoint<T>
  {
    public T R { get; set; }

    public T G { get; set; }
  }

  public class RGPoint :
#if DECIMAL
    RGPoint<decimal>
#else
 RGPoint<double>
#endif
  {
  }
}