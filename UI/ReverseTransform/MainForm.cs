using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReverseTransform
{
  public partial class MainForm : Form
  {
    internal class Pixel
    {
      public int X { get; set; }

      public int Y { get; set; }

      public Color Color { get; set; }
    }

    public MainForm()
    {
      InitializeComponent();
    }

    private Queue<Pixel> Pixels { get; set; }

    private Task GrTask { get; set; }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      Pixels = new Queue<Pixel>();
      InitializeArea();
      InitializePainter();
    }

    private void InitializePainter()
    {
      GrTask = new Task(() =>
      {
        while (Visible)
        {
          var bmp = pictureBox.Image as Bitmap;
          if (bmp == null || !Pixels.Any())
            continue;
          lock (bmp)
          {
            var p = Pixels.Dequeue();
            bmp.SetPixel(p.X, p.Y, p.Color);
          }
        }
      });
      GrTask.Start();
    }

    private void InitializeArea()
    {
      var r = new Random();
      pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
      for (var i = 0; i < pictureBox.Width; i++)
      {
        for (var j = 0; j < pictureBox.Height; j++)
        {
          SetPixel(i, j, Color.FromArgb(r.Next()));
        }
      }
    }

    private void SetPixel(int x, int y, Color color)
    {
      Pixels.Enqueue(new Pixel { X = x, Y = y, Color = color });
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
