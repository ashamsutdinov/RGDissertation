using System;
using System.Globalization;
using System.Windows.Forms;

namespace RGUI
{
  public partial class TrackPointSlide : Form
  {
    public event TrackPointSlideChangedEventDelegate TrackPointChanged;

    public event TrackPointSlideDestroyedEventDelegate TrackPointDestroyed;

    protected virtual void OnTrackPointDestroyed(object sender, EventArgs eargs)
    {
      TrackPointChanged = null;
      var handler = TrackPointDestroyed;
      if (handler != null)
      {
        handler(this);
      }
    }

    protected virtual void OnTrackPointChanged(object sender, EventArgs eargs)
    {
      if (sender == slideCurrent)
      {
        txtTo.TextChanged -= OnTrackPointChanged;
        txtTo.Text = slideCurrent.Value.ToString(CultureInfo.InvariantCulture);
        txtTo.TextChanged += OnTrackPointChanged;
      }

      if (sender == txtTo)
      {
        slideCurrent.ValueChanged -= OnTrackPointChanged;
        slideCurrent.Value = PointsCount;
        slideCurrent.ValueChanged += OnTrackPointChanged;
      }

      var handler = TrackPointChanged;
      if (handler != null)
      {
        handler(this);
      }
    }

    public TrackPointSlide(int maxTrack)
    {
      InitializeComponent();
      slideCurrent.Minimum = 1;
      slideCurrent.Maximum = maxTrack;
      slideCurrent.Value = 1;
      slideCurrent.ValueChanged += OnTrackPointChanged;
      txtTo.Validated += OnTrackPointChanged;
      txtTo.Text = slideCurrent.Value.ToString(CultureInfo.InvariantCulture);
      Closing += OnTrackPointDestroyed;
    }

    public bool ShowFullPath
    {
      get { return chShowFullTrack.Checked; }
    }

    public int PointsCount
    {
      get { return int.Parse(txtTo.Text); }
    }
  }
}
