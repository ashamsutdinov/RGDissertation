using System;
using System.Drawing;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
    public class SceneReverseIterationAction : SceneFillIterationAction
    {
        #region Overrides of SceneFillIterationAction

        protected override CPoint NegativeAlphaOrientir
        {
            get { return NegativeOrientir ?? (NegativeOrientir = new CPoint { C0 = 0, C1 = 0, C2 = 1 }); }
        }

        protected override CPoint PositiveAlphaOrientir
        {
            get { return PositiveOrientir ?? (PositiveOrientir = new CPoint { C0 = 1, C1 = 0, C2 = 0 }); }
        }

        public override Func<CPoint, CPoint> SingleIteration
        {
            get
            {
                return SingleIterationInternal ?? (SingleIterationInternal = point => Iterator.IterateReverse(point, N, Alpha));
            }
            set { SingleIterationInternal = value; }
        }

        public override Func<CPoint, CPoint, bool> OrientationCriteria
        {
            get
            {
                return OrientationCriteriaInternal ?? (OrientationCriteriaInternal = (p1, p2) => p1.C0 < p2.C0);
            }
            set { OrientationCriteriaInternal = value; }
        }

        protected override int GetRedChannel(IterationResult result)
        {
            var speedFactor = Math.Max(0, Config.CriticalIterationsCount - result.TrackSteps);
            var multiplier = (float)MaxChannelValue / Config.CriticalIterationsCount;
            var fRed = speedFactor * multiplier;
            return Color.FromArgb(0, (int)fRed, 0, 0).ToArgb();
        }

        #endregion
    }
}