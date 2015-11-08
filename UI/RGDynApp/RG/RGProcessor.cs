using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RGDynApp.RG
{
    public class RGProcessor :
        Stack<RGScene>
    {
        public double Alpha;

        public double N;

        private double _nMinus1;

        public double Lambda;

        public double NSqrt;

        private double _nAlphaMinus1;

        private double _nAlphaMinus3To2;

        private double _nAlphaMinus1To2;

        public double LambdaMinus1;

        public double LambdaMinus2;

        public double NLambdaMinus2;

        public double Lambda2;

        public double OneDivN;

        private double _rPlus;

        private double _rMinus;

        private double _gPlus;

        private double _gMinus;

        public double B0;

        private double _curveB;

        private readonly PictureBox _plottingPanel;

        private RGSceneTransform _displayedTransformation;

        private Tuple<Color, PointF, CPoint> _boundaryPointLeft;

        private Tuple<Color, PointF, CPoint> _boundaryPointRight;

        private Tuple<Color, PointF, CPoint> _trackPoint;

        public RGScene Current
        {
            get { return Peek(); }
        }

        public RGProcessor(PictureBox box)
        {
            _plottingPanel = box;
        }

        public void Initialize(double alpha, double n, double b)
        {
            Alpha = alpha;
            N = n;
            _curveB = b;
            Lambda = Math.Pow(N, Alpha - 1);
            LambdaMinus1 = Math.Pow(Lambda, -1);
            LambdaMinus2 = Math.Pow(Lambda, -2);
            NLambdaMinus2 = N * LambdaMinus2;
            Lambda2 = Lambda * Lambda;
            OneDivN = 1 / N;
            _nMinus1 = Math.Pow(N, -1);
            NSqrt = Math.Pow(N, 0.5);
            _nAlphaMinus1 = Math.Pow(N, Alpha - 1);
            _nAlphaMinus3To2 = Math.Pow(N, Alpha - 1.5);
            _nAlphaMinus1To2 = Math.Pow(N, Alpha - 0.5);

            _rPlus = (NSqrt - _nAlphaMinus1) / (1 - NSqrt);
            _rMinus = (-NSqrt - _nAlphaMinus1) / (1 + NSqrt);
            _gPlus = N * (1 - _nAlphaMinus3To2) / (1 - _nAlphaMinus1To2);
            _gMinus = N * (1 + _nAlphaMinus3To2) / (1 + _nAlphaMinus1To2);

            B0 = -(Lambda * N - Lambda) / (Lambda * N - 1);
        }

        public void CreateNew(RectangleF rgFrame, Size uiFrame)
        {
            var scene = new RGScene(rgFrame, uiFrame);
            _displayedTransformation = new DirectRGTransformationC1C2();
            scene.ApplyTransformation(_displayedTransformation, this);
            Push(scene);
        }

        public void StartNewProcesing()
        {
            while (Count > 0)
            {
                var i = Pop();
                i.Dispose();
            }
            CreateNew(new RectangleF(-1.1f, -1.1f, 2.2f, 2.2f), _plottingPanel.Size);
            Draw();
        }

        public void Draw()
        {
            var scene = Current;
            if (_plottingPanel.Image != null)
                _plottingPanel.Image.Dispose();
            _plottingPanel.Image = new Bitmap(scene.ResultedImage);
            _plottingPanel.Update();
        }

        public void DrawMarkupDynamics(int step)
        {
            if (step < 0 || step > 50)
            {
                Draw();
                return;
            }
            var scene = Current;
            var bmp = new Bitmap(scene.ResultedImage);
            var gr = Graphics.FromImage(bmp);
            _displayedTransformation.ApplyMarkupDynamics(step, bmp, gr, scene, this);
            if (_plottingPanel.Image != null)
                _plottingPanel.Image.Dispose();
            _plottingPanel.Image = bmp;
            _plottingPanel.Update();
        }

        public void Back()
        {
            Pop();
            Draw();
        }

        public void StartTrackPoint(PointF rgPoint, Size uiFrame)
        {
            var clr = _displayedTransformation.GetPixelColor(rgPoint, Current, this);
            _trackPoint = new Tuple<Color, PointF, CPoint>(clr, rgPoint, CPoint.New(rgPoint, CProjection.C1C2));
            DrawTrackPointDynamics(0);
        }

        public void StartBoundaryAnalysis(RectangleF rgFrame, Size uiFrame)
        {
            var mid = rgFrame.Y + rgFrame.Height / 2;
            var left = rgFrame.X;

            Tuple<Color, PointF, CPoint> pt1 = null;
            Tuple<Color, PointF, CPoint> pt2 = null;
            const float acc = 0.00001f;
            for (var x = left; x <= left + rgFrame.Width; x += acc)
            {
                var fpt1 = new PointF(x, mid);
                var clr1 = _displayedTransformation.GetPixelColor(fpt1, Current, this);
                var fpt2 = new PointF(x + acc, mid);
                var clr2 = _displayedTransformation.GetPixelColor(fpt2, Current, this);
                if (clr1 != clr2)
                {
                    pt1 = new Tuple<Color, PointF, CPoint>(clr1, fpt1, CPoint.New(fpt1, CProjection.C1C2));
                    pt2 = new Tuple<Color, PointF, CPoint>(clr2, fpt2, CPoint.New(fpt2, CProjection.C1C2));
                    break;
                }
            }

            if (pt1 != null && pt2 != null)
            {
                var curPt1 = pt1.Item2;
                var curClr1 = pt1.Item1;
                var curPt2 = pt2.Item2;
                var curClr2 = pt2.Item1;

                var acc1 = 0.000001f;
                var accuracyChanged1 = false;
                var afterAccuracyChangedSteps1 = 0;
                while (true)
                {
                    var testPt = new PointF(curPt1.X + acc1, curPt1.Y);
                    var testClr = _displayedTransformation.GetPixelColor(testPt, Current, this);
                    if (testClr != curClr1)
                    {
                        if (acc1 >= float.Epsilon*2)
                        {
                            acc1 = acc1/2;
                            accuracyChanged1 = true;
                            afterAccuracyChangedSteps1 = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        curPt1 = testPt;
                        if (accuracyChanged1)
                        {
                            afterAccuracyChangedSteps1++;
                            if (afterAccuracyChangedSteps1 >= 4)
                            {
                                break;
                            }
                        }
                    }
                }
                _boundaryPointLeft = new Tuple<Color, PointF, CPoint>(pt1.Item1, curPt1, CPoint.New(curPt1, CProjection.C1C2));

                var acc2 = 0.000001f;
                var accuracyChanged2 = false;
                var afterAccuracyChangedSteps2 = 0;
                while (true)
                {
                    var testPt = new PointF(curPt2.X - acc2, curPt1.Y);
                    var testClr = _displayedTransformation.GetPixelColor(testPt, Current, this);
                    if (testClr != curClr2)
                    {
                        if (acc2 >= float.Epsilon * 2)
                        {
                            acc2 = acc2 / 2;
                            accuracyChanged2 = true;
                            afterAccuracyChangedSteps2 = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        curPt2 = testPt;
                        if (accuracyChanged2)
                        {
                            afterAccuracyChangedSteps2++;
                            if (afterAccuracyChangedSteps2 >= 4)
                            {
                                break;
                            }
                        }
                    }
                }

                _boundaryPointRight = new Tuple<Color, PointF, CPoint>(pt2.Item1, curPt2, CPoint.New(curPt2, CProjection.C1C2));
            }

            DrawBoundaryPointDynamics(0);
        }

        public void DrawBoundaryPointDynamics(int step)
        {
            if (step < 0 || step > 50)
            {
                Draw();
                return;
            }
            var scene = Current;
            var bmp = new Bitmap(scene.ResultedImage);
            var gr = Graphics.FromImage(bmp);
            _displayedTransformation.ApplyBoundaryPointDynamics(step, bmp, gr, _boundaryPointLeft, _boundaryPointRight, Current, this);
            if (_plottingPanel.Image != null)
                _plottingPanel.Image.Dispose();
            _plottingPanel.Image = bmp;
            _plottingPanel.Update();
        }

        public void DrawTrackPointDynamics(int step)
        {
            if (step < 0 || step > 50)
            {
                Draw();
                return;
            }
            var scene = Current;
            var bmp = new Bitmap(scene.ResultedImage);
            var gr = Graphics.FromImage(bmp);
            _displayedTransformation.ApplyTrackPointDynamics(step, bmp, gr, _trackPoint, Current, this);
            if (_plottingPanel.Image != null)
                _plottingPanel.Image.Dispose();
            _plottingPanel.Image = bmp;
            _plottingPanel.Update();
        }
    }
}