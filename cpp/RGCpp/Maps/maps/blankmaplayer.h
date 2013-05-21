#ifndef BLANKMAPLAYER_H
#define BLANKMAPLAYER_H

#include "./../defines.h"
#include "./../maps/maplayer.h"

class MAPSSHARED_EXPORT BlankMapLayer :
    public MapLayer
{
public:
  explicit BlankMapLayer(IMap* map);
  virtual ~BlankMapLayer();

public:
  virtual QBitmap* getSquare(int level, int x, int y);
  virtual QString name() const;
  virtual QVariant params() const;

protected:
  virtual QBitmap* createSquare(int level = 0, int x = 0, int y = 0) = 0;
};

#endif // BLANKMAPLAYER_H
