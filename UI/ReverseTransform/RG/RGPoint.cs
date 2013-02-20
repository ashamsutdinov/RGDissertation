namespace ReverseTransform
{
  public class RGPoint
  {
#if DECIMAL
    public decimal R { get; set; }

    public decimal G { get; set; }
#else
    public double R { get; set; }

    public double G { get; set; }
#endif
  }
}