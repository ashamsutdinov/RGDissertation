using RenormGroups._Points.Interfaces;

namespace RenormGroups._Points
{
  public class RGPointDouble : RGPointBase<double>, IRGPointDouble
  {
    public override double DistanceTo(IPoint<double> pt)
    {
      throw new System.NotImplementedException();
    }
  }
}