using System.Collections.Generic;

namespace RenormGroups.Processing
{
  public class SceneTrackPointDirectAction : SceneTrackPointAction
  {
    protected override IEnumerable<CPoint> Track()
    {
      return Iterator.TrackPointDirect(InitialPoint, IterationsCount, N, Alpha);
    }
  }
}