using System;
using System.Drawing;

namespace RGDynApp
{
    public class DirectRGTransformationC1C2 :
        RGSceneC1C2Transform
    {
        protected bool Initialized;

        protected double LeftC2Limit;

        protected double RightC2Limit;

        protected int IterationsLimit;

        protected void Initialize(RGProcessor processor)
        {
            if (Initialized)
                return;

            const int leftA = 2;
            var leftAB2 = leftA - processor.B0 - 2;
            LeftC2Limit = (leftAB2) / (Math.Sqrt(1 + Math.Pow(leftAB2, 2)));

            const int rightA = -1;
            var rightAB2 = rightA - processor.B0 - 2;
            RightC2Limit = -(rightAB2) / (Math.Sqrt(1 + Math.Pow(rightAB2, 2)));

            IterationsLimit = 100;
            Initialized = true;
        }

        protected override Color GetC1C2Color(Bitmap bmp, CPoint cpt, RGPoint rg, RGScene scene, RGProcessor processor)
        {
            Initialize(processor);
            var i = 0;
            Color res;
            var start = cpt;
            var current = start;
            if (cpt.RG(CProjection.C1C2).G < 0)
            {
                res = RGScene.NegativeColor;
            }
            else
            {
                while (i < IterationsLimit)
                {
                    current = DirectIterated(current, CProjection.C1C2, processor);
                    if (current.C1 < 0 && current.C2 > LeftC2Limit)
                        break;
                    if (current.C1 > 0 && current.C2 > RightC2Limit)
                        break;
                    i++;
                }
                if (i == IterationsLimit)
                {
                    res = RGScene.UndefinedColor;
                }
                else
                {
                    if (start.C1 < 0 && start.C2 > LeftC2Limit)
                    {
                        res = RGScene.NegativeMomentalDynamicsColor;
                    }
                    else if (start.C1 > 0 && start.C2 > RightC2Limit)
                    {
                        res = RGScene.PositiveMomentalDynamicsColor;
                    }
                    else
                    {
                        res = current.C1 < 0 ? RGScene.NegativeDynamicsColor : RGScene.PositiveDynamicsColor;
                    }
                }
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
    }
}