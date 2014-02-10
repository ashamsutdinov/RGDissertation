using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RgLib;

namespace RgLib
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private readonly ProcessingStack _stack = new ProcessingStack();

        private bool _pointSelecting;

        private CPoint _pointSelected;

        private List<CPoint> _pointTrack;

        private List<CPoint> PointTrack
        {
            get { return _pointTrack ?? (_pointTrack = _pointSelected.ReverseTrack(Config.ReserverInterestedPoint, CProjection.UpC0C1).ToList()); }
        }

        public int PointNumber { private get; set; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            InitializeConfig();
            Redraw();
        }

        private void InitializeConfig()
        {
            var frame = new ProcessingFrame
            {
                Rectangle = new DRect
                  {
                      X = -1.0,
                      Y = -1.0,
                      Width = 2.0,
                      Height = 2.0
                  }
            };

            _stack.Push(frame);
        }

        private Bitmap Recreate()
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
            }
            var bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bmp;
            return bmp;
        }

        private void FillPoint(CPoint p1, Color clr)
        {
            var np = NPt(p1);
            var gr = Graphics.FromImage(pictureBox.Image);
            var bgClr = RG.GetColorReversed(new CPoint(p1), CProjection.UpC0C1);
            var blend = RG.Blend(clr, bgClr);
            gr.TryDraw(g => g.FillEllipse(new SolidBrush(clr), np.X - 2, np.Y - 2, 4, 4));
            gr.TryDraw(g => g.DrawRectangle(new Pen(blend), np.X - 10, np.Y - 10, 20, 20));
        }

        private void DrawPoint(CPoint p1, Color clr)
        {
            var np = NPt(p1);
            var gr = Graphics.FromImage(pictureBox.Image);
            gr.TryDraw(g => g.DrawEllipse(new Pen(clr), np.X - 4, np.Y - 4, 8, 8));
        }

        private void DrawCross(CPoint p1, Color clr)
        {
            var np = NPt(p1);
            var gr = Graphics.FromImage(pictureBox.Image);
            var p = new Pen(clr);
            gr.TryDraw(g => g.DrawEllipse(p, np.X - 6, np.Y - 6, 12, 12));
            var pt1 = new Point(np.X - 6, np.Y);
            var pt2 = new Point(np.X + 6, np.Y);
            var pt3 = new Point(np.X, np.Y - 6);
            var pt4 = new Point(np.X, np.Y + 6);
            gr.TryDraw(g => g.DrawLine(p, pt1, pt2));
            gr.TryDraw(g => g.DrawLine(p, pt3, pt4));
        }

        private void DrawXCross(CPoint p1, Color clr)
        {
            var np = NPt(p1);
            var gr = Graphics.FromImage(pictureBox.Image);
            var p = new Pen(clr);
            gr.TryDraw(g => g.DrawEllipse(p, np.X - 6, np.Y - 6, 12, 12));
            var pt1 = new Point(np.X - 6, np.Y + 6);
            var pt2 = new Point(np.X + 6, np.Y - 6);
            var pt3 = new Point(np.X - 6, np.Y - 6);
            var pt4 = new Point(np.X + 6, np.Y + 6);
            gr.TryDraw(g => g.DrawLine(p, pt1, pt2));
            gr.TryDraw(g => g.DrawLine(p, pt3, pt4));
        }

        private Point NPt(double x, double y)
        {
            var fr = _stack.Peek();
            var rect = fr.Rectangle;
            var xA = rect.Width / pictureBox.Width;
            var yA = rect.Height / pictureBox.Height;
            var di = x - rect.X;
            var dj = y - rect.Y;
            var fi = di / xA;
            var fj = dj / yA;
            var i = (int)fi;
            var j = (int)fj;
            return new Point(i, j);
        }

        private Point NPt(CPoint p)
        {
            return NPt(p.C0, p.C1);
        }

        private void FillPixel(Bitmap bmp, double x, double y)
        {
            var c0 = x;
            var c1 = y;
            var rd = c0 * c0 + c1 * c1;
            var c2 = 1 - rd;
            var pt = NPt(c0, c1);
            if (rd <= 1)
            {
                c2 = Math.Sqrt(c2);
                var cpt = new CPoint(c0, c1, c2);
                var color = RG.GetColorReversed(cpt, CProjection.UpC0C1);
                SetPixel(bmp, pt.X, pt.Y, color);
            }
            else
            {
                SetPixel(bmp, pt.X, pt.Y, Config.White);
            }
        }

        private void Redraw()
        {
            var bmp = Recreate();
            var fr = _stack.Peek();
            var r = fr.Rectangle;
            var w = pictureBox.Width;
            var h = pictureBox.Height;
            var onepxw = r.Width / w;
            var onepxh = r.Height / h;

            for (var x = r.X; x <= r.X + r.Width; x += onepxw)
            {
                for (var y = r.Y; y <= r.Y + r.Height; y += onepxh)
                {
                    FillPixel(bmp, x, y);
                }
            }

            var gr = Graphics.FromImage(pictureBox.Image);

            DrawPoint(new CPoint(1, 0, 0), Config.White);
            DrawPoint(new CPoint(0, 0, 1), Config.Black);

            var sqrtN = Math.Sqrt(RgSettings.N);
            var nAlphaMinus1 = Math.Pow(RgSettings.N, RgSettings.Alpha - 1);
            var nAlphaMinus3Div2 = Math.Pow(RgSettings.N, RgSettings.Alpha - 1.5);
            var nAlphaMinus1Div2 = Math.Pow(RgSettings.N, RgSettings.Alpha - 0.5);

            var rPlus = (sqrtN - nAlphaMinus1) / (1 - sqrtN);
            var rMinus = (-sqrtN - nAlphaMinus1) / (1 + sqrtN);
            var gPlus = RgSettings.N * ((1 - nAlphaMinus3Div2) / (1 - nAlphaMinus1Div2));
            var gMinus = RgSettings.N * ((1 + nAlphaMinus3Div2) / (1 + nAlphaMinus1Div2));

            var rgPlus = new RGPoint(rPlus, gPlus);
            var rgMinus = new RGPoint(rMinus, gMinus);

            var cPlus = rgPlus.CReversed;
            var cMinus = rgMinus.CReversed;

            DrawCross(cPlus, Config.White);
            DrawCross(cMinus, Config.Black);

            var sing = new CPoint(1, RgSettings.Lambda, RgSettings.Lambda2);
            DrawXCross(sing, Config.White);

            gr.Save();

            fr.Bitmap = new Bitmap(bmp);
            DrawPoint();
        }

        private void DrawPoint()
        {
            if (_pointSelected == null || PointTrack == null)
                return;

            var pt = PointTrack[PointNumber];
            FillPoint(pt, Config.Black);
        }

        private static void SetPixel(Bitmap bmp, int x, int y, Color color)
        {
            if (bmp == null)
                return;
            lock (bmp)
            {
                bmp.SetPixel(x, y, color);
            }
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void PictureBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var fr = _stack.Peek();
            var r = fr.Rectangle;
            var pixWd = r.Width / pictureBox.Width;
            var pixHt = r.Height / pictureBox.Height;
            var newX = r.X + e.X * pixWd;
            var newY = r.Y + e.Y * pixHt;

            if (_pointSelecting)
            {
                var c0 = newX;
                var c1 = newY;
                var rd = c0 * c0 + c1 * c1;
                var c2 = 1 - rd;
                if (rd <= 1)
                {
                    c2 = Math.Sqrt(c2);
                    var cpt = new CPoint(c0, c1, c2);
                    _pointSelected = cpt;
                    PointNumber = 0;
                    DrawPoint();
                }
                _pointSelecting = false;
                var tr = new TrackPoint(this, PointTrack.Count);
                tr.Show(this);
                track.Lines = PointTrack.Select(p => string.Format("{0} {1}", p, p.RG(CProjection.C0C1))).ToArray();
            }
            else
            {
                var newW = 20 * pixWd;
                var newH = 20 * pixHt;
                var newRect = new DRect { X = newX, Y = newY, Width = newW, Height = newH };
                var newFr = new ProcessingFrame { Rectangle = newRect };
                _stack.Push(newFr);
                Redraw();
            }
        }

        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (_pointSelecting)
                return;

            RedrawScene(gr => gr.TryDraw(g => g.DrawRectangle(Config.BlackPen, e.X, e.Y, 20, 20)));
        }

        public void RedrawScene(Action<Graphics> redrawAction = null)
        {
            var fr = _stack.Peek();
            var bmp = fr.Bitmap;
            if (bmp == null)
                return;
            lock (bmp)
            {
                var oldImg = pictureBox.Image;
                pictureBox.Image = new Bitmap(bmp);

                var gr = Graphics.FromImage(pictureBox.Image);
                if (redrawAction != null)
                    redrawAction(gr);
                gr.Save();
                DrawPoint();
                pictureBox.Refresh();
                if (oldImg != null)
                    oldImg.Dispose();
            }
        }

        private void BackToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_stack.Count < 2)
                return;

            var peek = _stack.Pop();
            pictureBox.Image = new Bitmap(peek.Bitmap);
            DrawPoint();
            pictureBox.Refresh();
        }

        private void SetPointToolStripMenuItemClick(object sender, EventArgs e)
        {
            _pointSelecting = true;
        }

        public void CleanPoint(object sender, EventArgs e)
        {
            _pointSelected = null;
            _pointTrack = null;
            track.Lines = null;
            RedrawScene();
        }
    }
}


