using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RgLib;

namespace RGTransform
{
    public partial class RgMain : Form
    {
        public RgMain()
        {
            InitializeComponent();
        }

        private readonly ProcessingStack _stack = new ProcessingStack();

        private bool _pointSelecting;

        private RGPoint _pointSelected;

        private List<RGPoint> _pointTrack;

        private List<RGPoint> PointTrack
        {
            get { return _pointTrack ?? (_pointTrack = _pointSelected.ReverseTrack(Config.ReserverInterestedPointRg).ToList()); }
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
                    X = RgSettings.X1,
                    Y = RgSettings.Y1,
                    Width = RgSettings.W,
                    Height = RgSettings.H
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

        private void FillPoint(RGPoint p1, Color clr)
        {
            var np = NPt(p1);
            var gr = Graphics.FromImage(pictureBox.Image);
            var bgClr = RG.GetColorReversed(new RGPoint(p1), CProjection.UpC0C1);
            var blend = RG.Blend(clr, bgClr);
            gr.TryDraw(g => g.FillEllipse(new SolidBrush(clr), np.X - 2, np.Y - 2, 4, 4));
            gr.TryDraw(g => g.DrawRectangle(new Pen(blend), np.X - 10, np.Y - 10, 20, 20));
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

        private Point NPt(RGPoint p)
        {
            return NPt(p.R, p.G);
        }

        private void FillPixel(Bitmap bmp, double x, double y)
        {
            var r = x;
            var g = y;
            var pt = NPt(r, g);
            var cpt = new RGPoint(r, g);
            var color = RG.GetColorReversed(cpt, CProjection.UpC0C1);
            SetPixel(bmp, pt.X, pt.Y, color);
        }

        private void Redraw()
        {
            var bmp = Recreate();
            var fr = _stack.Peek();
            var r = fr.Rectangle;
            var w = pictureBox.Width;
            var h = pictureBox.Height;
            var fw = w * 1.2;
            var fh = h * 1.2;
            var onepxw = r.Width / w;
            var onepxh = r.Height / h;
            var fonepxw = r.Width / fw;
            var fonepxh = r.Height / fh;

            for (var x = r.X; x <= r.X + r.Width; x += fonepxw)
            {
                for (var y = r.Y; y <= r.Y + r.Height; y += fonepxh)
                {
                    FillPixel(bmp, x, y);
                }
            }

            var gr = Graphics.FromImage(pictureBox.Image);

            var ix = (float)(Math.Abs(r.X) / onepxw);
            var jy = (float)(Math.Abs(r.Y) / onepxh);
            var ix1 = ix;
            var jy1 = jy;
            gr.TryDraw(g => g.DrawEllipse(Config.WhitePen, ix1 - 3, jy1 - 3, 6, 6));


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
                var rp = newX;
                var gp = newY;
                var cpt = new RGPoint(rp, gp);
                _pointSelected = cpt;
                PointNumber = 0;
                DrawPoint();
                _pointSelecting = false;
                var tr = new TrackPoint(this, PointTrack.Count);
                tr.Show(this);
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
            RedrawScene();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
