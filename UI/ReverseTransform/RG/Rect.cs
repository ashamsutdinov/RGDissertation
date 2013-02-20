using System;

namespace ReverseTransform
{
  public class Rect<T>
    where T : IComparable
  {
    public T X { get; set; }

    public T Y { get; set; }

    public T Width { get; set; }

    public T Height { get; set; }
  }
}