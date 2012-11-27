using System.Drawing;
using System.Linq;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
  public class SceneDirectionDirectIterationAction : SceneDirectIterationAction
  {
    protected override int GetRedChannel(IterationResult result)
    {
      if (result.Resolution == IterationResolution.Trivial)
      {
        var interestedPoint = result.Track.Last(p => p.DistanceTo(Orientir) >= Config.LimitAccuracy);
        if (interestedPoint == null)
        {
          return Color.FromArgb(MaxChannelValue, MaxChannelValue, 0, 0).ToArgb();
        }
        var clr = OrientationCriteria(interestedPoint, Orientir) ? Config.LeftHandColor : Config.RightHandColor;
        return clr.ToArgb();
      }
      return Color.FromArgb(MaxChannelValue, 0, 0, 0).ToArgb();
    }
  }
}