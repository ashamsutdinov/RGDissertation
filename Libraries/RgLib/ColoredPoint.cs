using System.Drawing;

namespace ReverseTransform
{
    public class ColoredPoint<T>
    {
        public T Value { get; set; }

        public Color Color { get; set; }
    }
}