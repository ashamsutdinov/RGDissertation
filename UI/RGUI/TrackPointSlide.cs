using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using RenormGroups;

namespace RGUI
{
  public partial class TrackPointSlide : Form
  {
    public event TrackPointSlideChangedEventDelegate TrackPointChanged;

    public event TrackPointSlideDestroyedEventDelegate TrackPointDestroyed;

    protected void OnTrackPointDestroyed(object sender, EventArgs eargs)
    {
      TrackPointChanged = null;
      var handler = TrackPointDestroyed;
      if (handler != null)
      {
        handler(this);
      }
    }

    protected void OnTrackPointChanged(object sender, EventArgs eargs)
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

    public TrackPointSlide(int maxTrack, CPoint initialPoint, double n, double alpha)
    {
      InitializeComponent();
      InitialPoint = initialPoint;
      N = n;
      Alpha = alpha;
      slideCurrent.Minimum = 0;
      slideCurrent.Maximum = maxTrack;
      slideCurrent.Value = 0;
      slideCurrent.ValueChanged += OnTrackPointChanged;
      txtTo.Validated += OnTrackPointChanged;
      txtTo.Text = slideCurrent.Value.ToString(CultureInfo.InvariantCulture);
      Closing += OnTrackPointDestroyed;
      OnTrackPointChanged(slideCurrent, new EventArgs());
    }

    public bool ShowFullPath
    {
      get { return chShowFullTrack.Checked; }
    }

    public int PointsCount
    {
      get { return int.Parse(txtTo.Text); }
    }

    public CPoint InitialPoint { get; set; }

    public double N { get; set; }

    public double Alpha { get; set; }
  }
}
