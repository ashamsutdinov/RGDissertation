using System;
using System.Collections.Generic;
using RenormGroups._Points.Interfaces;
using RenormGroups._Sets.Interfaces;

namespace RenormGroups._Iterators.Interfaces
{
  /// <summary>
  /// Интерфейс "Итератор". Класс, раскрывающий данный интерфейс, 
  /// позволяет проводить итерации в динамических системах
  /// </summary>
  public interface IIterator
  {
  }

  public interface IIterator<T> : IIterator
    where T : IPoint
  {
    T IterateDirect(T x, int n = 1);
    T IterateReverse(T x, int n = 1);
    IEnumerable<T> IterateDirect(IEnumerable<T> arr, int n = 1);
    IEnumerable<T> IterateReverse(IEnumerable<T> arr, int n = 1);
    IEnumerable<T> IterateDirect(IIterableSet<T> set, int n = 1);
    IEnumerable<T> IterateReverse(IIterableSet<T> set, int n = 1);
    Func<T, T> DirectIteration { get; }
    Func<T, T> ReverseIteration { get; }
  }
}