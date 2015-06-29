using System.Collections.Generic;
using RenormGroups._Points.Interfaces;
using RenormGroups._Sets.Interfaces;

namespace RenormGroups._Sets
{
  public abstract class SphericalNeighborhoodBase<T, TD> : ISphericalNeighborhood<T, TD>
    where T : IPoint<TD>
  {
    public IEnumerable<T> Collection { get; protected set; }

    public abstract bool InSet(IPoint point);

    public T Center { get; set; }

    public TD Radius { get; set; }

    public TD Accuracy { get; set; }

    public abstract void Initialize();
  }
}
