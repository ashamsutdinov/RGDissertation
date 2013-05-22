#ifndef MAP_H
#define MAP_H

#include "./../defines.h"
#include "./../maps/interfaces/imap.h"

class MAPSSHARED_EXPORT Map :
    public IMap
{
private:
  MapLayerList _layers;

public:
  explicit Map(QObject* parent);
  virtual ~Map();

public:
  virtual QString getPath(const QString& directory, int level, int x, int y, const QString& layer = QString(), const QVariant& layerParams = QVariant());
  virtual const MapLayerList& layers() const;
  virtual void addLayer(IMapLayer *layer);
  virtual void removeLayer(const QString& name);

  virtual void dispose();

private:
  void checkDirectory(const QString& path);
};

#endif // MAP_H
