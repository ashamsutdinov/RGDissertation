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

    private decimal Alpha { get; set; }

    private decimal N { get; set; }

    private CPoint P1 { get; set; }

    private CPoint P2 { get; set; }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      InitializeConfig();
      Redraw();
    }

    private Color GetColor(CPoint pt)
    {
      if (!pt.IsNormal)
      {
        return Color.White;
      }
      else
      {
        var rp = pt.RG;
        var mix = 0;
        mix = rp.G <= 0 ? Color.Green.ToArgb() : Color.Yellow.ToArgb();
        var track = pt.ReverseTrack(P1, Alpha, N).ToList();
        if (track.Count == 100)
        {
          return Color.FromArgb(mix);
        }
        else
        {
          var last = track.Last();
          if (last.C1 < P1.C1)
          {
            return Color.Red; Color.FromArgb(Color.Red.ToArgb() | mix);
          }
          else
          {
            return Color.Blue; Color.FromArgb(Color.Blue.ToArgb() | mix);
          }
        }
      }
    }

    private void InitializeConfig()
    {
      Stack = new ProcessingStack();
      P1 = new CPoint(1, 0, 0);
      P2 = new CPoint(0, 0, 1);
      var frame = new ProcessingFrame { Rectangle = new DRect { X = (decimal)(-1.1), Y = (decimal)(-1.1), Width = (decimal)2.2, Height = (decimal)2.2 } };
      var conf = ConfigurationManager.AppSettings;
      Alpha = decimal.Parse(conf["Alpha"]);
      N = decimal.Parse(conf["N"]);
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
            c2 = c2.SqrtB();
          }
          var cpt = new CPoint(c0, c1, c2);
          SetPixel(i, j, GetColor(cpt));
        }
      }
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

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
