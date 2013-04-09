using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ReverseTransform;

namespace RGLines
{
  public partial class RGLines : Form
  {
    #region Private

    private const string BgFileName = "bg.bmp";

    private Bitmap _bg;

    private Bitmap _bgWithLines;

    private double _alpha;

    private double _n;

    private double _a;

    private double _b;

    private double _a1;

    private double _b1;

    private double _a11;

    private double _b11;

    private double _rmin;

    private double _rmax;

    private double _rstep;

    private double _lambda;

    private int _currentStep;

    private List<RGPoint> _rline1;

    private List<RGPoint> _rline2;

    private List<CPoint> _line1;

    private List<CPoint> _line2;

    private const double X = -1.0;

    private const double Y = -1.0;

    private const double Xsz = 2.0;

    private const double Ysz = 2.0;

    private int _sz;

    private double _xpxsz;

    private double _ypxsz;

    #endregion

    #region Constructors

    public RGLines()
    {
      InitializeComponent();

      InitialFigure();
      ApplyRGSettings();
      BtnDefBClick(this, new EventArgs());
      ApplyLineSettings();
      Redraw();
    }

    #endregion

    private void BnApplyClick(object sender, EventArgs e)
    {
      ApplyRGSettings();
      Redraw();
    }

    #region Event handlers

    private void BtnDefBClick(object sender, EventArgs e)
    {
      _b = -(_n - 1) / (_n - _lambda);
      txtLineB.Text = _b.ToString(CultureInfo.InvariantCulture).Replace(".", ",");
    }

    private void TrackLineValueChanged(object sender, EventArgs e)
    {
      _currentStep = trackLine.Value;
      RedrawPoint();
      var pt = _rline1[_currentStep];
      txtStepR.Text = pt.R.ToString(CultureInfo.InvariantCulture);
    }

    private void BtnAppyLineSettingsClick(object sender, EventArgs e)
    {
      ApplyLineSettings();
      Redraw();
    }

    private void BtnBackClick(object sender, EventArgs e)
    {
      if (trackLine.Value != 0)
        trackLine.Value = trackLine.Value - 1;
      trackLine.Update();
    }

    private void BtnNextClick(object sender, EventArgs e)
    {
      if (trackLine.Value != trackLine.Maximum)
        trackLine.Value = trackLine.Value + 1;
      trackLine.Update();
    }

    #endregion

    #region Private members

    private void InitialFigure()
    {
      _xpxsz = Xsz / pictureBox.Width;
      _ypxsz = Ysz / pictureBox.Height;
      _sz = pictureBox.Width;
      if (File.Exists(BgFileName))
      {
        var img = Image.FromFile(BgFileName);
        if (img.Width != pictureBox.Width || img.Height != pictureBox.Height)
        {
          img.Dispose();
          File.Delete(BgFileName);
          InitialFigure();
        }
        else
        {
          _bg = img as Bitmap;
        }
      }
      else
      {
        var img = RG.GetBg(pictureBox.Width, pictureBox.Height);
        img.Save(BgFileName, ImageFormat.Bmp);
        _bg = img as Bitmap;
      }
      if (_bg != null)
      {
        pictureBox.Image = _bg.Clone() as Bitmap;
      }
    }

    private void ApplyRGSettings()
    {
      _alpha = double.Parse(txtAlpha.Text);
      _n = double.Parse(txtN.Text);
      _lambda = Math.Pow(_n, _alpha - 1);
    }

    private void ApplyLineSettings()
    {
      _a = double.Parse(txtLineA.Text);
      _a1 = ((_n - 1) * _a) / (_a - _b + (_n + 1) * Math.Pow(_lambda, -1));
      _a11 = _lambda * _a;
      _b = double.Parse(txtLineB.Text);
      _b1 = (_b - _n * _a) / (1 - _n);
      _b11 = (1 + _lambda * _b) / _n - 1;
      _rmin = double.Parse(txtRMin.Text);
      _rmax = double.Parse(txtRMax.Text);
      _rstep = double.Parse(txtRStep.Text);
      var steps = (int)((_rmax - _rmin) / _rstep) - 1;
      trackLine.Minimum = 0;
      trackLine.Maximum = steps;
      trackLine.Value = 0;
      _currentStep = 0;
    }

    private void ChangePictureBoxPicture(ICloneable newImageSource)
    {
      var newImg = newImageSource.Clone() as Bitmap;
      if (pictureBox.Image != null)
      {
        pictureBox.Image.Dispose();
      }
      pictureBox.Image = newImg;
    }

    private void Redraw()
    {
      if (_bgWithLines != null)
      {
        _bgWithLines.Dispose();
      }
      _bgWithLines = _bg.Clone() as Bitmap;
      _rline1 = RGPoint.Parabola(_lambda, _alpha, _n, _a, _b, _rmin, _rmax, _rstep).ToList();
      _line1 = _rline1.Select(rg => rg.C).ToList();

      _rline2 = new List<RGPoint>();
      foreach (var rp1 in _rline1)
      {
        var r = rp1.R;
        var r1 = _lambda * ((_a - _b + (_n - 1) * Math.Pow(_lambda, -1)) / (1 - _n)) * ((r - _a1) / (r - _b1));
        var g1 = ((r1 - _a11) / (r1 - _b11)) * Math.Pow(r1 + 1, 2);
        var rp2 = new RGPoint { R = r1, G = g1 };
        _rline2.Add(rp2);
      }
      _line2 = _rline2.Select(rg => rg.C).ToList();


      //_rline2 = RGPoint.Parabola(_lambda, _alpha, _n, _a, _b, _rmin, _rmax, _rstep, false).ToList();
      //_line2 = _rline2.Select(rg => rg.C).ToList();

      if (_bgWithLines != null)
      {
        var gr = Graphics.FromImage(_bgWithLines);

        var pen1 = new Pen(Color.Black);
        for (var i = 0; i < _line1.Count - 1; i++)
        {
          var c1 = _line1[i];
          var c2 = _line1[i + 1];
          RG.DrawLine(X, Y, _xpxsz, _ypxsz, _sz, pen1, c1, c2, gr);
        }

        var pen2 = new Pen(Color.White);
        for (var i = 0; i < _line2.Count - 1; i++)
        {
          var c1 = _line2[i];
          var c2 = _line2[i + 1];
          RG.DrawLine(X, Y, _xpxsz, _ypxsz, _sz, pen2, c1, c2, gr);
        }
        gr.Save();
      }
      ChangePictureBoxPicture(_bgWithLines);
      pictureBox.Update();
    }

    private void RedrawPoint()
    {
      ChangePictureBoxPicture(_bgWithLines);
      var gr = Graphics.FromImage(pictureBox.Image);
      var p1 = _line1[_currentStep];
      RG.FillPoint(X, Y, _xpxsz, _ypxsz, _sz, Color.Black, p1, gr);
      /*
      var rp1 = _rline1[_currentStep];
      var r = rp1.R;
      var r1 = _lambda * ((_a - _b + (_n - 1) * Math.Pow(_lambda, -1)) / (1 - _n)) * ((r - _a1) / (r - _b1));
      var g1 = ((r1 - _a11) / (r1 - _b11)) * Math.Pow(r1 + 1, 2);
      var rp2 = new RGPoint { R = r1, G = g1 };
       * */
      var p2 = _line2[_currentStep];
      RG.FillPoint(X, Y, _xpxsz, _ypxsz, _sz, Color.White, p2, gr);
    }

    #endregion
  }
}
