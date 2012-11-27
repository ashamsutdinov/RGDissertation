using System.Collections.Generic;

namespace RenormGroups.Processing
{
  public sealed class IterationResult
  {
    public CPoint StartPoint { get; set; }

    public CPoint EndPoint { get; set; }

    public IEnumerable<CPoint> Track { get; set; }

    public IterationResolution Resolution { get; set; }

    public int TrackSteps { get; set; }
  }
}