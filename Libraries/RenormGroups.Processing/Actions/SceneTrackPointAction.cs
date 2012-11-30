using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
  public abstract class SceneTrackPointAction : SceneIterationAction
  {
    public CPoint InitialPoint { get; set; }

    public int IterationsCount { get; set; }

    protected abstract IEnumerable<CPoint> Track();

    public bool DrawFullPath { get; set; }

    public override void Apply<TStack>(TStack stack)
    {
      var c = Config.PointColor;
      var clr = Color.FromArgb(255, c.R, c.G, c.B);
      var track = Track();
      if (DrawFullPath)
      {
        foreach (var cPoint in track)
        {
          stack.DrawPoint(cPoint, clr);
        }
      }
      else
      {
        var pt = track.Last();
        stack.DrawPoint(pt, clr);
      }
    }

    protected SceneTrackPointAction()
    {
      IsSimple = true;
    }
  }
}