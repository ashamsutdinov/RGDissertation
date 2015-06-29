using RenormGroups._Points.Interfaces;

namespace RenormGroups._Iterators.Interfaces
{
  /// <summary>
  /// Интерфейс итератора точек в C-координатах
  /// </summary>
  /// <typeparam name="T">Тип данных компоненты координаты</typeparam>
  public interface ICPointIterator<T> : IIterator<_Points.Interfaces.ICPoint<T>>
  {
  }
}