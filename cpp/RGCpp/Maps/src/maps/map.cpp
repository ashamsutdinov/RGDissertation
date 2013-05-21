#include "./../../maps/map.h"
#include "./../../maps/interfaces/imaplayer.h"

Map::Map(QObject* parent) :
  IMap(parent)
{
}

Map::~Map()
{
  dispose();
}

QString Map::getPath(const QString &directory, int level, int x, int y, const QString &layer, const QVariant& layerParams)
{
  auto cfg = _.config();
  auto dPath = cfg->get(MAPS_PATH_KEY, DEFAULT_MAPS_PATH).toString();
  QString dir = dPath + "/" + directory + "/";
  if (!layer.isNull() && !layer.isEmpty())
  {
    dir += layer + "/";
  }
  dir += level + "/";
  checkDirectory(dir);
  QString filePath = dir + layerParams.toString() + "-" + x + "." + y + MAPS_FILE_EXT;
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

void Map::dispose()
{
  for (auto l : _layers)
  {
    delete l;
  }
}

void Map::addLayer(IMapLayer *layer)
{
  _layers.append(layer);
}

void Map::removeLayer(const QString &name)
{
  IMapLayer* found;
  for (auto l : _layers)
  {
    if (l->name() == name)
    {
      found = l;
      break;
    }
  }
  if (found)
  {
    _layers.removeOne(found);
    delete found;
  }
}
