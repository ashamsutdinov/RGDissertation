#include <QDir>
#include <QFile>
#include <QPainter>
#include "imap.h"
#include "defines.h"
#include "map_defaults.h"

MapFragment::MapFragment(QBitmap *leftTop, QBitmap *rightTop, QBitmap *leftBottom, QBitmap *rightBottom)
{
  _filledFragments = 0;
  if (leftTop)
  {
    _fragments[0] = leftTop;
    _filledFragments++;
  }
  if (rightTop)
  {
    _fragments[1] = rightTop;
    _filledFragments++;
  }
  if (leftBottom)
  {
    _fragments[2] = leftBottom;
    _filledFragments++;
  }
  if (rightBottom)
  {
    _fragments[3] = rightBottom;
    _filledFragments++;
  }
  int w1 = leftTop->width();
  int h1 = leftTop->height();
  int w = w1;
  int h = h1;
  if (rightTop)
  {
    w += rightTop->width();
  }
  if (leftBottom && rightBottom)
  {
    h += leftBottom->height();
  }
  _combinedBitmap = new QBitmap(w,h);
  QPainter painter(_combinedBitmap);
  if (leftTop)
  {
    QImage iLT = leftTop->toImage();
    painter.drawImage(0,0,iLT);
  }
  if (rightTop)
  {
    QImage iRT = rightTop->toImage();
    painter.drawImage(w1,0,iRT);
  }
  if (leftBottom && rightBottom)
  {
    QImage iLB = leftBottom->toImage();
    painter.drawImage(0,h1,iLB);
    QImage iRB = rightBottom->toImage();
    painter.drawImage(w1,h1,iRB);
  }
  painter.save();
}

int MapFragment::filledFragments() const
{
  return _filledFragments;
}

const QBitmap* MapFragment::leftTop() const
{
  return _fragments[0];
}

const QBitmap* MapFragment::rightTop() const
{
  return _fragments[1];
}

const QBitmap* MapFragment::leftBottom() const
{
  return _fragments[2];
}

const QBitmap* MapFragment::rightBottom() const
{
  return _fragments[3];
}

const QBitmap* MapFragment::combined() const
{
  return _combinedBitmap;
}

void MapFragment::dispose()
{
  delete _combinedBitmap;
  for (auto b : _fragments)
  {
    if (b)
    {
      delete b;
    }
  }
}

IMapLayer::IMapLayer()
{
}

IMapLayer::~IMapLayer()
{
}

MapLayer::MapLayer(const IMap *map) :
  _map(map)
{
}

MapLayer::~MapLayer()
{
}

const IMap* MapLayer::map() const
{
  return _map;
}

QBitmap* MapLayer::getSquare(int level, int x, int y)
{
  auto cfg = _.config();
  auto dPath = cfg->get(MAPS_LAYERS_PATH_KEY, DEFAULT_LAYERS_MAPS_PATH).toString();
  auto flPath = _map->getPath(dPath, level, x, y, name(), params());
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

IMap::IMap(QObject* parent) :
  IService(parent)
{
}

IMap::~IMap()
{
}

Map::Map(QObject* parent) :
  IMap(parent)
{
}

Map::~Map()
{
  for (auto l : _layers)
  {
    delete l;
  }
}

QBitmap* Map::getBlankSquare(int level, int x, int y)
{
  LOCK();
  auto cfg = _.config();
  auto bPath = cfg->get(MAPS_BLANK_PATH_KEY, DEFAULT_BLANK_MAPS_PATH).toString();
  auto flPath = getPath(bPath, level, x, y);
  QFile fl(flPath);
  QBitmap* pImg;
  if (!fl.exists())
  {
    pImg = createBlankSquare(level, x, y);
  }
  else
  {
    pImg = new QBitmap(flPath);
  }
  return pImg;
}

QString Map::getPath(const QString &directory, int level, int x, int y, const QString &layer, const QVariant& layerParams)
{
  auto dPath = cfg->get(MAPS_PATH_KEY, DEFAULT_MAPS_PATH).toString();
  QString dir = dPath + "/" + directory + "/";
  if (!layer.isNull() && !layer.isEmpty())
  {
    dir += layer + "/";
  }
  dir += level + "/";
  checkDirectory(dir);
  QString filePath = dir + layerParams + "-" + x + "." + y + MAPS_FILE_EXT;
  return filePath;
}

const MapLayerList& Map::layers() const
{
  return _layers;
}

void Map::checkDirectory(const QString& path)
{
  QDir directory(path);
  if (!directory.exists())
    directory.mkpath(".");
}
