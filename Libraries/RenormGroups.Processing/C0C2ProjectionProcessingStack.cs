using System.Drawing;

namespace RenormGroups.Processing
{
  public sealed class C0C2ProjectionProcessingStack :
    ProcessingStack
  {
    public C0C2ProjectionProcessingStack()
    {
      Projection = CSphereProjection.C0C2;
    }

    #region Overrides of ProcessingStack

    public override void SelectCPointArea(ICPoint<double> point, double epsilon, Color color)
    {
      var xc = point.C0;
      var yc = point.C2;
      SelectCPointAreaBase(xc, yc, epsilon, color);
    }

    public override void SelectCPoint(ICPoint<double> point, Color color)
    {
      if (point.C1 < 0)
        point = point.Diametral;
      var xc = point.C0;
      var yc = point.C2;
      SelectCPointBase(xc, yc, color);
    }

    public override void DrawLine(ICPoint<double> point1, ICPoint<double> point2, Color color)
    {
      if (point1.C1 < 0)
        point1 = point1.Diametral;
      if (point2.C1 < 0)
        point2 = point2.Diametral;
      var x1 = point1.C0;
      var x2 = point2.C0;
      var y1 = point1.C2;
      var y2 = point2.C2;
      DrawLineBase(x1, x2, y1, y2, color);
    }

    #endregion
  }
}