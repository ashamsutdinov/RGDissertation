#ifndef POINT_H
#define POINT_H

#include "./../defines.h"

class RGLIBSHARED_EXPORT Point
{
private:
  double* _d;
  int     _size;
  double  _norm;

public:
  explicit Point(const double* d, const int size);
  explicit Point(const Point& cpy);
  virtual ~Point();

public:
  double value(int num) const;
  double norm() const;

private:
  void copy(const Point& cpy);
  bool checkInput(const double* d, const int size);
};

#endif // POINT_H
