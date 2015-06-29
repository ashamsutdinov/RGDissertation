using RenormGroups._Points.Interfaces;

namespace RenormGroups._Iterators.Interfaces
{
  /// <summary>
  /// ��������� ��������� ����� � C-�����������
  /// </summary>
  /// <typeparam name="T">��� ������ ���������� ����������</typeparam>
  public interface ICPointIterator<T> : IIterator<_Points.Interfaces.ICPoint<T>>
  {
  }
}