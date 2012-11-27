using RenormGroups._Points.Interfaces;

namespace RenormGroups._Sets.Interfaces
{
  public interface INeighborhood : IIterableSet
  {
  }

  public interface INeighborhood<T, TD> : IIterableSet<T, TD>, INeighborhood
     where T : IPoint<TD>
  {
    T Center { get; set; }

    TD Radius { get; set; }

    TD Accuracy { get; set; }

    void Initialize();
  }
}