using System;
using System.Drawing;
using System.Windows.Forms;
using RGDynApp.UI;

namespace RGDynApp
{
    public partial class Main : Form
    {
        private readonly RGProcessor _processor;

        private MouseMovingState _mouseMovingState;

        public Main()
        {
            InitializeComponent();
            _processor = new RGProcessor(_plotPanel);
        }

        private void initializeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var initDlg = new Initialize();
            var res = initDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                _processor.Initialize(initDlg.Alpha, initDlg.N);
                _processor.StartNewProcesing();
            }
        }

        private void BeginSelect(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            var currentPoint = new Point(e.X, e.Y);

            if (_mouseMovingState != null)
            {
                _mouseMovingState = null;
                return;
            }

            _mouseMovingState = new MouseMovingState { Current = currentPoint, Start = currentPoint };
        }

        private void ContinueSelect(object sender, MouseEventArgs e)
        {
            var currentPoint = new Point(e.X, e.Y);

            var pb = CheckCanDrawRect(sender, e);
            if (pb == null)
                return;

            _mouseMovingState.Current = currentPoint;
            var rect = GetRectangle(_mouseMovingState.Start, _mouseMovingState.Current);
            DrawRectangle(rect);
        }

        private void EndSelect(object sender, MouseEventArgs e)
        {
            var pb = CheckCanDrawRect(sender, e);
            if (pb == null)
                return;

            var currentPoint = new Point(e.X, e.Y);

            _mouseMovingState.End = currentPoint;

            var rect = GetRectangle(_mouseMovingState.Start, _mouseMovingState.End);
            if (rect.Width < 2 || rect.Height < 2)
            {
                _mouseMovingState = null;
                return;
            }
            DrawRectangle(rect);

            var pt1 = _processor.Current.MapToRGFrame(new Point(rect.Left, rect.Bottom));
            var pt2 = _processor.Current.MapToRGFrame(new Point(rect.Right, rect.Top));
            var rectF = new RectangleF(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);
            _processor.CreateNew(rectF, _plotPanel.Size);
            _processor.Draw();

            _mouseMovingState = null;
        }

        private PictureBox CheckCanDrawRect(object sender, MouseEventArgs eargs)
        {
            if (eargs.Button != MouseButtons.Left)
                return null;

            if (_mouseMovingState == null)
                return null;

            var pb = sender as PictureBox;
            return pb;
        }

        public static Rectangle GetRectangle(Point p1, Point p2)
        {
            var x1 = Math.Min(p1.X, p2.X);
            var x2 = Math.Max(p1.X, p2.X);
            var y1 = Math.Min(p1.Y, p2.Y);
            var y2 = Math.Max(p1.Y, p2.Y);
            var rect = new Rectangle(x1, y1, (x2 - x1), (y2 - y1));
            return rect;
        }

        private void DrawRectangle(Rectangle rect)
        {
            var img = new Bitmap(_processor.Current.OriginalImage);
            var oldImage = _plotPanel.Image;

            var gr = Graphics.FromImage(img);
            var pen = new Pen(RGScene.MarkupColor);
            gr.DrawRectangle(pen, rect);
            gr.Save();

            _plotPanel.Image = img;

            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }
    }
}
