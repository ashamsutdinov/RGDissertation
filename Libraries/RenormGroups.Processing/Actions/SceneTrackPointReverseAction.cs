using System.Collections.Generic;

namespace RenormGroups.Processing
{
  public class SceneTrackPointReverseAction : SceneTrackPointAction
  {
    protected override IEnumerable<CPoint> Track()
    {
      return Iterator.TrackPointReverse(InitialPoint, IterationsCount, N, Alpha);
    }
  }
}