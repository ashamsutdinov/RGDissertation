using System.Drawing;
using System.Linq;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
    public class SceneDirectionReverseIterationAction : SceneReverseIterationAction
    {
        protected override int GetRedChannel(IterationResult result)
        {
            if (result.Resolution == IterationResolution.Trivial)
            {
                var orient = Orientir(Alpha);
                var interestedPoint = result.Track.Last(p => p.DistanceTo(orient) >= Config.LimitAccuracy);
                if (interestedPoint == null)
                {
                    return Color.FromArgb(MaxChannelValue, MaxChannelValue, 0, 0).ToArgb();
                }
                var clr = OrientationCriteria(interestedPoint, orient) ? Config.LeftHandColor : Config.RightHandColor;
                return clr.ToArgb();
            }
            return Color.FromArgb(MaxChannelValue, 0, 0, 0).ToArgb();
        }
    }
}