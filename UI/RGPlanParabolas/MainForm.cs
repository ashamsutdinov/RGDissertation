using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RgLib;

namespace RGPlanParabolas
{
    public partial class MainForm : Form
    {
        private float _parabolaA;

        private float _parabolaB;

        private float _parabolaC;

        private float _alpha;

        private float _n;

        private int _iterations;

        private const float Step = 0.00001f;

        private const float XFrom = -10;

        private const float XTo = 10;

        private const float YFrom = -10;

        private const float YTo = 10;

        private const float XW = 20;

        private const float YH = 20;

        private const int FirstColor = 10;

        private const int LastColor = 230;

        private void InitializeConfiguration()
        {
            var conf = ConfigurationManager.AppSettings;
            float.TryParse(conf["Alpha"], out _alpha);
            float.TryParse(conf["N"], out _n);
            int.TryParse(conf["Iterations"], out _iterations);
            float.TryParse(conf["ParabolaA"], out _parabolaA);
            float.TryParse(conf["ParabolaB"], out _parabolaB);
            float.TryParse(conf["ParabolaC"], out _parabolaC);
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeConfiguration();
            DrawLines();
        }

        private void DrawLines()
        {
            var sz = _plot.Size;
            var bmp = new Bitmap(sz.Width, sz.Height);
            var gr = Graphics.FromImage(bmp);
            var whiteBrush = new SolidBrush(Color.White);
            gr.FillRectangle(whiteBrush, 0, 0, sz.Width, sz.Height);
            var parabolaPen = new Pen(Color.Black);
            var markupPen = new Pen(Color.YellowGreen);

            #region DrawMarkup

            gr.DrawLine(markupPen, 0, sz.Height / 2, sz.Width, sz.Height / 2);
            gr.DrawLine(markupPen, sz.Width / 2, 0, sz.Width / 2, sz.Height);

            #endregion

            var mainParabolaPoints = new List<PointF>();

            #region DrawMainParabola

            float x;
            for (x = XFrom; x < XTo; x += Step)
            {
                var y = _parabolaA * x * x + _parabolaB * x + _parabolaC;
                var pt = new PointF(x, y);
                var pts = MapToPlot(sz, pt);

                var x1 = x + Step;
                var y1 = _parabolaA * x1 * x1 + _parabolaB * x1 + _parabolaC;
                var pt1 = new PointF(x1, y1);
                var pts1 = MapToPlot(sz, pt1);

                if (pts.X >= 0 && pts.X < sz.Width && pts.Y >= 0 && pts.Y <= sz.Height &&
                    pts1.X >= 0 && pts1.X < sz.Width && pts1.Y >= 0 && pts1.Y <= sz.Height)
                {
                    gr.DrawLine(parabolaPen, pts, pts1);
                    mainParabolaPoints.Add(pt);
                }
            }

            #endregion //DrawMainParabola

            #region Iterations

            RgSettings.Build(_alpha, _n);

            var parabolaPtsLeft = mainParabolaPoints.Where(r => r.X <= 0).Select(r => new RGPoint(r.X, r.Y)).ToList();
            var cEnuLeft = parabolaPtsLeft.Select(r => r.CDirect).ToList();
            var iteratedLeft = RGPoint.DirectIteratedMany(cEnuLeft, _iterations, CProjection.C1C2).Last();
            var colorLeft = Color.Blue;
            var penLeft = new Pen(colorLeft);
            var rgPtsLeft = iteratedLeft.Value;
            foreach (var rgPoint in rgPtsLeft)
            {
                var pt = MapToPlot(sz, new PointF((float)rgPoint.R, (float)rgPoint.G));
                if (pt.X < 0 || pt.Y < 0 || pt.X > sz.Width || pt.Y > sz.Height)
                    continue;
                gr.DrawEllipse(penLeft, pt.X, pt.Y, 1, 1);
            }

            var parabolaPtsRight = mainParabolaPoints.Where(r => r.X >= 0).Select(r => new RGPoint(r.X, r.Y)).ToList();
            var cEnuRight = parabolaPtsRight.Select(r => r.CDirect).ToList();
            var iteratedRight = RGPoint.DirectIteratedMany(cEnuRight, _iterations, CProjection.C1C2).Last();
            var colorRight = Color.Red;
            var penRignt = new Pen(colorRight);
            var rgPtsRight = iteratedRight.Value;
            foreach (var rgPoint in rgPtsRight)
            {
                var pt = MapToPlot(sz, new PointF((float)rgPoint.R, (float)rgPoint.G));
                if (pt.X < 0 || pt.Y < 0 || pt.X > sz.Width || pt.Y > sz.Height)
                    continue;
                gr.DrawEllipse(penRignt, pt.X, pt.Y, 1, 1);
            }

            #endregion


            _plot.Image = bmp;
        }

        private Point MapToPlot(Size sz, PointF pt)
        {
            var pixw = XW / sz.Width;
            var pixh = YH / sz.Height;
            var x = (pt.X - XFrom) / pixw;
            var y = (YH - pt.Y + YFrom) / pixh;
            return new Point((int)x, (int)y);
        }
    }
}
