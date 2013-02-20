using System;
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

    private double Alpha { get; set; }

    private double N { get; set; }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      InitializeConfig();
      InitializeArea();
    }

    private void InitializeConfig()
    {
      
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
      var bmp = pictureBox.Image as Bitmap;
      if (bmp != null)
      {
        lock (bmp)
        {
          bmp.SetPixel(x, y, color);
        }
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
