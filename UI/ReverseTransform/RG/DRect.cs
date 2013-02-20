namespace ReverseTransform
{
  public class DRect :
#if DECIMAL
 Rect<decimal>
#else
 Rect<double>
#endif
  {
  }
}