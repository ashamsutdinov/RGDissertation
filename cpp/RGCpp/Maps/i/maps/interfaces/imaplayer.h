#ifndef IMAPLAYER_H
#define IMAPLAYER_H

#include "./../../defines.h"

class IMap;
class MAPSSHARED_EXPORT IMapLayer
{
public:
  explicit IMapLayer();
  virtual ~IMapLayer();

public:
  virtual QBitmap* getSquare(int level = 0, int x = 0, int y = 0) = 0;
  virtual IMap* map() const = 0;
  virtual QString name() const = 0;
  virtual QVariant params() const = 0;

protected:
  virtual QBitmap* createSquare(int level = 0, int x = 0, int y = 0) = 0;
};

#endif // IMAPLAYER_H
