using System;
using System.Drawing;

namespace RGDynApp
{
    public class CPoint
    {
        public double C0;

        public double C1;

        public double C2;

        public bool IsNormal;

        public static CPoint New(PointF pt, CProjection projection)
        {
            double c0 = 0, c1 = 0, c2 = 0;
            switch (projection)
            {
                case CProjection.C1C2:
                    c1 = pt.X;
                    c2 = pt.Y;
                    var q = c1 * c1 + c2 * c2;
                    var s = 1 - q;
                    if (s > 0)
                    {
                        c0 = Math.Sqrt(s);
                        return new CPoint(c0, c1, c2)
                        {
                            IsNormal = true
                        };
                    }
                    return new CPoint(c0, c1, c2);
            }
            throw new NotSupportedException();
        }

        public RGPoint RG(CProjection projection)
        {
            switch (projection)
            {
                case CProjection.C1C2:
                    return RGProjC1C2();
                case CProjection.C0C1:
                    return RGProjC0C1();
                case CProjection.C0C2:
                    return RGProjC0C2();
            }
            throw new NotSupportedException();
        }

        public CPoint(double c0, double c1, double c2)
        {
            Build(c0, c1, c2);
        }

        public CPoint(CPoint src)
            : this(src.C0, src.C1, src.C2)
        {
        }

        private void Build(double c0, double c1, double c2)
        {
            C0 = c0;
            C1 = c1;
            C2 = c2;
            var norm = Norm;
            if (!(norm > 0))
                return;
            C0 = C0 / norm;
            C1 = C1 / norm;
            C2 = C2 / norm;
        }

        private void ToOpposite()
        {
            C0 = -C0;
            C1 = -C1;
            C2 = -C2;
        }

        public double Norm
        {
            get
            {
                var sum = C0 * C0 + C1 * C1 + C2 * C2;
                var n = Math.Sqrt(sum);
                return n;
            }
        }

        public double DistanceTo(CPoint c)
        {
            var sc0 = C0 - c.C0;
            var sc1 = C1 - c.C1;
            var sc2 = C2 - c.C2;
            var sum = sc0*sc0 + sc1*sc1 + sc2*sc2;
            var dist = Math.Sqrt(sum);
            return dist;
        }

        public CPoint Opposite
        {
            get { return new CPoint(-C0, -C1, -C2); }
        }

        private RGPoint RGProjC0C1()
        {
            const double max = double.MaxValue;
            try
            {
                var r = C2 > 0 || C2 < 0 ? -C1 / C2 : max;
                var g = C2 > 0 || C2 < 0 ? (C1 * C1 - C0 * C2) / (C2 * C2) : max;
                return new RGPoint { R = r, G = g };
            }
            catch (Exception)
            {
                return new RGPoint { R = max, G = max };
            }
        }

        private RGPoint RGProjC1C2()
        {

            const double max = double.MaxValue;
            try
            {
                var r = C0 > 0 || C0 < 0 ? -C2 / C0 : max;
                var g = C0 > 0 || C0 < 0 ? (C1 * C1 - C0 * C2) / (C0 * C0) : max;
                return new RGPoint { R = r, G = g };
            }
            catch (Exception)
            {
                return new RGPoint { R = max, G = max };
            }
        }

        private RGPoint RGProjC0C2()
        {
            const double max = double.MaxValue;
            try
            {
                var r = C1 > 0 || C1 < 0 ? -C0 / C1 : max;
                var g = C1 > 0 || C1 < 0 ? (C1 * C1 - C0 * C2) / (C1 * C1) : max;
                return new RGPoint { R = r, G = g };
            }
            catch (Exception)
            {
                return new RGPoint { R = max, G = max };
            }
        }

        public void Project(CProjection projection)
        {
            var oppositize = 
                (projection.HasFlag(CProjection.C0C1) && (C2) < 0) ||
                (projection.HasFlag(CProjection.C0C2) && (C1) < 0) ||
                (projection.HasFlag(CProjection.C1C2) && (C0) < 0);

            if (oppositize)
                ToOpposite();
        }
    }
}