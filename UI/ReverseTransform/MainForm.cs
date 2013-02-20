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

    private ProcessingStack Stack { get; set; }

#if DECIMAL
    private decimal Alpha { get; set; }

    private decimal N { get; set; }
#else
    private double Alpha { get; set; }

    private double N { get; set; }
#endif

    private CPoint P1 { get; set; }

    // ReSharper disable UnusedAutoPropertyAccessor.Local
    private CPoint P2 { get; set; }
    // ReSharper restore UnusedAutoPropertyAccessor.Local

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
        return Color.White;
      }

      var rp = pt.RG;
      var baseClr = rp.G <= 0 ? Color.Green : Color.Yellow;
      var track = pt.ReverseTrack(P1, Alpha, N).ToList();
      if (track.Count == 100)
      {
        return baseClr;
      }
      var last = track.Last();
      var clr = last.C1 < P1.C1 ?
        Color.Red : Color.Blue;
      var resClr = Blend(baseClr, clr);
      return resClr;
    }

    private void InitializeConfig()
    {
      Stack = new ProcessingStack();
      P1 = new CPoint(1, 0, 0);
      P2 = new CPoint(0, 0, 1);
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

    private void Recreate()
    {
      if (pictureBox.Image != null)
      {
        pictureBox.Image.Dispose();
      }
      pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
    }

    private void Redraw()
    {
      Recreate();
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
          SetPixel(i, j, GetColor(cpt));
        }
      }

      var gr = Graphics.FromImage(pictureBox.Image);

      if (r.X < 0 && r.X + r.Width > 0 && r.Y < 0 && r.Y + r.Height > 0)
      {
        var ix = (float)(Math.Abs(r.X) / onepxw);
        var jy = (float)(Math.Abs(r.Y) / onepxh);
        gr.DrawEllipse(new Pen(Color.White), ix - 3, jy - 3, 6, 6);
      }
      if (r.X + r.Width > 1 && r.Y < 0 && r.Y + r.Height > 0)
      {
        var ix = (float)((Math.Abs(r.X) + 1) / onepxw);
        var jy = (float)(Math.Abs(r.Y) / onepxh);
        gr.DrawEllipse(new Pen(Color.Red), ix - 3, jy - 3, 6, 6);
      }

      gr.Save();
    }

    private void SetPixel(int x, int y, Color color)
    {
      var bmp = pictureBox.Image as Bitmap;
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
      var newW = 30 * pixWd;
      var newH = 30 * pixHt;
      var newRect = new DRect { X = newX, Y = newY, Width = newW, Height = newH };
      var newFr = new ProcessingFrame { Rectangle = newRect };
      Stack.Push(newFr);
      Redraw();
    }
  }
}
