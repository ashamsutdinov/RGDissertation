#include "./../../maps/blankmaplayer.h"

BlankMapLayer::BlankMapLayer(IMap* map) :
  MapLayer(map)
{
}

BlankMapLayer::~BlankMapLayer()
{
}

QBitmap* BlankMapLayer::getSquare(int level, int x, int y)
{
  auto cfg = IoCContainer::instance()->resolve<IConfig>();
  auto dPath = cfg->get(MAPS_BLANK_PATH_KEY, DEFAULT_BLANK_MAPS_PATH).toString();
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

QString BlankMapLayer::name() const
{
  return QString();
}

QVariant BlankMapLayer::params() const
{
  return QVariant();
}
