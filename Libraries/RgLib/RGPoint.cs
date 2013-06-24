using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseTransform
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

        public CPoint CReversed
        {
            get
            {
                return new CPoint(R * R - G, -R, 1);
            }
        }

        public CPoint CDirect
        {
            get
            {
                return new CPoint(1, -R, R * R - G);
            }
        }

        private static RGPoint ParabolaPoint(double r, double a, double b, double l1)
        {
            var g = r;
            if (g - b == 0)
            {
                g = double.MaxValue;
            }
            else
            {
                g = ((r - a) / (r - b)) * Math.Pow(r + l1, 2);
            }
            return new RGPoint { R = r, G = g };
        }

        public static IEnumerable<RGPoint> ParabolaReversed(double a, double b, double rmin, double rmax, double rstep, bool beforeTrans = true)
        {
            var l1 = RGSettings.LambdaMinus1;
            if (!beforeTrans)
            {
                l1 = 1;
            }

            for (var r = rmin; r <= rmax; r += rstep)
            {
                yield return ParabolaPoint(r, a, b, l1);
            }
        }

        public static IEnumerable<RGPoint> ParabolaDirect(double a, double b, double rmin, double rmax, double rstep, bool beforeTrans = true)
        {
            var l1 = 1d;
            if (!beforeTrans)
            {
                l1 = RGSettings.Lambda;
            }

            for (var r = rmin; r <= rmax; r += rstep)
            {
                yield return ParabolaPoint(r, a, b, l1);
            }
        }

        private static void CorrectAcc(double h1, double h2, ref double xacc, ref double yacc)
        {
            var d = h2 - h1;
            var div = 1.1;
            if (d > 1.5)
            {
                div = 1.5;
            }
            else if (d > 1)
            {
                div = 1.7;
            }
            else if (d > 0.5)
            {
                div = 2;
            }
            else if (d > 0.3)
            {
                div = 2.5;
            }
            else if (d > 0.2)
            {
                div = 3;
            }
            else if (d <= 0.2)
            {
                div = 5;
            }
            xacc = xacc / div;
            yacc = yacc / div;
        }

        public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetTriangleNegative(double h1, double h2, double xacc, double yacc, CProjection proj)
        {
            var sgn = proj.HasFlag(CProjection.Down) ? -1 : 1;
            if (proj.HasFlag(CProjection.C0C1))
                return GetC0C1TriangleNegative(h1, h2, xacc, yacc, sgn);
            if (proj.HasFlag(CProjection.C1C2))
                return GetC1C2TriangleNegative(h1, h2, xacc, yacc, sgn);
            throw new NotSupportedException();
        }

        public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetTrianglePositive(double h1, double h2, double xacc, double yacc, CProjection proj)
        {
            var sgn = proj.HasFlag(CProjection.Down) ? -1 : 1;
            if (proj.HasFlag(CProjection.C0C1))
                return GetC0C1TrianglePositive(h1, h2, xacc, yacc, sgn);
            if (proj.HasFlag(CProjection.C1C2))
                return GetC1C2TrianglePositive(h1, h2, xacc, yacc, sgn);
            throw new NotSupportedException();
        }

        public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetArcNegative(double h1, double h2, double acc, CProjection proj)
        {
            var sgn = proj.HasFlag(CProjection.Down) ? -1 : 1;
            if (proj.HasFlag(CProjection.C0C1))
                return GetC0C1ArcNegative(h1, h2, acc, sgn);
            if (proj.HasFlag(CProjection.C1C2))
                return GetC1C2ArcNegative(h1, h2, acc, sgn);
            throw new NotSupportedException();
        }

        public static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetArcPositive(double h1, double h2, double acc, CProjection proj)
        {
            var sgn = proj.HasFlag(CProjection.Down) ? -1 : 1;
            if (proj.HasFlag(CProjection.C0C1))
                return GetC0C1ArcPositive(h1, h2, acc, sgn);
            if (proj.HasFlag(CProjection.C1C2))
                return GetC1C2ArcPositive(h1, h2, acc, sgn);
            throw new NotSupportedException();
        }

        private static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetC0C1TriangleNegative(double h1, double h2, double xacc, double yacc, int sgn)
        {
            CorrectAcc(h1, h2, ref xacc, ref yacc);
            for (var x = h1; x <= h2; x += xacc)
            {
                for (var y = -1.0; y <= 0; y += yacc)
                {
                    var c0 = x;
                    var c1 = y;
                    var rd = c0 * c0 + c1 * c1;
                    var c2 = 1 - rd;
                    if (rd <= 1)
                    {
                        c2 = sgn * Math.Sqrt(c2);
                        var cpt = new CPoint(c0, c1, c2);
                        var rg = cpt.RG(CProjection.C0C1);
                        if (rg.G >= 0)
                        {
                            yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
                        }
                    }
                }
            }
        }

        private static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetC1C2TriangleNegative(double h1, double h2, double xacc, double yacc, int sgn)
        {
            CorrectAcc(h1, h2, ref xacc, ref yacc);
            for (var x = -1.0; x <= 0; x += xacc)
            {
                for (var y = h1; y <= h2; y += yacc)
                {
                    var c1 = x;
                    var c2 = y;
                    var rd = c1 * c1 + c2 * c2;
                    var c0 = 1 - rd;
                    if (rd <= 1)
                    {
                        c0 = sgn * Math.Sqrt(c0);
                        var cpt = new CPoint(c0, c1, c2);
                        var rg = cpt.RG(CProjection.C1C2);
                        if (rg.G >= 0)
                        {
                            yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
                        }
                    }
                }
            }
        }

        private static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetC0C1TrianglePositive(double h1, double h2, double xacc, double yacc, int sgn)
        {
            CorrectAcc(h1, h2, ref xacc, ref yacc);
            for (var x = h1; x <= h2; x += xacc)
            {
                for (var y = 0.0; y <= 1.0; y += yacc)
                {
                    var c0 = x;
                    var c1 = y;
                    var rd = c0 * c0 + c1 * c1;
                    var c2 = 1 - rd;
                    if (rd <= 1)
                    {
                        c2 = sgn * Math.Sqrt(c2);
                        var cpt = new CPoint(c0, c1, c2);
                        var rg = cpt.RG(CProjection.C0C1);
                        if (rg.G >= 0)
                        {
                            yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
                        }
                    }
                }
            }
        }

        private static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetC1C2TrianglePositive(double h1, double h2, double xacc, double yacc, int sgn)
        {
            CorrectAcc(h1, h2, ref xacc, ref yacc);
            for (var x = 0.0; x <= 1.0; x += xacc)
            {
                for (var y = h1; y <= h2; y += yacc)
                {
                    var c1 = x;
                    var c2 = y;
                    var rd = c1 * c1 + c2 * c2;
                    var c0 = 1 - rd;
                    if (rd <= 1)
                    {
                        c0 = sgn * Math.Sqrt(c0);
                        var cpt = new CPoint(c0, c1, c2);
                        var rg = cpt.RG(CProjection.C1C2);
                        if (rg.G >= 0)
                        {
                            yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
                        }
                    }
                }
            }
        }

        private static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetC0C1ArcNegative(double h1, double h2, double xacc, int sgn)
        {
            xacc = xacc / 100;
            for (var x = h1; x <= h2; x += xacc)
            {
                var c0 = x;
                var c1 = -Math.Sqrt(1 - Math.Pow(c0, 2));
                var rd = c0 * c0 + c1 * c1;
                var c2 = 1 - rd;
                if (rd <= 1)
                {
                    c2 = sgn * Math.Sqrt(c2);
                    var cpt = new CPoint(c0, c1, c2);
                    var rg = cpt.RG(CProjection.C0C1);
                    if (rg.G >= 0)
                    {
                        yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
                    }
                }
            }
        }

        private static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetC0C1ArcPositive(double h1, double h2, double xacc, int sgn)
        {
            xacc = xacc / 100;
            for (var x = h1; x <= h2; x += xacc)
            {
                var c0 = x;
                var c1 = Math.Sqrt(1 - Math.Pow(c0, 2));
                var rd = c0 * c0 + c1 * c1;
                var c2 = 1 - rd;
                if (rd <= 1)
                {
                    c2 = sgn * Math.Sqrt(c2);
                    var cpt = new CPoint(c0, c1, c2);
                    var rg = cpt.RG(CProjection.C0C1);
                    if (rg.G >= 0)
                    {
                        yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
                    }
                }
            }
        }

        private static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetC1C2ArcNegative(double h1, double h2, double yacc, int sgn)
        {
            yacc = yacc / 100;
            for (var y = h1; y <= h2; y += yacc)
            {
                var c2 = y;
                var c1 = -Math.Sqrt(1 - Math.Pow(c2, 2));
                var rd = c1 * c1 + c2 * c2;
                var c0 = 1 - rd;
                if (rd <= 1)
                {
                    c0 = sgn * Math.Sqrt(c0);
                    var cpt = new CPoint(c0, c1, c2);
                    var rg = cpt.RG(CProjection.C1C2);
                    if (rg.G >= 0)
                    {
                        yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
                    }
                }
            }
        }

        private static IEnumerable<KeyValuePair<CPoint, RGPoint>> GetC1C2ArcPositive(double h1, double h2, double yacc, int sgn)
        {
            yacc = yacc / 100;
            for (var y = h1; y <= h2; y += yacc)
            {
                var c2 = y;
                var c1 = Math.Sqrt(1 - Math.Pow(c2, 2));
                var rd = c1 * c1 + c2 * c2;
                var c0 = 1 - rd;
                if (rd <= 1)
                {
                    c0 = sgn * Math.Sqrt(c0);
                    var cpt = new CPoint(c0, c1, c2);
                    var rg = cpt.RG(CProjection.C1C2);
                    if (rg.G >= 0)
                    {
                        yield return new KeyValuePair<CPoint, RGPoint>(cpt, rg);
                    }
                }
            }
        }

        public static IEnumerable<KeyValuePair<CPoint, RGPoint>> ReverseIterated(IEnumerable<CPoint> points, CProjection projection)
        {
            return points.Select(cPoint => cPoint.ReverseIterated(projection)).Select(cpt => new KeyValuePair<CPoint, RGPoint>(cpt, cpt.RG(projection)));
        }

        public static IEnumerable<KeyValuePair<List<CPoint>, List<RGPoint>>> ReverseIteratedMany(IEnumerable<CPoint> points, int count, CProjection projection)
        {
            var lst = new List<KeyValuePair<List<CPoint>, List<RGPoint>>>();
            var curIterated = ReverseIterated(points, projection).ToList();
            lst.Add(
              new KeyValuePair<List<CPoint>, List<RGPoint>>(
                curIterated.Select(e => e.Key).ToList(),
                curIterated.Select(e => e.Value).ToList()
                )
              );
            for (var i = 2; i <= count; i++)
            {
                curIterated = ReverseIterated(curIterated.Select(c => c.Key), projection).ToList();
                lst.Add(
                new KeyValuePair<List<CPoint>, List<RGPoint>>(
                  curIterated.Select(e => e.Key).ToList(),
                  curIterated.Select(e => e.Value).ToList()
                  )
                );
            }
            return lst;
        }

        public static IEnumerable<KeyValuePair<CPoint, RGPoint>> DirectIterated(IEnumerable<CPoint> points, CProjection projection)
        {
            return points.Select(cPoint => cPoint.DirectIterated(projection)).Select(cpt => new KeyValuePair<CPoint, RGPoint>(cpt, cpt.RG(projection)));
        }

        public static IEnumerable<KeyValuePair<List<CPoint>, List<RGPoint>>> DirectIteratedMany(IEnumerable<CPoint> points, int count, CProjection projection)
        {
            var lst = new List<KeyValuePair<List<CPoint>, List<RGPoint>>>();
            var curIterated = DirectIterated(points, projection).ToList();
            lst.Add(
              new KeyValuePair<List<CPoint>, List<RGPoint>>(
                curIterated.Select(e => e.Key).ToList(),
                curIterated.Select(e => e.Value).ToList()
                )
              );
            for (var i = 2; i <= count; i++)
            {
                curIterated = DirectIterated(curIterated.Select(c => c.Key), projection).ToList();
                lst.Add(
                new KeyValuePair<List<CPoint>, List<RGPoint>>(
                  curIterated.Select(e => e.Key).ToList(),
                  curIterated.Select(e => e.Value).ToList()
                  )
                );
            }
            return lst;
        }

        public override string ToString()
        {
            return string.Format("({0};{1})", R, G);
        }

        public double DistanceTo(RGPoint pt)
        {
            var x = R - pt.R;
            var y = G - pt.G;
            var dist = Math.Sqrt(x * x + y * y);
            return dist;
        }

        public RGPoint ReverseIterated()
        {
            var rPlusLambda = R + RGSettings.Lambda;
            var rPlusLambda2 = rPlusLambda * rPlusLambda;
            var r1 = RGSettings.LambdaMinus1 * (((rPlusLambda2 - G) / (rPlusLambda2 - RGSettings.N * G)) * rPlusLambda - RGSettings.Lambda);
            var g1 = (RGSettings.N / RGSettings.Lambda2) * Math.Pow((rPlusLambda2 - G) / (rPlusLambda2 - RGSettings.N * G), 2) * G;
            return new RGPoint { R = r1, G = g1 };
        }

        public void IterateReverse()
        {
            var rPlusLambda = R + RGSettings.Lambda;
            var rPlusLambda2 = rPlusLambda * rPlusLambda;
            var r1 = RGSettings.LambdaMinus1 * (((rPlusLambda2 - G) / (rPlusLambda2 - RGSettings.N * G)) * rPlusLambda - RGSettings.Lambda);
            var g1 = (RGSettings.N / RGSettings.Lambda2) * Math.Pow((rPlusLambda2 - G) / (rPlusLambda2 - RGSettings.N * G), 2) * G;
            R = r1;
            G = g1;
        }

        public IEnumerable<RGPoint> ReverseTrack(RGPoint crit)
        {
            var pt = this;
            const double max = double.MaxValue;
            var d = max;
            var i = 0;
            var res = new List<RGPoint>();
            while (d > Config.Acc && i < Config.Count)
            {
                res.Add(pt);
                pt = pt.ReverseIterated();
                d = pt.DistanceTo(crit);
                i++;
            }
            return res;
        }

        public RGPoint ReverseTrackEndPoint(RGPoint crit, CProjection projection, out int cnt)
        {
            var pt = this;
            const double max = double.MaxValue;
            var d = max;
            var i = 0;
            while (d > Config.Acc && i < Config.Count)
            {
                pt.IterateReverse();
                d = pt.DistanceTo(crit);
                i++;
            }
            cnt = i;
            return pt;
        }
    }
}