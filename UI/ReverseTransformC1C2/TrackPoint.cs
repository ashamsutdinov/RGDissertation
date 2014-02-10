using System;
using System.Windows.Forms;

namespace RgLib
{
  public partial class TrackPoint : Form
  {
    private readonly MainForm _form;

    public TrackPoint(MainForm form, int max)
    {
      InitializeComponent();
      trackBar.Minimum = 0;
      trackBar.Maximum = max - 1;
      trackBar.Value = 0;
      _form = form;
    }

    private void TrackBarValueChanged(object sender, EventArgs e)
    {
      _form.PointNumber = trackBar.Value;
      _form.RedrawScene();
    }

    protected override void OnClosed(EventArgs e)
    {
      base.OnClosed(e);
      _form.CleanPoint(this, e);
    }
  }
}
