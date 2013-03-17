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

    public List<CPoint> PointTrack
    {
      get { return _pointTrack ?? (_pointTrack = _pointSelected.ReverseTrack(Config.P1, Config.Alpha, Config.N, Config.Acc, Config.Count)); }
    }

    public int PointNumber { get; set; }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      InitializeConfig();
      Redraw();
    }

    public Color ColorMixer(Color c1, Color c2)
    {

      var r = Math.Min((c1.R + c2.R), 255);
      var g = Math.Min((c1.G + c2.G), 255);
      var b = Math.Min((c1.B + c2.B), 255);

      return Color.FromArgb(255, r, g, b);
    }

    public static Color Blend(Color color, Color backColor, double amount = 0.5)
    {
      var r = (byte)((color.R * amount) + backColor.R * (1 - amount));
      var g = (byte)((color.G * amount) + backColor.G * (1 - amount));
      var b = (byte)((color.B * amount) + backColor.B * (1 - amount));
      return Color.FromArgb(r, g, b);
    }

    private Color GetColor(CPoint pt)
    {
      var rp = pt.RG;
      var cnt = 0;
      var end = pt.ReverseTrackEndPoint(Config.P1, Config.Alpha, Config.N, out cnt, Config.Acc, Config.Count);
      if (cnt >= Config.Count)
      {
        return Config.Black;
      }
      var last = end;
      var clr = rp.G < 0 ? (last.C1 < Config.P1.C1 ? Config.Yellow : Config.Green) : (last.C1 < Config.P1.C1 ? Config.Red : Config.Blue);
      var resClr = clr;
      return resClr;
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
      var bgClr = GetColor(new CPoint(p1));
      var blend = Blend(clr, bgClr);
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
        SetPixel(bmp, pt.X, pt.Y, GetColor(cpt));
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

      /*
      for (var i = 0; i < w; i++)
      {
        for (var j = 0; j < h; j++)
        {
          var x = r.X + i * onepxw;
          var y = r.Y + j * onepxh;

          var c0 = x;
          var c1 = y;
          var rd = c0 * c0 + c1 * c1;
          var c2 = 1 - rd;
          if (rd <= 1)
          {
            c2 = Math.Sqrt(c2);
            var cpt = new CPoint(c0, c1, c2);
            SetPixel(bmp, i, j, GetColor(cpt));
          }
          else
          {
            SetPixel(bmp, i, j, _white);
          }
        }
      }
         * */


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

    public void DrawPoint()
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
        track.Lines = PointTrack.Select(p => string.Format("{0} {1}", p, p.RG)).ToArray();
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


