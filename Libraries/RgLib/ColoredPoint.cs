using System.Drawing;

namespace RgLib
{
    public class ColoredPoint<T>
    {
        public T Value { get; set; }

        public Color Color { get; set; }
    }
}