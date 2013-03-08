using System;
using System.Configuration;
using System.Drawing;
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

    public double Alpha;

    public double N;

    private readonly CPoint _p1 = new CPoint(1, 0, 0);

    private readonly Pen _blackPen = new Pen(Color.Black);

    private readonly Pen _redPen = new Pen(Color.Red);

    private readonly Pen _whitePen = new Pen(Color.White);

    private readonly Color _red = Color.Red;

    private readonly Color _blue = Color.Blue;

    private readonly Color _green = Color.Green;

    private readonly Color _yellow = Color.Yellow;

    private readonly Color _white = Color.White;

    private readonly Color _black = Color.Black;

    private bool _pointSelecting;

    private CPoint _pointSelected;

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
      /*      
      var baseClr = rp.G <= 0 ? _green : _yellow;
      var track = pt.ReverseTrack(_p1, Alpha, N);
      if (track.Count == 100)
      {
        return _black;
      }
      var last = track.Last();
      var clr = last.C1 < _p1.C1 ? _red : _blue;
      var resClr = Blend(baseClr, clr);
      return resClr;
       * */
      var cnt = 0;
      var end = pt.ReverseTrackEndPoint(_p1, Alpha, N, out cnt);
      if (cnt >= 100)
      {
        return _black;
      }
      var last = end;
      var clr = rp.G < 0 ? (last.C1 < _p1.C1 ? _yellow : _green) : (last.C1 < _p1.C1 ? _red : _blue);
      var resClr = clr;
      return resClr;
    }

    private void InitializeConfig()
    {
      var frame = new ProcessingFrame
      {
        Rectangle = new DRect
          {
            X = -1.1,
            Y = -1.1,
            Width = 2.2,
            Height = 2.2
          }
      };
      var conf = ConfigurationManager.AppSettings;
      Alpha = double.Parse(conf["Alpha"]);
      N = double.Parse(conf["N"]);
      CPoint.Lambda = Math.Pow(N, Alpha - 1);
      CPoint.LambdaMinus1 = 1 / CPoint.Lambda;
      CPoint.LambdaMinus2 = CPoint.LambdaMinus1 / CPoint.Lambda;
      CPoint.NLambdaMinus2 = N * CPoint.LambdaMinus2;
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
      gr.FillEllipse(new SolidBrush(clr), np.X - 2, np.Y - 2, 4, 4);
    }

    private void DrawLine(CPoint p1, CPoint p2, Color clr)
    {
      FillPoint(p1, clr);
      FillPoint(p2, clr);

      var np1 = NPt(p1);
      var np2 = NPt(p2);

      var gr = Graphics.FromImage(pictureBox.Image);
      gr.DrawLine(new Pen(clr), np1, np2);
    }

    private Point NPt(CPoint p)
    {
      var fr = _stack.Peek();
      var xA = fr.Rectangle.Width / pictureBox.Width;
      var yA = fr.Rectangle.Height / pictureBox.Height;
      var i = (int)((p.C0 - fr.Rectangle.X) / xA);
      var j = (int)((p.C1 - fr.Rectangle.Y) / yA);
      return new Point(i, j);
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

      var gr = Graphics.FromImage(pictureBox.Image);

      if (r.X < 0 && r.X + r.Width > 0 && r.Y < 0 && r.Y + r.Height > 0)
      {
        var ix = (float)(Math.Abs(r.X) / onepxw);
        var jy = (float)(Math.Abs(r.Y) / onepxh);
        gr.DrawEllipse(_whitePen, ix - 3, jy - 3, 6, 6);
      }
      if (r.X + r.Width > 1 && r.Y < 0 && r.Y + r.Height > 0)
      {
        var ix = (float)((Math.Abs(r.X) + 1) / onepxw);
        var jy = (float)(Math.Abs(r.Y) / onepxh);
        gr.DrawEllipse(_redPen, ix - 3, jy - 3, 6, 6);
      }

      gr.Save();

      fr.Bitmap = new Bitmap(bmp);
      DrawPoint();
    }

    private void DrawPoint()
    {
      if (_pointSelected == null)
        return;

      var dyn = _pointSelected.ReverseTrack(_p1, Alpha, N);
      for (var i = 0; i < dyn.Count - 1; i++)
      {
        var p1 = dyn[i];
        var p2 = dyn[i + 1];
        DrawLine(p1, p2, _black);
      }
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

    private void PictureBoxClick(object sender, EventArgs e)
    {

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
          DrawPoint();
        }
        _pointSelecting = false;
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

      var fr = _stack.Peek();
      var bmp = fr.Bitmap;
      if (bmp == null)
        return;
      lock (bmp)
      {
        var oldImg = pictureBox.Image;
        pictureBox.Image = new Bitmap(bmp);

        var gr = Graphics.FromImage(pictureBox.Image);
        gr.DrawRectangle(_blackPen, e.X, e.Y, 20, 20);
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

    private void CleanPointToolStripMenuItemClick(object sender, EventArgs e)
    {
      _pointSelected = null;
      pictureBox.Image = new Bitmap(_stack.Peek().Bitmap);
    }
  }
}

