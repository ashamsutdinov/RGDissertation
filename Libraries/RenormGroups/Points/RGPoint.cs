namespace RenormGroups
{
  public class RGPoint : IRGPoint<double>
  {
    #region Implementation of IRGPoint<double>

    public double R { get; set; }

    public double G { get; set; }

    public ICPoint<double> CPoint
    {
      get { return new CPoint(); }
    }

    #endregion
  }
}