#include "./../../maps/maplayer.h"

MapLayer::MapLayer(IMap *map) :
  _map(map)
{
}

MapLayer::~MapLayer()
{
}

IMap* MapLayer::map() const
{
  return _map;
}

QBitmap* MapLayer::getSquare(int level, int x, int y)
{
  auto cfg = IoCContainer::instance()->resolve<IConfig>();
  auto dPath = cfg->get(MAPS_LAYERS_PATH_KEY, DEFAULT_LAYERS_MAPS_PATH).toString();
  auto flPath = map()->getPath(dPath, level, x, y, name(), params());
  QFile fl(flPath);
  QBitmap* pImg;
  if (!fl.exists())
  {
    pImg = createSquare(level, x, y);
  }
  else
  {
    pImg = new QBitmap(flPath);
  }
  return pImg;
}
