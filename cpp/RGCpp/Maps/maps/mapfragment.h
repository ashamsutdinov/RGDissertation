#ifndef MAPFRAGMENT_H
#define MAPFRAGMENT_H

#include "./../defines.h"

class MAPSSHARED_EXPORT MapFragment :
    public IDisposable
{
private:
  QBitmap*    _fragments[4];
  QBitmap*    _combinedBitmap;
  int         _filledFragments;

public:
  explicit MapFragment(QBitmap* leftTop, QBitmap* rightTop, QBitmap* leftBottom, QBitmap* rightBottom);

public:
  int filledFragments() const;
  const QBitmap* leftTop() const;
  const QBitmap* rightTop() const;
  const QBitmap* leftBottom() const;
  const QBitmap* rightBottom() const;
  const QBitmap* combined() const;

public:
  virtual void dispose();
};

#endif // MAPFRAGMENT_H
