using RenormGroups._Points.Interfaces;

namespace RenormGroups._Sets.Interfaces
{
  public interface ISphericalNeighborhood<T, TD> : INeighborhood<T, TD>
    where T : IPoint<TD>
  {
  }
}