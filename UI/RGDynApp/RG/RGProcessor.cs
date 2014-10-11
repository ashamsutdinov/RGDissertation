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
            if (step < 0 || step > 10)
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
    }
}