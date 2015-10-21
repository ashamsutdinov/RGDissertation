using System;
using System.Drawing;
using System.Linq;

namespace RGDynApp.RG
{
    public abstract class RGSceneC1C2Transform :
        BothRGSceneTransform
    {
        public override Color GetPixelColor(PointF ptF, RGScene scene, RGProcessor processor)
        {
            var cpt = CPoint.New(ptF, CProjection.C1C2);
            if (!cpt.IsNormal) 
                return RGScene.BackgroundColr;

            var rg = cpt.RG(CProjection.C1C2);
            var clr = GetC1C2Color(cpt, rg, scene, processor);
            return clr;
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
            gr.DrawEllipse(markupPen, zeroFixedPointUiCoords.X - 4, zeroFixedPointUiCoords.Y - 4, 8, 8);
            gr.DrawString("0", labelsFont, labelsBrushEx, zeroFixedPointUiCoords.X, zeroFixedPointUiCoords.Y);

            var infinityFixedPoint = new PointF(0, 1);
            var infinityFixedPointUiCoords = scene.MapToUIFrame(infinityFixedPoint);
            gr.DrawEllipse(markupPen, infinityFixedPointUiCoords.X - 4, infinityFixedPointUiCoords.Y - 4, 8, 8);
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
            gr.DrawEllipse(markupPen, singularPoinUiCoords.X - 4, singularPoinUiCoords.Y - 4, 8, 8);
            gr.DrawLine(markupPen, singularPoinUiCoords.X - 4, singularPoinUiCoords.Y - 4, singularPoinUiCoords.X + 4, singularPoinUiCoords.Y + 4);
            gr.DrawLine(markupPen, singularPoinUiCoords.X - 4, singularPoinUiCoords.Y + 4, singularPoinUiCoords.X + 4, singularPoinUiCoords.Y - 4);


            var fixedPointX1 = Math.Sqrt((1 - processor.Lambda * processor.NSqrt) / (processor.NSqrt - processor.Lambda));
            var fixedPointY1 = Math.Sqrt((processor.NSqrt - processor.Lambda) / (1 - processor.Lambda * processor.NSqrt));
            var fixedPointR1 = (processor.NSqrt + fixedPointX1 * fixedPointY1) / (fixedPointX1 * fixedPointX1 - processor.NSqrt);
            var fixedPointG1 = processor.NSqrt * Math.Pow((fixedPointX1 + fixedPointY1) / (fixedPointX1 * fixedPointX1 - processor.NSqrt), 2);
            var fixedPointRG1 = new RGPoint(fixedPointR1, fixedPointG1);
            var fixedPoint1 = fixedPointRG1.C(CProjection.C1C2);
            var fixedPointFrame1 = new PointF((float)fixedPoint1.C1, (float)fixedPoint1.C2);
            var fixedPointUICoords1 = scene.MapToUIFrame(fixedPointFrame1);
            gr.DrawEllipse(markupPen, fixedPointUICoords1.X - 4, fixedPointUICoords1.Y - 4, 8, 8);
            gr.DrawLine(markupPen, fixedPointUICoords1.X - 4, fixedPointUICoords1.Y, fixedPointUICoords1.X + 4, fixedPointUICoords1.Y);
            gr.DrawLine(markupPen, fixedPointUICoords1.X, fixedPointUICoords1.Y - 4, fixedPointUICoords1.X, fixedPointUICoords1.Y + 4);

            var fixedPointX2 = Math.Sqrt((1 + processor.Lambda * processor.NSqrt) / (processor.NSqrt + processor.Lambda));
            var fixedPointY2 =
                -Math.Sqrt((processor.NSqrt + processor.Lambda) / (1 + processor.Lambda * processor.NSqrt));
            var fixedPointR2 = (processor.NSqrt + fixedPointX2 * fixedPointY2) /
                               (fixedPointX2 * fixedPointX2 - processor.NSqrt);
            var fixedPointG2 = processor.NSqrt *
                               Math.Pow(
                                   (fixedPointX2 + fixedPointY2) / (fixedPointX2 * fixedPointX2 - processor.NSqrt), 2);
            var fixedPointRG2 = new RGPoint(fixedPointR2, fixedPointG2);
            var fixedPointC2 = fixedPointRG2.C(CProjection.C1C2);
            var fixedPointFrame2 = new PointF((float)fixedPointC2.C1, (float)fixedPointC2.C2);
            var fixedPointUICoords2 = scene.MapToUIFrame(fixedPointFrame2);
            gr.DrawEllipse(markupPen, fixedPointUICoords2.X - 4, fixedPointUICoords2.Y - 4, 8, 8);
            gr.DrawLine(markupPen, fixedPointUICoords2.X - 4, fixedPointUICoords2.Y, fixedPointUICoords2.X + 4,
                fixedPointUICoords2.Y);

            if (processor.Any())
            {
                var fixPart1 = Math.Sqrt(processor.Lambda) / (1 + processor.Lambda);
                var fixPart2 = (1 + processor.Lambda * processor.Lambda * processor.N) / (processor.Lambda * Math.Sqrt(processor.N));
                var fixPart3 = (processor.N + 1) / Math.Sqrt(processor.N);
                var fixPart4 = Math.Sqrt((1 + processor.Lambda * processor.Lambda * processor.N) / (processor.N + processor.Lambda * processor.Lambda));
                var cycleX1 = fixPart1 * Math.Sqrt(fixPart2 - fixPart3 * fixPart4);
                var cycleY1 = Math.Sqrt((processor.N + processor.Lambda * processor.Lambda) / (1 + processor.Lambda * processor.Lambda * processor.N)) * cycleX1;
                var cycleR1 = (processor.NSqrt + cycleX1 * cycleY1) / (cycleX1 * cycleX1 - processor.NSqrt);
                var cycleG1 = processor.NSqrt * Math.Pow((cycleX1 + cycleY1) / (cycleX1 * cycleX1 - processor.NSqrt), 2);
                var cycleRG1 = new RGPoint(cycleR1, cycleG1);
                var cycleC1 = cycleRG1.C(CProjection.C1C2);
                var cycleCoords1 = scene.MapToUIFrame(new PointF((float)cycleC1.C1, (float)cycleC1.C2));
                gr.DrawEllipse(markupPen, cycleCoords1.X - 3, cycleCoords1.Y - 3, 6, 6);
                gr.DrawEllipse(markupPen, cycleCoords1.X - 4, cycleCoords1.Y - 4, 8, 8);
                gr.DrawEllipse(markupPen, cycleCoords1.X - 5, cycleCoords1.Y - 5, 10, 10);
                var cycleX2 = fixPart1 * Math.Sqrt(fixPart2 + fixPart3 * fixPart4);
                var cycleY2 = -Math.Sqrt((processor.N + processor.Lambda * processor.Lambda) / (1 + processor.Lambda * processor.Lambda * processor.N)) * cycleX2;
                var cycleR2 = (processor.NSqrt + cycleX2 * cycleY2) / (cycleX2 * cycleX2 - processor.NSqrt);
                var cycleG2 = processor.NSqrt * Math.Pow((cycleX2 + cycleY2) / (cycleX2 * cycleX2 - processor.NSqrt), 2);
                var cycleRG2 = new RGPoint(cycleR2, cycleG2);
                var cycleC2 = cycleRG2.C(CProjection.C1C2);
                var cycleCoords2 = scene.MapToUIFrame(new PointF((float)cycleC2.C1, (float)cycleC2.C2));
                gr.DrawEllipse(markupPen, cycleCoords2.X - 3, cycleCoords2.Y - 3, 6, 6);
                gr.DrawEllipse(markupPen, cycleCoords2.X - 4, cycleCoords2.Y - 4, 8, 8);
                gr.DrawEllipse(markupPen, cycleCoords2.X - 5, cycleCoords2.Y - 5, 10, 10);
            }
        }

        public override void ApplyTrackPointDynamics(int step, Bitmap bmp, Graphics gr, Tuple<Color, PointF, CPoint> point, RGScene scene, RGProcessor processor)
        {
            var cpt = point.Item3;
            var clr =
                point.Item1 == RGScene.PositiveDynamicsRightColor ||
                point.Item1 == RGScene.NegativeDynamicsLeftColor
                    ? RGScene.Line2Color
                    : RGScene.Line1Color;
            var pen = new Pen(clr);

            var trans = this as DirectRGTransformationC1C2;
            if (trans == null)
                return;

            var res = cpt;
            for (var i = 0; i < step; i++)
            {
                res = trans.DirectIterated(res, CProjection.C1C2, processor);
            }

            var coordsLeft = scene.MapToUIFrame(new PointF((float)res.C1, (float)res.C2));
            gr.DrawEllipse(pen, coordsLeft.X, coordsLeft.Y, 1, 1);
            gr.DrawEllipse(pen, coordsLeft.X - 3, coordsLeft.Y - 3, 6, 6);
        }

        public override void ApplyBoundaryPointDynamics(int step, Bitmap bmp, Graphics gr, Tuple<Color, PointF, CPoint> left, Tuple<Color, PointF, CPoint> right, RGScene scene, RGProcessor processor)
        {
            var cptLeft = left.Item3;
            var clrLeft =
                left.Item1 == RGScene.PositiveDynamicsRightColor ||
                left.Item1 == RGScene.NegativeDynamicsLeftColor
                    ? RGScene.Line2Color
                    : RGScene.Line1Color;
            var penLeft = new Pen(clrLeft);
            var cptRight = right.Item3;
            var clrRight =
                right.Item1 == RGScene.PositiveDynamicsRightColor ||
                right.Item1 == RGScene.NegativeDynamicsLeftColor
                    ? RGScene.Line2Color
                    : RGScene.Line1Color;
            var penRight = new Pen(clrRight);

            var trans = this as DirectRGTransformationC1C2;
            if (trans == null)
                return;

            var resLeft = cptLeft;
            var resRight = cptRight;
            for (var i = 0; i < step; i++)
            {
                resLeft = trans.DirectIterated(resLeft, CProjection.C1C2, processor);
                resRight = trans.DirectIterated(resRight, CProjection.C1C2, processor);
            }

            var coordsLeft = scene.MapToUIFrame(new PointF((float)resLeft.C1, (float)resLeft.C2));
            gr.DrawEllipse(penLeft, coordsLeft.X, coordsLeft.Y, 1, 1);
            gr.DrawEllipse(penLeft, coordsLeft.X - 3, coordsLeft.Y - 3, 6, 6);

            var coordsRight = scene.MapToUIFrame(new PointF((float)resRight.C1, (float)resRight.C2));
            gr.DrawEllipse(penRight, coordsRight.X, coordsRight.Y, 1, 1);
            gr.DrawEllipse(penRight, coordsRight.X - 3, coordsRight.Y - 3, 6, 6);
        }

        protected abstract Color GetC1C2Color(CPoint cpt, RGPoint rg, RGScene scene, RGProcessor processor);
    }
}