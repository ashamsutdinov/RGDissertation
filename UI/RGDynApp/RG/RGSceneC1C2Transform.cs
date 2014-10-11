using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RGDynApp
{
    public abstract class RGSceneC1C2Transform :
        BothRGSceneTransform
    {
        protected override Color GetPixelColor(Bitmap bmp, PointF ptF, RGScene scene, RGProcessor processor)
        {
            var cpt = CPoint.New(ptF, CProjection.C1C2);
            if (cpt.IsNormal)
            {
                var rg = cpt.RG(CProjection.C1C2);
                var clr = GetC1C2Color(bmp, cpt, rg, scene, processor);
                return clr;
            }
            return RGScene.BackgroundColr;
        }

        protected override void ApplyMarkup(Bitmap bmp, Graphics gr, RGScene scene, RGProcessor processor)
        {
            var markupPen = new Pen(RGScene.MarkupColor);
            var labelsBrush = new SolidBrush(RGScene.LabelsColor);
            var labelsBrushEx = new SolidBrush(RGScene.MarkupColor);
            var labelsFont = new Font(FontFamily.GenericSansSerif, 12);
            var line1Pen = new Pen(RGScene.Line1Color);
            var line2Pen = new Pen(RGScene.Line2Color);

            var zeroFixedPoint = new PointF(0, 0);
            var zeroFixedPointUiCoords = scene.MapToUIFrame(zeroFixedPoint);
            gr.DrawEllipse(markupPen, zeroFixedPointUiCoords.X - 3, zeroFixedPointUiCoords.Y - 3, 6, 6);
            gr.DrawString("0", labelsFont, labelsBrushEx, zeroFixedPointUiCoords.X, zeroFixedPointUiCoords.Y);

            var infinityFixedPoint = new PointF(0, 1);
            var infinityFixedPointUiCoords = scene.MapToUIFrame(infinityFixedPoint);
            gr.DrawEllipse(markupPen, infinityFixedPointUiCoords.X - 3, infinityFixedPointUiCoords.Y - 3, 6, 6);
            gr.DrawString("δ", labelsFont, labelsBrushEx, infinityFixedPointUiCoords.X, infinityFixedPointUiCoords.Y);

            var x0Pt = new PointF(-1.1f, 0);
            var x1Pt = new PointF(1.1f, 0);
            var sx0Pt = scene.MapToUIFrame(x0Pt);
            var sx1Pt = scene.MapToUIFrame(x1Pt);
            gr.DrawLine(markupPen, sx0Pt, sx1Pt);
            var xlabelPt = new PointF(1.0f, -0.02f);
            var sxLabelPt = scene.MapToUIFrame(xlabelPt);
            gr.DrawString("C1", labelsFont, labelsBrushEx, sxLabelPt);

            var y0Pt = new PointF(0, -1.1f);
            var y1Pt = new PointF(0, 1.1f);
            var sy0Pt = scene.MapToUIFrame(y0Pt);
            var sy1Pt = scene.MapToUIFrame(y1Pt);
            gr.DrawLine(markupPen, sy0Pt, sy1Pt);
            var ylabelPt = new PointF(0.07f, 1.07f);
            var syLabelPt = scene.MapToUIFrame(ylabelPt);
            gr.DrawString("C2", labelsFont, labelsBrushEx, syLabelPt);

            var singularPoint = new CPoint(1, 1, 1);
            var frameSingularPoint = new PointF((float)singularPoint.C1, (float)singularPoint.C2);
            var singularPoinUiCoords = scene.MapToUIFrame(frameSingularPoint);
            gr.DrawEllipse(markupPen, singularPoinUiCoords.X - 3, singularPoinUiCoords.Y - 3, 6, 6);
            gr.DrawLine(markupPen, singularPoinUiCoords.X - 3, singularPoinUiCoords.Y - 3, singularPoinUiCoords.X + 3, singularPoinUiCoords.Y + 3);
            gr.DrawLine(markupPen, singularPoinUiCoords.X - 3, singularPoinUiCoords.Y + 3, singularPoinUiCoords.X + 3, singularPoinUiCoords.Y - 3);
            
            /*
            var plusFixedPoint = new CPoint(1, -processor.RPlus, Math.Pow(processor.RPlus, 2) - processor.GPlus);
            var framePlusFixedPoint = new PointF((float)plusFixedPoint.C1, (float)plusFixedPoint.C2);
            var plusFixedPointUiCoords = scene.MapToUIFrame(framePlusFixedPoint);
            gr.DrawEllipse(markupPen, plusFixedPointUiCoords.X - 3, plusFixedPointUiCoords.Y - 3, 6, 6);
            gr.DrawLine(markupPen, plusFixedPointUiCoords.X - 3, plusFixedPointUiCoords.Y, plusFixedPointUiCoords.X + 3, plusFixedPointUiCoords.Y);
            gr.DrawLine(markupPen, plusFixedPointUiCoords.X, plusFixedPointUiCoords.Y - 3, plusFixedPointUiCoords.X, plusFixedPointUiCoords.Y + 3);

            var minusFixedPoint = new CPoint(1, -processor.RMinus, Math.Pow(processor.RMinus, 2) - processor.GMinus);
            var frameMinusFixedPoint = new PointF((float)minusFixedPoint.C1, (float)minusFixedPoint.C2);
            var minusFixedPointUiCoords = scene.MapToUIFrame(frameMinusFixedPoint);
            gr.DrawEllipse(markupPen, minusFixedPointUiCoords.X - 3, minusFixedPointUiCoords.Y - 3, 6, 6);
            gr.DrawLine(markupPen, minusFixedPointUiCoords.X - 3, minusFixedPointUiCoords.Y, minusFixedPointUiCoords.X + 3, minusFixedPointUiCoords.Y);
            */

            /*
            var b = processor.CurveB;
            var list1 = new List<RGPoint>();
            var list2 = new List<RGPoint>();
            for (var r = -100.0; r < 100; r += 0.01)
            {
                var rg = new RGPoint(r, b * Math.Pow(r + 1, 2));
                list1.Add(rg);
                list2.Add(rg.DirectIterated(processor.N, processor.Lambda, processor.Lambda2));
            }
            var curve1 = list1;
            var cCurve1 = curve1.Select(r => r.C(CProjection.C1C2)).ToList();
            for (var i = 0; i < cCurve1.Count - 1; i++)
            {
                var c1 = cCurve1[i];
                var c2 = cCurve1[i + 1];
                var ui1 = scene.MapToUIFrame(new PointF((float)c1.C1, (float)c1.C2));
                var ui2 = scene.MapToUIFrame(new PointF((float)c2.C1, (float)c2.C2));
                gr.DrawLine(line1Pen, ui1, ui2);
            }
            var curve2 = list2;
            var cCurve2 = curve2.Select(r => r.C(CProjection.C1C2)).ToList();
            for (var i = 0; i < cCurve2.Count - 1; i++)
            {
                var c1 = cCurve2[i];
                var c2 = cCurve2[i + 1];
                var ui1 = scene.MapToUIFrame(new PointF((float)c1.C1, (float)c1.C2));
                var ui2 = scene.MapToUIFrame(new PointF((float)c2.C1, (float)c2.C2));
                gr.DrawLine(line2Pen, ui1, ui2);
            }
            if (b == 1)
            {
                var c1 = cCurve2[0];
                var ui1 = scene.MapToUIFrame(new PointF((float)c1.C1, (float)c1.C2));
                gr.DrawEllipse(line2Pen, ui1.X - 3, ui1.Y - 3, 6, 6);
            }
             * */
        }

        private IEnumerable<RGPoint> Curve(RGProcessor proc, float a, float r0, float r1, float step = 0.0001f)
        {
            for (var r = r0; r <= r1; r += step)
            {
                var g = ((r - a) / (r - proc.B0)) * Math.Pow(r + 1, 2);
                yield return new RGPoint(r, g);
            }
        }

        protected abstract Color GetC1C2Color(Bitmap bmp, CPoint cpt, RGPoint rg, RGScene scene, RGProcessor processor);
    }
}