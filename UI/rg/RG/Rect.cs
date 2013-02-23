using System;

namespace rg
{
  public class Rect<T>
    where T : IComparable
  {
    public T X { get; set; }

    public T Y { get; set; }

    public T Width { get; set; }

    public T Height { get; set; }
  }

  public class Rect :
Rect<double>
  {
  }
}