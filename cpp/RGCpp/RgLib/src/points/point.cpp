#include "./../../i/points/point.h"

Point::Point(const double* d, const int size)
{
  if (checkInput(d, size))
  {
    _size = size;
    _norm = -1;
    _d = new double[_size];
    for (int i=0;i<_size;i++)
    {
      _d[i] = d[i];
    }
  }
}

Point::Point(const Point& cpy)
{
  copy(cpy);
}

Point::~Point()
{

}

void Point::copy(const Point &cpy)
{
  Q_UNUSED(cpy);
}

bool Point::checkInput(const double *d, const int size)
{
  if (d && size > 0)
    return true;
  _d = NULL;
  _size = -1;
  return false;
}
