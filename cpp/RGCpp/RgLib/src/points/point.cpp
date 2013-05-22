#include "./../../i/points/point.h"

Point::Point()
{
    copy(NULL, COORD_ZERO);
}

Point::Point(const coord* d, const int size) :
    _d(NULL),
    _size(0)
{
    copy(d, size);
}

Point::~Point()
{
    dispose();
}

Point::Point(const Point& cpy)
{
  copy(cpy._d, cpy._size);
}

void Point::dispose()
{
    delete _d;
}

void Point::copy(const coord* d, const int size)
{
    dispose();
    if (checkInput(d, size))
    {
      _size = size;
      _norm = -1;
      _d = new double[_size];
      memcpy(_d, d, size * sizeof(double));
    }
}

bool Point::checkInput(const coord *d, const int size)
{
  if (d && size > 0)
    return true;
  _d = NULL;
  _size = -1;
  return false;
}

coord Point::v(int num) const
{
    return _d[num];
}

int Point::dim() const
{
    return _size;
}

coord Point::norm()
{
    coord n = 0;
    for (int i=0;i<_size;i++)
    {
        auto v = _d[i];
        n += v;
    }
    n = sqrt(n);
    return n;
}

coord Point::distanceTo(const Point &pt) const
{
    coord n = 0;
    for (auto i = 0; i < _size; i++)
    {
        auto c = _d[i] - pt._d[i];
        c *= c;
        n += c;
    }
    n = sqrt(n);
    return n;
}

Point& Point::operator = (const Point& pt)
{
    if (this == &pt)
    {
        return *this;
    }
    copy(pt._d, pt._size);
    return *this;
}
