using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms.DataVisualization.Charting;

namespace RGDynApp
{
    public class DirectRGTransformationC1C2 :
        RGSceneC1C2Transform
    {
        protected bool Initialized;

        protected double LeftC2Limit;

        protected double RightC2Limit;

        protected int IterationsLimit;

        protected double DistanceLimit;

        protected CPoint CriticalPoint;

        protected CPoint CriticalPointOpposite;

        public List<List<CPoint>> MarkupVPoints = new List<List<CPoint>>();

        public List<List<CPoint>> MarkupHPoints = new List<List<CPoint>>();

        protected void Initialize(RGProcessor processor)
        {
            if (Initialized)
                return;

            var leftRG = new RGPoint(2, 0);
            var leftC = leftRG.C(CProjection.C1C2);
            LeftC2Limit = leftC.C2;

            var rightRG = new RGPoint(-1000, ((-1000 + 1) / (-1000 - processor.B0)) * Math.Pow(-1000 + 1, 2));
            var rightC = rightRG.C(CProjection.C1C2);
            RightC2Limit = rightC.C2;

            CriticalPoint = processor.Alpha > 1 ? new CPoint(0, 0, 1) : new CPoint(1, 0, 0);
            CriticalPointOpposite = CriticalPoint.Opposite;

            IterationsLimit = 100;
            DistanceLimit = 0.0001;
            Initialized = true;

            MarkupVPoints.Clear();
            MarkupHPoints.Clear();

            for (var i = 0; i <= 10; i++)
            {
                MarkupVPoints.Add(new List<CPoint>());
                MarkupHPoints.Add(new List<CPoint>());
            }

            for (var y = -1f; y <= 1f; y += 0.00001f)
            {
                var c = CPoint.New(new PointF(0, y), CProjection.C1C2);
                MarkupVPoints[0].Add(c);
                for (var i = 1; i <= 10; i++)
                {
                    c = DirectIterated(c, CProjection.C1C2, processor);
                    MarkupVPoints[i].Add(c);
                }
            }

            for (var x = -1f; x <= 1f; x += 0.00001f)
            {
                var c = CPoint.New(new PointF(x, 0), CProjection.C1C2);
                MarkupHPoints[0].Add(c);
                for (var i = 1; i <= 10; i++)
                {
                    c = DirectIterated(c, CProjection.C1C2, processor);
                    MarkupHPoints[i].Add(c);
                }
            }
        }

        protected override Color GetC1C2Color(Bitmap bmp, CPoint cpt, RGPoint rg, RGScene scene, RGProcessor processor)
        {
            Initialize(processor);

            var i = 0;
            Color res;
            var start = cpt;
            var current = start;


            while (i < IterationsLimit)
            {
                current = DirectIterated(current, CProjection.C1C2, processor);
                if (processor.Alpha > 1 && rg.G > 0)
                {
                    if (current.C1 < 0 && current.C2 > LeftC2Limit)
                    {
                        break;
                    }
                    if (current.C1 > 0 && current.C2 > RightC2Limit)
                    {
                        break;
                    }
                }
                else
                {
                    var d1 = current.DistanceTo(CriticalPoint);
                    var d2 = current.DistanceTo(CriticalPointOpposite);
                    if (d1 < DistanceLimit || d2 < DistanceLimit)
                    {
                        break;
                    }
                }
                i++;
            }
            if (i == IterationsLimit)
            {
                res = RGScene.UndefinedColor;
            }
            else
            {
                res = rg.G >= 0
                    ?
                    (current.C1 < 0 ? RGScene.PositiveDynamicsLeftColor : RGScene.PositiveDynamicsRightColor)
                    :
                    //(current.C1 < 0 ? RGScene.NegativeDynamicsLeftColor : RGScene.NegativeDynamicsRightColor)
                    RGScene.NegativeDynamicsLeftColor
                    ;
            }

            return res;
        }

        public CPoint DirectIterated(CPoint source, CProjection projection, RGProcessor processor)
        {
            var c1MinusC0 = source.C1 - source.C0;
            var c2MunusC1 = source.C2 - source.C1;
            var c0C2MinusC1Sq = source.C0 * source.C2 - source.C1 * source.C1;
            var n1 = processor.OneDivN;

            var c0 = (c1MinusC0 * c1MinusC0 + n1 * c0C2MinusC1Sq);
            var c1 = processor.Lambda * (c1MinusC0 * c2MunusC1 + n1 * c0C2MinusC1Sq);
            var c2 = processor.Lambda2 * (c2MunusC1 * c2MunusC1 + n1 * c0C2MinusC1Sq);

            var res = new CPoint(c0, c1, c2);
            res.Project(projection);
            return res;
        }

        protected override void ApplyMarkup(Bitmap bmp, Graphics gr, RGScene scene, RGProcessor processor)
        {
            base.ApplyMarkup(bmp, gr, scene, processor);
            var markupPen = new Pen(RGScene.MarkupColor);

            //for (var x = -1d; x <= 1d; x += 0.01)
            //{
            //    var eq1 = 1 - x*x - LeftC2Limit*LeftC2Limit;
            //    if (eq1 > 0)
            //    {
            //        var c = new CPoint(eq1, x, LeftC2Limit);
            //        if (c.RG(CProjection.C1C2).G > 0 && x < 0)
            //        {
            //            var pt = new PointF((float) x, (float) LeftC2Limit);
            //            var pt1 = new PointF((float) x + 0.001f, (float) LeftC2Limit);
            //            var p1 = scene.MapToUIFrame(pt);
            //            var p2 = scene.MapToUIFrame(pt1);
            //            gr.DrawLine(markupPen, p1, p2);
            //        }
            //    }
            //}

            //for (var x = -1d; x <= 1d; x += 0.01)
            //{
            //    var eq1 = 1 - x * x - RightC2Limit * RightC2Limit;
            //    if (eq1 > 0)
            //    {
            //        var c = new CPoint(eq1, x, RightC2Limit);
            //        var pt = new PointF((float)x, (float)RightC2Limit);
            //        var pt1 = new PointF((float)x + 0.001f, (float)RightC2Limit);
            //        var p1 = scene.MapToUIFrame(pt);
            //        var p2 = scene.MapToUIFrame(pt1);
            //        gr.DrawLine(markupPen, p1, p2);
            //    }
            //}
        }

        public override void ApplyMarkupDynamics(int step, Bitmap bmp, Graphics gr, RGScene scene, RGProcessor processor)
        {
            var vline = MarkupVPoints[step];
            var vPen = new Pen(RGScene.MarkupVColor);
            foreach (var cpt1 in vline)
            {
                var p1 = scene.MapToUIFrame(new PointF((float)cpt1.C1, (float)cpt1.C2));
                gr.DrawEllipse(vPen, p1.X, p1.Y, 1, 1);
            }
            var hline = MarkupHPoints[step];
            var hPen = new Pen(RGScene.MarkupHColor);
            foreach (var cpt1 in hline)
            {
                var p1 = scene.MapToUIFrame(new PointF((float)cpt1.C1, (float)cpt1.C2));
                gr.DrawEllipse(hPen, p1.X, p1.Y, 1, 1);
            }
        }
    }
}