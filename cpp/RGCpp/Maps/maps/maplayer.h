#ifndef MAPLAYER_H
#define MAPLAYER_H

#include "./../defines.h"
#include "./../maps/interfaces/imaplayer.h"
#include "./../maps/map.h"

class MAPSSHARED_EXPORT MapLayer :
  public IMapLayer
{
private:
  IMap* _map;

public:
  explicit MapLayer(IMap* map);
  virtual ~MapLayer();

public:
  virtual QBitmap* getSquare(int level, int x, int y);
  virtual IMap* map() const;
};

#endif // MAPLAYER_H
