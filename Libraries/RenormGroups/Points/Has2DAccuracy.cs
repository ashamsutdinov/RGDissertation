using System;

namespace RenormGroups
{
  public class Has2DAccuracy : HasAccuracy
  {
    [Obsolete("Accuracy cannot be used in class, derived from Has2DAccuracy")]
    public new double Accuracy { get; set; }

    public double AccuracyX { get; set; }

    public double AccuracyY { get; set; }
  }
}