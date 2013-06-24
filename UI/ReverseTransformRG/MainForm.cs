using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ReverseTransform
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
      get { return _pointTrack ?? (_pointTrack = _pointSelected.ReverseTrack(Config.ReserverInterestedPoint, CProjection.UpC1C2).ToList()); }
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
      var bgClr = RG.GetColorReversed(new CPoint(p1), CProjection.UpC1C2);
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

    private Point NPt(CPoint p)
    {
      var rg = p.RG(CProjection.UpC1C2);
      return NPt(rg.R, rg.G);
    }

    private void FillPixel(Bitmap bmp, double x, double y)
    {
      var r = x;
      var g = y;
      var rg = new RGPoint { R = r, G = g };
      var cpt = rg.CReversed;
      var color = RG.GetColorReversed(cpt, CProjection.UpC1C2);
      var pt = NPt(r, g);
      SetPixel(bmp, pt.X, pt.Y, color);
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

      var ix = (float)(Math.Abs(r.X) / onepxw);
      var jy = (float)(Math.Abs(r.Y) / onepxh);
      var ix1 = ix;
      var jy1 = jy;
      gr.TryDraw(g => g.DrawEllipse(Config.WhitePen, ix1 - 3, jy1 - 3, 6, 6));
      ix = (float)(Math.Abs(1 - r.X) / onepxw);
      jy = (float)(Math.Abs(r.Y) / onepxh);
      gr.TryDraw(g => g.DrawEllipse(Config.RedPen, ix - 3, jy - 3, 6, 6));

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
        var rx = newX;
        var gx = newY;
        var rg = new RGPoint { R = rx, G = gx };
        var cpt = rg.CReversed;
        _pointSelected = cpt;
        PointNumber = 0;
        DrawPoint();
        _pointSelecting = false;
        var tr = new TrackPoint(this, PointTrack.Count);
        tr.Show(this);
        track.Lines = PointTrack.Select(p => string.Format("{0} {1}", p, p.RG(CProjection.C0C2))).ToArray();
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


