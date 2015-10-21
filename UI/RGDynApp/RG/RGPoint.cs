using System;

namespace RGDynApp.RG
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

        public RGPoint DirectIterated(double n, double lambda, double lambda2)
        {
            var rPlus1 = R + 1;
            var rPlus1Q = rPlus1 * rPlus1;
            var t = rPlus1Q - G;
            var d = rPlus1Q - G / n;
            var td = t / d;
            var m = td * rPlus1 - 1;
            var r = lambda * m;
            var g = (lambda2 / n) * td * td * G;
            return new RGPoint { R = r, G = g };
        }
    }
}