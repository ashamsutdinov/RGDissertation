#ifndef POINT_H
#define POINT_H

#include "./../defines.h"

class RGLIBSHARED_EXPORT Point :
        public IDisposable
{
private:
  coord*    _d;
  int       _size;
  coord     _norm;

protected:
  explicit Point();

public:
  explicit Point(const coord* d, const int size);
  explicit Point(const Point& cpy);
  virtual ~Point();

public:
  coord v(int num) const;
  int dim() const;
  coord norm();
  coord distanceTo(const Point& pt) const;

  virtual Point& operator = (const Point& cpy);

  virtual void dispose();

protected:
  void copy(const coord* d, const int size);

private:
  bool checkInput(const coord* d, const int size);
};

#endif // POINT_H
