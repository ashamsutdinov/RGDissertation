using System.Collections.Generic;
using System.Drawing;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
  public abstract class SceneTrackPointAction : SceneIterationAction
  {
    public CPoint InitialPoint { get; set; }

    public int IterationsCount { get; set; }

    protected abstract IEnumerable<CPoint> Track();

    public override void Apply<TStack>(TStack stack)
    {
      var track = Track();
      foreach (var cPoint in track)
      {
        var c = Config.PointColor;
        var clr = Color.FromArgb(255, c.R, c.G, c.B);
        stack.DrawPoint(cPoint, clr);
      }
    }

    protected SceneTrackPointAction()
    {
      IsSimple = true;
    }
  }
}