using System;
using System.Configuration;
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

    private readonly ProcessingStack Stack = new ProcessingStack();

#if DECIMAL
    private decimal Alpha { get; set; }

    private decimal N { get; set; }
#else
    private double Alpha { get; set; }

    private double N { get; set; }
#endif

    private readonly CPoint _p1 = new CPoint(1, 0, 0);

    private readonly CPoint _p2 = new CPoint(0, 0, 1);

    private readonly Brush _blackBrush = new SolidBrush(Color.Black);

    private readonly Pen _blackPen = new Pen(Color.Black);

    private readonly Pen _redPen = new Pen(Color.Red);

    private readonly Pen _whitePen = new Pen(Color.White);

    private readonly Color _red = Color.Red;

    private readonly Color _blue = Color.Blue;

    private readonly Color _black = Color.Black;

    private readonly Color _green = Color.Green;

    private readonly Color _yellow = Color.Yellow;

    private readonly Color _white = Color.White;

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
      if (!pt.IsNormal)
      {
        return _white;
      }

      var rp = pt.RG;
      var baseClr = rp.G <= 0 ? _green : _yellow;
     
      var track = pt.ReverseTrack(_p1, Alpha, N).ToList();
      if (track.Count == 100)
      {
        return baseClr;
      }
      var last = track.Last();
      var clr = last.C1 < _p1.C1 ? _red : _blue;
      var resClr = Blend(baseClr, clr);
      return resClr;
    }

    private void InitializeConfig()
    {
      var frame = new ProcessingFrame
      {
        Rectangle = new DRect
          {
#if DECIMAL
            X = -1.1M,
            Y = -1.1M,
            Width = 2.2M,
            Height = 2.2M
#else
            X = -1.1,
            Y = -1.1,
            Width = 2.2,
            Height = 2.2
#endif
          }
      };
      var conf = ConfigurationManager.AppSettings;
#if DECIMAL
      Alpha = decimal.Parse(conf["Alpha"]);
      N = decimal.Parse(conf["N"]);
#else
      Alpha = double.Parse(conf["Alpha"]);
      N = double.Parse(conf["N"]);
#endif
      Stack.Push(frame);
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

    private void DrawParabola(Color clr, double a, double b, Graphics gr, DRect r, double onepxw, double onepxh, int w, int h)
    {
      var rgParabola = RGPoint.Parabola(Alpha, N, a, b);
      var cParabola = rgParabola.Select(rg => rg.C).ToList();
      var pen = new Pen(clr);

      for (var i = 0; i < cParabola.Count - 1; i++)
      {
        var cp1 = cParabola[i];
        var cp2 = cParabola[i + 1];

        var i1 = (cp1.C0 - r.X) / onepxw;
        var i2 = (cp2.C0 - r.X) / onepxw;
        var j1 = (cp1.C1 - r.Y) / onepxh;
        var j2 = (cp2.C1 - r.Y) / onepxh;

        if (Math.Abs(i1 - i2) <= 20)
        {
          if (i1 >= 0 && i1 <= w && i2 >= 0 && i2 <= w && j1 >= 0 && j1 <= h && j2 >= 0 && j2 <= h)
          {
            gr.DrawLine(pen, (float) i1, (float) j1, (float) i2, (float) j2);
          }
        }
      }
    }

    private void Redraw()
    {
      var bmp = Recreate();
      var fr = Stack.Peek();
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
#if DECIMAL
            c2 = c2.SqrtB();
#else
            c2 = Math.Sqrt(c2);
#endif
          }
          var cpt = new CPoint(c0, c1, c2);
          SetPixel(bmp, i, j, GetColor(cpt));
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

      for (var a = -5; a <= 5; a+=1)
      {
        var cl = (a + 5) * 25;
        var clr = Color.FromArgb(255, cl, cl, cl);
        DrawParabola(clr, a, 1, gr, r, onepxw, onepxh, w, h);
      }

      gr.Save();

      fr.Bitmap = new Bitmap(bmp);
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
      var fr = Stack.Peek();
      var r = fr.Rectangle;
      var pixWd = r.Width / pictureBox.Width;
      var pixHt = r.Height / pictureBox.Height;
      var newX = r.X + e.X * pixWd;
      var newY = r.Y + e.Y * pixHt;
      var newW = 20 * pixWd;
      var newH = 20 * pixHt;
      var newRect = new DRect { X = newX, Y = newY, Width = newW, Height = newH };
      var newFr = new ProcessingFrame { Rectangle = newRect };
      Stack.Push(newFr);
      Redraw();
    }

    private void PictureBoxMouseMove(object sender, MouseEventArgs e)
    {
      var fr = Stack.Peek();
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

        pictureBox.Refresh();
        if (oldImg != null)
          oldImg.Dispose();
      }
    }
  }
}
