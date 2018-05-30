using System;
using System.Collections.Generic;
using System.Drawing;
using RgLib;

namespace RgLib
{
    public static class RG
    {
        public static Color Blend(Color color, Color backColor, double amount = 0.5)
        {
            var r = (byte)(color.R * amount + backColor.R * (1 - amount));
            var g = (byte)(color.G * amount + backColor.G * (1 - amount));
            var b = (byte)(color.B * amount + backColor.B * (1 - amount));
            return Color.FromArgb(r, g, b);
        }

        private static Color GetBgColor(CPoint pt, CProjection projection)
        {
            var rp = pt.RG(projection);
            return rp.G < 0 ? Config.Green : Config.Blue;
        }

        public static Image GetBg(int width, int height, CProjection proj)
        {
            if (proj.HasFlag(CProjection.C0C1))
                return GetBgC0C1(width, height);
            if (proj.HasFlag(CProjection.C1C2))
                return GetBgC1C2(width, height);
            if (proj.HasFlag(CProjection.C0C2))
                return GetBgC0C2(width, height);
            throw new NotSupportedException();
        }

        private static Image GetBgC0C1(int width, int height)
        {
            var img = new Bitmap(width, height);

            var onePtWidth = 2.0 / width;
            var onePtHeight = 2.0 / height;
            const double x1 = -1.0;
            const double y1 = -1.0;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var x = x1 + i * onePtWidth;
                    var y = y1 + j * onePtHeight;
                    var c0 = x;
                    var c1 = y;
                    var rd = c0 * c0 + c1 * c1;
                    var c2 = 1 - rd;
                    Color clr;
                    if (rd <= 1)
                    {
                        c2 = Math.Sqrt(c2);
                        var cpt = new CPoint(c0, c1, c2);
                        clr = GetBgColor(cpt, CProjection.C0C1);
                    }
                    else
                    {
                        clr = Color.White;
                    }
                    img.SetPixel(i, j, clr);
                }
            }
            return img;
        }

        private static Image GetBgC1C2(int width, int height)
        {
            var img = new Bitmap(width, height);

            var onePtWidth = 2.0 / width;
            var onePtHeight = 2.0 / height;
            const double x1 = -1.0;
            const double y1 = -1.0;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var x = x1 + i * onePtWidth;
                    var y = y1 + j * onePtHeight;
                    var c1 = x;
                    var c2 = y;
                    var rd = c1 * c1 + c2 * c2;
                    var c0 = 1 - rd;
                    Color clr;
                    if (rd <= 1)
                    {
                        c0 = Math.Sqrt(c0);
                        var cpt = new CPoint(c0, c1, c2);
                        clr = GetBgColor(cpt, CProjection.C1C2);
                    }
                    else
                    {
                        clr = Color.White;
                    }
                    img.SetPixel(i, j, clr);
                }
            }
            return img;
        }

        private static Image GetBgC0C2(int width, int height)
        {
            var img = new Bitmap(width, height);

            var onePtWidth = 2.0 / width;
            var onePtHeight = 2.0 / height;
            const double x1 = -1.0;
            const double y1 = -1.0;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    var x = x1 + i * onePtWidth;
                    var y = y1 + j * onePtHeight;
                    var c0 = x;
                    var c2 = y;
                    var rd = c0 * c0 + c2 * c2;
                    var c1 = 1 - rd;
                    Color clr;
                    if (rd <= 1)
                    {
                        c1 = Math.Sqrt(c1);
                        var cpt = new CPoint(c0, c1, c2);
                        clr = GetBgColor(cpt, CProjection.C0C2);
                    }
                    else
                    {
                        clr = Color.White;
                    }
                    img.SetPixel(i, j, clr);
                }
            }
            return img;
        }

        public static Color GetColorReversed(CPoint pt, CProjection projection)
        {
            var rp = pt.RG(projection);
            int cnt;
            var end = pt.ReverseTrackEndPoint(Config.ReserverInterestedPoint, projection, out cnt);
            if (cnt >= Config.Count)
            {
                return Config.Black;
            }
            var last = end;
            var p = Config.ReserverInterestedPoint;
            Color clr;
            if (last.C0 > 0)
            {
                clr = rp.G < 0
                            ? (last.C1 < p.C1 ? Config.Yellow : Config.Green)
                            : (last.C1 < p.C1 ? Config.Red : Config.Blue);
            }
            else
            {
                clr = rp.G < 0
                            ? (last.C1 > p.C1 ? Config.Yellow : Config.Green)
                            : (last.C1 > p.C1 ? Config.Red : Config.Blue);
            }
            var resClr = clr;
            return resClr;
        }

        public static Color GetColorReversed(RGPoint pt, CProjection projection)
        {
            var rp = pt;
            int cnt;
            var end = pt.ReverseTrackEndPoint(Config.ReserverInterestedPointRg, projection, out cnt);
            if (cnt >= Config.Count)
            {
                return Config.Black;
            }
            var last = end;
            var p = Config.ReserverInterestedPointRg;
            var clr = rp.G < 0
                            ? (last.R < p.R ? Config.Yellow : Config.Green)
                            : (last.R < p.R ? Config.Red : Config.Blue);

            var resClr = clr;
            return resClr;
        }

        public static Color GetColorDirect(RGPoint pt, CProjection projection)
        {
            var rp = pt;
            int cnt;
            var end = pt.DirectTrackEndPoint(Config.ReserverInterestedPointRg, projection, out cnt);
            if (cnt >= Config.Count)
            {
                return Config.Black;
            }
            var last = end;
            var p = Config.ReserverInterestedPointRg;
            var clr = rp.G < 0
                            ? (last.R < p.R ? Config.Yellow : Config.Green)
                            : (last.R < p.R ? Config.Red : Config.Blue);

            var resClr = clr;
            return resClr;
        }

        private static double Correct(double value, double max)
        {
            if (value < 0)
                return 0;
            if (value >= max)
                return max - 1;
            return value;
        }

        private static void SetPixel(int i, int j, Bitmap bmp, Color clr)
        {
            if (i < bmp.Width && i >= 0 && j < bmp.Height && j >= 0)
                bmp.SetPixel(i, j, clr);
        }

        public static void DrawLine(double x, double y, double xsz, double ysz, double sz, Pen pen, CPoint cp1, CPoint cp2, Graphics gr, CProjection projection, bool verify = true)
        {
            if (projection.HasFlag(CProjection.C0C1))
                DrawLineC0C1(x, y, xsz, ysz, sz, pen, cp1, cp2, gr, verify);
            else if (projection.HasFlag(CProjection.C1C2))
                DrawLineC1C2(x, y, xsz, ysz, sz, pen, cp1, cp2, gr, verify);
            else if (projection.HasFlag(CProjection.C0C2))
                DrawLineC0C2(x, y, xsz, ysz, sz, pen, cp1, cp2, gr, verify);
            else
                throw new NotSupportedException();
        }

        public static void FillPoint(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, CProjection projection, int radius = 3)
        {
            if (projection.HasFlag(CProjection.C0C1))
                FillPointC0C1(x, y, xsz, ysz, sz, clr, cp1, gr, radius);
            else if (projection.HasFlag(CProjection.C1C2))
                FillPointC1C2(x, y, xsz, ysz, sz, clr, cp1, gr, radius);
            else if (projection.HasFlag(CProjection.C0C2))
                FillPointC0C2(x, y, xsz, ysz, sz, clr, cp1, gr, radius);
            else
                throw new NotSupportedException();
        }

        public static void DrawPoint(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, CProjection projection, int radius = 3)
        {
            if (projection.HasFlag(CProjection.C0C1))
                DrawPointC0C1(x, y, xsz, ysz, sz, clr, cp1, gr, radius);
            else
                throw new NotSupportedException();
        }

        public static void DrawCross(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, CProjection projection, int radius = 3)
        {
            if (projection.HasFlag(CProjection.C0C1))
                DrawCrossC0C1(x, y, xsz, ysz, sz, clr, cp1, gr, radius);
            else
                throw new NotSupportedException();
        }

        public static void DrawXCross(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, CProjection projection, int radius = 3)
        {
            if (projection.HasFlag(CProjection.C0C1))
                DrawXCrossC0C1(x, y, xsz, ysz, sz, clr, cp1, gr, radius);
            else
                throw new NotSupportedException();
        }

        public static void SetPixel(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Bitmap bmp, CProjection projection)
        {
            if (projection.HasFlag(CProjection.C0C1))
                SetPixelC0C1(x, y, xsz, ysz, sz, clr, cp1, bmp);
            else if (projection.HasFlag(CProjection.C1C2))
                SetPixelC1C2(x, y, xsz, ysz, sz, clr, cp1, bmp);
            else if (projection.HasFlag(CProjection.C0C2))
                SetPixelC0C2(x, y, xsz, ysz, sz, clr, cp1, bmp);
            else
                throw new NotSupportedException();
        }

        public static void FillArea(double x, double y, double xsz, double ysz, double sz, Color clr, IEnumerable<CPoint> cpts, Bitmap bmp, CProjection projection)
        {
            if (projection.HasFlag(CProjection.C0C1))
                FillAreaC0C1(x, y, xsz, ysz, sz, clr, cpts, bmp);
            else if (projection.HasFlag(CProjection.C1C2))
                FillAreaC1C2(x, y, xsz, ysz, sz, clr, cpts, bmp);
            else if (projection.HasFlag(CProjection.C0C2))
                FillAreaC0C2(x, y, xsz, ysz, sz, clr, cpts, bmp);
            else
                throw new NotSupportedException();
        }

        private static void DrawLineC0C1(double x, double y, double xsz, double ysz, double sz, Pen pen, CPoint cp1, CPoint cp2, Graphics gr, bool verify = true)
        {
            var i1 = Correct((cp1.C0 - x) / xsz, sz);
            var i2 = Correct((cp2.C0 - x) / xsz, sz);
            var j1 = Correct((cp1.C1 - y) / ysz, sz);
            var j2 = Correct((cp2.C1 - y) / ysz, sz);

            var di = Math.Abs(i2 - i1);
            var dj = Math.Abs(j2 - j1);
            var d = Math.Sqrt(di * di + dj * dj);

            var check = sz / 3;
            if (d >= check && verify)
            {
                return;
            }

            lock (gr)
            {
                gr.DrawLine(pen, (float)i1, (float)j1, (float)i2, (float)j2);
            }
        }

        private static void FillPointC0C1(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, int radius = 3)
        {
            var i1 = Correct((cp1.C0 - x) / xsz, sz);
            var j1 = Correct((cp1.C1 - y) / ysz, sz);

            var brush = new SolidBrush(clr);

            lock (gr)
            {
                gr.FillEllipse(brush, (float)(i1 - radius), (float)(j1 - radius), 2 * radius, 2 * radius);
            }
        }

        private static void DrawPointC0C1(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, int radius = 3)
        {
            var i1 = Correct((cp1.C0 - x) / xsz, sz);
            var j1 = Correct((cp1.C1 - y) / ysz, sz);

            var pen = new Pen(clr);

            lock (gr)
            {
                gr.DrawEllipse(pen, (float)(i1 - radius), (float)(j1 - radius), 2 * radius, 2 * radius);
            }
        }

        private static void DrawCrossC0C1(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, int radius = 3)
        {
            var i1 = Correct((cp1.C0 - x) / xsz, sz);
            var j1 = Correct((cp1.C1 - y) / ysz, sz);

            var pen = new Pen(clr);

            lock (gr)
            {
                var p1 = new Point((int)(i1 - radius), (int)j1);
                var p2 = new Point((int)(i1 + radius), (int)j1);
                var p3 = new Point((int)i1, (int)(j1 - radius));
                var p4 = new Point((int)i1, (int)(j1 - radius));
                gr.DrawEllipse(pen, (float)(i1 - radius), (float)(j1 - radius), 2 * radius, 2 * radius);
                gr.DrawLine(pen, p1, p2);
                gr.DrawLine(pen, p3, p4);
            }
        }

        private static void DrawXCrossC0C1(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, int radius = 3)
        {
            var i1 = Correct((cp1.C0 - x) / xsz, sz);
            var j1 = Correct((cp1.C1 - y) / ysz, sz);

            var pen = new Pen(clr);

            lock (gr)
            {
                var p1 = new Point((int)(i1 - radius), (int)(j1 - radius));
                var p2 = new Point((int)(i1 + radius), (int)(j1 + radius));
                var p3 = new Point((int)(i1 - radius), (int)(j1 - radius));
                var p4 = new Point((int)(i1 + radius), (int)(j1 - radius));
                gr.DrawEllipse(pen, (float)(i1 - radius), (float)(j1 - radius), 2 * radius, 2 * radius);
                gr.DrawLine(pen, p1, p2);
                gr.DrawLine(pen, p3, p4);
            }
        }

        private static void SetPixelC0C1(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Bitmap bmp)
        {
            var i1 = (int)Correct((cp1.C0 - x) / xsz, sz);
            var j1 = (int)Correct((cp1.C1 - y) / ysz, sz);
            SetPixel(i1, j1, bmp, clr);
        }

        private static void FillAreaC0C1(double x, double y, double xsz, double ysz, double sz, Color clr, IEnumerable<CPoint> cpts, Bitmap bmp)
        {
            foreach (var cPoint in cpts)
            {
                SetPixelC0C1(x, y, xsz, ysz, sz, clr, cPoint, bmp);
            }
        }

        private static void DrawLineC1C2(double x, double y, double xsz, double ysz, double sz, Pen pen, CPoint cp1, CPoint cp2, Graphics gr, bool verify = true)
        {
            var i1 = Correct((cp1.C1 - x) / xsz, sz);
            var i2 = Correct((cp2.C1 - x) / xsz, sz);
            var j1 = Correct((cp1.C2 - y) / ysz, sz);
            var j2 = Correct((cp2.C2 - y) / ysz, sz);

            var di = Math.Abs(i2 - i1);
            var dj = Math.Abs(j2 - j1);
            var d = Math.Sqrt(di * di + dj * dj);

            var check = sz / 3;
            if (d >= check && verify)
            {
                return;
            }

            lock (gr)
            {
                gr.DrawLine(pen, (float)i1, (float)j1, (float)i2, (float)j2);
            }
        }

        private static void FillPointC1C2(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, int radius = 3)
        {
            var i1 = Correct((cp1.C1 - x) / xsz, sz);
            var j1 = Correct((cp1.C2 - y) / ysz, sz);

            var brush = new SolidBrush(clr);

            lock (gr)
            {
                gr.FillEllipse(brush, (float)(i1 - radius), (float)(j1 - radius), 2 * radius, 2 * radius);
            }
        }

        private static void SetPixelC1C2(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Bitmap bmp)
        {
            var i1 = (int)Correct((cp1.C1 - x) / xsz, sz);
            var j1 = (int)Correct((cp1.C2 - y) / ysz, sz);
            SetPixel(i1, j1, bmp, clr);
        }

        private static void FillAreaC1C2(double x, double y, double xsz, double ysz, double sz, Color clr, IEnumerable<CPoint> cpts, Bitmap bmp)
        {
            foreach (var cPoint in cpts)
            {
                SetPixelC1C2(x, y, xsz, ysz, sz, clr, cPoint, bmp);
            }
        }

        private static void DrawLineC0C2(double x, double y, double xsz, double ysz, double sz, Pen pen, CPoint cp1, CPoint cp2, Graphics gr, bool verify = true)
        {
            var i1 = Correct((cp1.C0 - x) / xsz, sz);
            var i2 = Correct((cp2.C0 - x) / xsz, sz);
            var j1 = Correct((cp1.C2 - y) / ysz, sz);
            var j2 = Correct((cp2.C2 - y) / ysz, sz);

            var di = Math.Abs(i2 - i1);
            var dj = Math.Abs(j2 - j1);
            var d = Math.Sqrt(di * di + dj * dj);

            var check = sz / 3;
            if (d >= check && verify)
            {
                return;
            }

            lock (gr)
            {
                gr.DrawLine(pen, (float)i1, (float)j1, (float)i2, (float)j2);
            }
        }

        private static void FillPointC0C2(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Graphics gr, int radius = 3)
        {
            var i1 = Correct((cp1.C0 - x) / xsz, sz);
            var j1 = Correct((cp1.C0 - y) / ysz, sz);

            var brush = new SolidBrush(clr);

            lock (gr)
            {
                gr.FillEllipse(brush, (float)(i1 - radius), (float)(j1 - radius), 2 * radius, 2 * radius);
            }
        }

        private static void SetPixelC0C2(double x, double y, double xsz, double ysz, double sz, Color clr, CPoint cp1, Bitmap bmp)
        {
            var i1 = (int)Correct((cp1.C0 - x) / xsz, sz);
            var j1 = (int)Correct((cp1.C0 - y) / ysz, sz);
            SetPixel(i1, j1, bmp, clr);
        }

        private static void FillAreaC0C2(double x, double y, double xsz, double ysz, double sz, Color clr, IEnumerable<CPoint> cpts, Bitmap bmp)
        {
            foreach (var cPoint in cpts)
            {
                SetPixelC0C2(x, y, xsz, ysz, sz, clr, cPoint, bmp);
            }
        }
    }
}