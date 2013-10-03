using System;

namespace RGDynApp
{
    public class RGPoint
    {
        public double R;

        public double G;

        public RGPoint(double r, double g)
        {
            R = r;
            G = g;
        }

        public RGPoint()
            : this(0, 0)
        {
        }

        public RGPoint(RGPoint p)
            : this(p.R, p.G)
        {
        }

        public CPoint C(CProjection projection)
        {
            switch (projection)
            {
                case CProjection.C1C2:
                    return new CPoint(1, -R, R * R - G);
                case CProjection.C0C2:
                    return new CPoint(R * R - G, -R, 1);
            }
            throw new NotSupportedException();
        }
    }
}