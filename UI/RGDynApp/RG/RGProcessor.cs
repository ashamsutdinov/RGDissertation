using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace RGDynApp
{
    public class RGProcessor :
        Stack<RGScene>
    {
        public double Alpha;

        public double N;

        public double NMinus1;

        public double Lambda;

        public double NSqrt;

        public double NAlphaMinus1;

        public double NAlphaMinus3To2;

        public double NAlphaMinus1To2;

        public double LambdaMinus1;

        public double LambdaMinus2;

        public double NLambdaMinus2;

        public double Lambda2;

        public double OneDivN;

        public double RPlus;

        public double RMinus;

        public double GPlus;

        public double GMinus;

        public double B0;

        public double CurveB;

        public PictureBox PlottingPanel;

        public RGSceneTransform DisplayedTransformation;

        public Tuple<Color, PointF, CPoint> BoundaryPointLeft;

        public Tuple<Color, PointF, CPoint> BoundaryPointRight; 

        public RGScene Current
        {
            get { return Peek(); }
        }

        public RGProcessor(PictureBox box)
        {
            PlottingPanel = box;
        }

        public void Initialize(double alpha, double n, double b)
        {
            Alpha = alpha;
            N = n;
            CurveB = b;
            Lambda = Math.Pow(N, Alpha - 1);
            LambdaMinus1 = Math.Pow(Lambda, -1);
            LambdaMinus2 = Math.Pow(Lambda, -2);
            NLambdaMinus2 = N * LambdaMinus2;
            Lambda2 = Lambda * Lambda;
            OneDivN = 1 / N;
            NMinus1 = Math.Pow(N, -1);
            NSqrt = Math.Pow(N, 0.5);
            NAlphaMinus1 = Math.Pow(N, Alpha - 1);
            NAlphaMinus3To2 = Math.Pow(N, Alpha - 1.5);
            NAlphaMinus1To2 = Math.Pow(N, Alpha - 0.5);

            RPlus = (NSqrt - NAlphaMinus1) / (1 - NSqrt);
            RMinus = (-NSqrt - NAlphaMinus1) / (1 + NSqrt);
            GPlus = N * (1 - NAlphaMinus3To2) / (1 - NAlphaMinus1To2);
            GMinus = N * (1 + NAlphaMinus3To2) / (1 + NAlphaMinus1To2);

            B0 = -(Lambda * N - Lambda) / (Lambda * N - 1);
        }

        public void CreateNew(RectangleF rgFrame, Size uiFrame)
        {
            var scene = new RGScene(rgFrame, uiFrame);
            DisplayedTransformation = new DirectRGTransformationC1C2();
            scene.ApplyTransformation(DisplayedTransformation, this);
            Push(scene);
        }

        public void StartNewProcesing()
        {
            while (Count > 0)
            {
                var i = Pop();
                i.Dispose();
            }
            CreateNew(new RectangleF(-1.1f, -1.1f, 2.2f, 2.2f), PlottingPanel.Size);
            Draw();
        }

        public void Draw()
        {
            var scene = Current;
            if (PlottingPanel.Image != null)
                PlottingPanel.Image.Dispose();
            PlottingPanel.Image = new Bitmap(scene.ResultedImage);
            PlottingPanel.Update();
        }

        public void DrawMarkupDynamics(int step)
        {
            if (step < 0 || step > 25)
            {
                Draw();
                return;
            }
            var scene = Current;
            var bmp = new Bitmap(scene.ResultedImage);
            var gr = Graphics.FromImage(bmp);
            DisplayedTransformation.ApplyMarkupDynamics(step, bmp, gr, scene, this);
            if (PlottingPanel.Image != null)
                PlottingPanel.Image.Dispose();
            PlottingPanel.Image = bmp;
            PlottingPanel.Update();
        }

        public void Back()
        {
            Pop();
            Draw();
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
                var clr1 = DisplayedTransformation.GetPixelColor(fpt1, Current, this);
                var fpt2 = new PointF(x + acc, mid);
                var clr2 = DisplayedTransformation.GetPixelColor(fpt2, Current, this);
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
                    var testClr = DisplayedTransformation.GetPixelColor(testPt, Current, this);
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
                BoundaryPointLeft = new Tuple<Color, PointF, CPoint>(pt1.Item1, curPt1, CPoint.New(curPt1, CProjection.C1C2));

                var acc2 = 0.000001f;
                var accuracyChanged2 = false;
                var afterAccuracyChangedSteps2 = 0;
                while (true)
                {
                    var testPt = new PointF(curPt2.X - acc2, curPt1.Y);
                    var testClr = DisplayedTransformation.GetPixelColor(testPt, Current, this);
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

                BoundaryPointRight = new Tuple<Color, PointF, CPoint>(pt2.Item1, curPt2, CPoint.New(curPt2, CProjection.C1C2));
            }

            DrawBoundaryPointDynamics(0);
        }

        public void DrawBoundaryPointDynamics(int step)
        {
            if (step < 0 || step > 25)
            {
                Draw();
                return;
            }
            var scene = Current;
            var bmp = new Bitmap(scene.ResultedImage);
            var gr = Graphics.FromImage(bmp);
            DisplayedTransformation.ApplyBoundaryPointDynamics(step, bmp, gr, BoundaryPointLeft, BoundaryPointRight, Current, this);
            if (PlottingPanel.Image != null)
                PlottingPanel.Image.Dispose();
            PlottingPanel.Image = bmp;
            PlottingPanel.Update();
        }
    }
}