using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RgLib;

namespace RGLines
{
    public partial class RgLinesReverse : Form
    {
        #region Private

        private const string BgFileName = "bg.bmp";

        private Bitmap _bg;

        private Bitmap _bgWithLines;

        private Bitmap _bgWithArea;

        private double _a;

        private double _b;

        private double _a1;

        private double _b1;

        private double _a11;

        private double _b11;

        private double _rmin;

        private double _rmax;

        private double _rstep;

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

        private bool _linesShown;

        private bool _areasShown;

        private double _h1;

        private double _h2;

        private int _areaStep;

        private bool _arc;

        private List<CPoint> _areaInitialSetCPositive;

        private List<CPoint> _areaInitialSetCNegative;

        private List<List<CPoint>> _iteratedAreasCPositive;

        private List<List<CPoint>> _iteratedAreasCNegative;

        #endregion

        #region Constructors

        public RgLinesReverse()
        {
            InitializeComponent();

            InitialFigure();
            ApplyRgSettings();
            BtnDefBClick(this, new EventArgs());
            ApplyLineSettings();
            trackLineGroup.Visible = false;
            trackAreaGroup.Visible = false;
            Redraw();
        }

        #endregion

        private void BnApplyClick(object sender, EventArgs e)
        {
            ApplyRgSettings();
            Redraw();
        }

        #region Event handlers

        private void BtnDefBClick(object sender, EventArgs e)
        {
            _b = -(RgSettings.N - 1) / (RgSettings.N - RgSettings.Lambda);
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
            trackLineGroup.Visible = true;
            _linesShown = true;
            ApplyLineSettings();
            Redraw();
        }

        private void BtnHideLineClick(object sender, EventArgs e)
        {
            trackLineGroup.Visible = false;
            _linesShown = false;
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

        private void BtnApplyAreaClick(object sender, EventArgs e)
        {
            trackAreaGroup.Visible = true;
            _areasShown = true;
            ApplyAreaSettings();
            Redraw();
        }

        private void BtnHideAreaClick(object sender, EventArgs e)
        {
            trackAreaGroup.Visible = false;
            _areasShown = false;
            Redraw();
        }

        private void TrackAreaValueChanged(object sender, EventArgs e)
        {
            _areaStep = trackArea.Value;
            RedrawArea();
            txtAreaIteration.Text = _areaStep.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnAreaPrevIterationClick(object sender, EventArgs e)
        {
            if (trackArea.Value != 0)
                trackArea.Value = trackArea.Value - 1;
            trackArea.Update();
        }

        private void BtnAreaNextIterationClick(object sender, EventArgs e)
        {
            if (trackArea.Value != trackArea.Maximum)
                trackArea.Value = trackArea.Value + 1;
            trackArea.Update();
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
                var img = RG.GetBg(pictureBox.Width, pictureBox.Height, CProjection.UpC0C1);
                img.Save(BgFileName, ImageFormat.Bmp);
                _bg = img as Bitmap;
            }
            if (_bg != null)
            {
                pictureBox.Image = _bg.Clone() as Bitmap;
            }
        }

        private void ApplyRgSettings()
        {
            var alpha = double.Parse(txtAlpha.Text);
            var n = double.Parse(txtN.Text);
            RgSettings.Build(alpha, n);
        }

        private void ApplyLineSettings()
        {
            _a = double.Parse(txtLineA.Text);
            _a1 = (RgSettings.N - 1) * _a / (_a - _b + (RgSettings.N + 1) * Math.Pow(RgSettings.Lambda, -1));
            _a11 = RgSettings.Lambda * _a;
            _b = double.Parse(txtLineB.Text);
            _b1 = (_b - RgSettings.N * _a) / (1 - RgSettings.N);
            _b11 = (1 + RgSettings.Lambda * _b) / RgSettings.N - 1;
            _rmin = double.Parse(txtRMin.Text);
            _rmax = double.Parse(txtRMax.Text);
            _rstep = double.Parse(txtRStep.Text);
            var steps = (int)((_rmax - _rmin) / _rstep) - 1;
            trackLine.Minimum = 0;
            trackLine.Maximum = steps;
            trackLine.Value = 0;
            _currentStep = 0;
        }

        private void ApplyAreaSettings()
        {
            trackArea.Value = 0;
            _areaStep = 0;
            _arc = checkBoxHOnlyArc.Checked;
            _h1 = double.Parse(txtAreaH1.Text);
            _h2 = double.Parse(txtAreaH2.Text);
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

        private void RedrawWithLines()
        {
            if (_bgWithLines != null)
            {
                _bgWithLines.Dispose();
            }
            _bgWithLines = _bg.Clone() as Bitmap;
            _rline1 = RGPoint.ParabolaReversed(_a, _b, _rmin, _rmax, _rstep).ToList();
            _line1 = _rline1.Select(rg => rg.CReversed).ToList();

            _rline2 = new List<RGPoint>();
            foreach (var rp1 in _rline1)
            {
                var r = rp1.R;
                var r1 = RgSettings.Lambda * ((_a - _b + (RgSettings.N - 1) * Math.Pow(RgSettings.Lambda, -1)) / (1 - RgSettings.N)) * ((r - _a1) / (r - _b1));
                var g1 = (r1 - _a11) / (r1 - _b11) * Math.Pow(r1 + 1, 2);
                var rp2 = new RGPoint { R = r1, G = g1 };
                _rline2.Add(rp2);
            }
            _line2 = _rline2.Select(rg => rg.CReversed).ToList();

            var nearestToB = _rline1.OrderBy(rp => Math.Abs(rp.R - _b)).FirstOrDefault();
            var nearestToL = _rline1.OrderBy(rp => Math.Abs(rp.R - -Math.Pow(RgSettings.Lambda, -1))).FirstOrDefault();

            var cNearestToB = nearestToB != null ? nearestToB.CReversed : null;
            var cNearesToL = nearestToL != null ? nearestToL.CReversed : null;

            if (_bgWithLines != null)
            {
                var gr = Graphics.FromImage(_bgWithLines);

                var pen1 = new Pen(Config.Black);
                for (var i = 0; i < _line1.Count - 1; i++)
                {
                    var c1 = _line1[i];
                    var c2 = _line1[i + 1];
                    RG.DrawLine(X, Y, _xpxsz, _ypxsz, _sz, pen1, c1, c2, gr, CProjection.UpC0C1);
                }

                var pen2 = new Pen(Config.White);
                for (var i = 0; i < _line2.Count - 1; i++)
                {
                    var c1 = _line2[i];
                    var c2 = _line2[i + 1];
                    RG.DrawLine(X, Y, _xpxsz, _ypxsz, _sz, pen2, c1, c2, gr, CProjection.UpC0C1);
                }

                if (cNearestToB != null) RG.FillPoint(X, Y, _xpxsz, _ypxsz, _sz, Config.Red, cNearestToB, gr, CProjection.UpC0C1);
                if (cNearesToL != null) RG.FillPoint(X, Y, _xpxsz, _ypxsz, _sz, Config.Yellow, cNearesToL, gr, CProjection.UpC0C1);
                gr.Save();
            }
            ChangePictureBoxPicture(_bgWithLines);
            pictureBox.Update();
        }

        private void RedrawWithArea()
        {
            if (_bgWithArea != null)
            {
                _bgWithArea.Dispose();
            }
            _bgWithArea = _bg.Clone() as Bitmap;

            var initialLstPositive = (_arc ? RGPoint.GetArcPositive(_h1, _h2, _xpxsz, CProjection.UpC0C1) : RGPoint.GetTrianglePositive(_h1, _h2, _xpxsz, _ypxsz, CProjection.UpC0C1)).ToList();
            var initialLstNegative = (_arc ? RGPoint.GetArcNegative(_h1, _h2, _xpxsz, CProjection.UpC0C1) : RGPoint.GetTriangleNegative(_h1, _h2, _xpxsz, _ypxsz, CProjection.UpC0C1)).ToList();
            _areaInitialSetCPositive = initialLstPositive.Select(e => e.Key).ToList();
            _areaInitialSetCNegative = initialLstNegative.Select(e => e.Key).ToList();

            RG.FillArea(X, Y, _xpxsz, _ypxsz, _sz, Config.Yellow, _areaInitialSetCPositive, _bgWithArea, CProjection.UpC0C1);
            RG.FillArea(X, Y, _xpxsz, _ypxsz, _sz, Config.Red, _areaInitialSetCNegative, _bgWithArea, CProjection.UpC0C1);

            var iteratedPositive = RGPoint.ReverseIteratedMany(_areaInitialSetCPositive, trackArea.Maximum, CProjection.UpC0C1).ToList();
            var iteratedNegative = RGPoint.ReverseIteratedMany(_areaInitialSetCNegative, trackArea.Maximum, CProjection.UpC0C1).ToList();
            _iteratedAreasCPositive = iteratedPositive.Select(e => e.Key.ToList()).ToList();
            _iteratedAreasCPositive.Insert(0, _areaInitialSetCPositive);
            _iteratedAreasCNegative = iteratedNegative.Select(e => e.Key.ToList()).ToList();
            _iteratedAreasCNegative.Insert(0, _areaInitialSetCNegative);

            ChangePictureBoxPicture(_bgWithArea);
            pictureBox.Update();
        }

        private void RedrawWithoutAnything()
        {
            ChangePictureBoxPicture(_bg);
        }

        private void Redraw()
        {
            if (_linesShown)
            {
                RedrawWithLines();
            }
            else if (_areasShown)
            {
                RedrawWithArea();
            }
            else
            {
                RedrawWithoutAnything();
            }
        }

        private void RedrawPoint()
        {
            ChangePictureBoxPicture(_bgWithLines);
            var gr = Graphics.FromImage(pictureBox.Image);
            var p1 = _line1[_currentStep];
            RG.FillPoint(X, Y, _xpxsz, _ypxsz, _sz, Config.Black, p1, gr, CProjection.UpC0C1);
            var p2 = _line2[_currentStep];
            RG.FillPoint(X, Y, _xpxsz, _ypxsz, _sz, Config.White, p2, gr, CProjection.UpC0C1);
        }

        private void RedrawArea()
        {
            ChangePictureBoxPicture(_bg);
            var img = pictureBox.Image.Clone() as Bitmap;
            /*
            var gr = Graphics.FromImage(img);

            RG.DrawPoint(X, Y, _xpxsz, _ypxsz, _sz, Config.White, new CPoint(0, 0, 1), gr, CProjection.UpC0C1);
            RG.DrawPoint(X, Y, _xpxsz, _ypxsz, _sz, Config.Black, new CPoint(1, 0, 0), gr, CProjection.UpC0C1);

            var sqrtN = Math.Sqrt(RgSettings.N);
            var nAlphaMinus1 = Math.Pow(RgSettings.N, RgSettings.Alpha - 1);
            var nAlphaMinus3Div2 = Math.Pow(RgSettings.N, RgSettings.Alpha - 1.5);
            var nAlphaMinus1Div2 = Math.Pow(RgSettings.N, RgSettings.Alpha - 0.5);

            var rPlus = (sqrtN - nAlphaMinus1) / (1 - sqrtN);
            var rMinus = (-sqrtN - nAlphaMinus1) / (1 + sqrtN);
            var gPlus = RgSettings.N * ((1 - nAlphaMinus3Div2) / (1 - nAlphaMinus1Div2));
            var gMinus = RgSettings.N * ((1 + nAlphaMinus3Div2) / (1 + nAlphaMinus1Div2));

            var rgPlus = new RGPoint(rPlus, gPlus);
            var rgMinus = new RGPoint(rMinus, gMinus);

            var cPlus = rgPlus.CReversed;
            var cMinus = rgMinus.CReversed;

            RG.DrawCross(X, Y, _xpxsz, _ypxsz, _sz, Config.White, cPlus, gr, CProjection.UpC0C1);
            RG.DrawCross(X, Y, _xpxsz, _ypxsz, _sz, Config.Black, cMinus, gr, CProjection.UpC0C1);

            var sing = new CPoint(1, RgSettings.Lambda, RgSettings.Lambda2);
            RG.DrawXCross(X, Y, _xpxsz, _ypxsz, _sz, Config.Black, sing, gr, CProjection.UpC0C1);

            var ptsPositive = _iteratedAreasCPositive[_areaStep];
            var ptsNegative = _iteratedAreasCNegative[_areaStep];
            RG.FillArea(X, Y, _xpxsz, _ypxsz, _sz, Config.Yellow, ptsPositive, img, CProjection.UpC0C1);
            RG.FillArea(X, Y, _xpxsz, _ypxsz, _sz, Config.Red, ptsNegative, img, CProjection.UpC0C1);
            gr.Save();
             * 
             * */

            ChangePictureBoxPicture(img);
        }

        #endregion
    }
}
