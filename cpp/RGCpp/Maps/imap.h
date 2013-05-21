#ifndef IMAP_H
#define IMAP_H

#include <QBitmap>
#include <QString>
#include <QList>
#include "maps_global.h"
#include "map_defaults.h"
#include "kernel.h"

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

class IMap;
class MAPSSHARED_EXPORT IMapLayer
{
public:
  explicit IMapLayer();
  virtual ~IMapLayer();

public:
  virtual QBitmap* getSquare(int level = 0, int x = 0, int y = 0) = 0;
  virtual const IMap* map() const = 0;
  virtual QString name() const = 0;
  virtual QVariant params() const = 0;

protected:
  virtual QBitmap* createSquare(int level = 0, int x = 0, int y = 0) = 0;
};

class MAPSSHARED_EXPORT MapLayer :
  public IMapLayer
{
private:
  const IMap* _map;

public:
  explicit MapLayer(const IMap* map);
  virtual ~MapLayer();

public:
  virtual QBitmap* getSquare(int level, int x, int y);
  virtual const IMap* map() const;
};

class MAPSSHARED_EXPORT IMap :
    public IService
{
  Q_OBJECT

public:
  explicit IMap(QObject* parent);
  virtual ~IMap();

public:
  virtual QBitmap* getBlankSquare(int level = 0, int x = 0, int y = 0) = 0;
  virtual QBitmap* getSquare(int level = 0, int x = 0, int y = 0) = 0;
  virtual QString getPath(const QString& directory, int level, int x, int y, const QString& layer = QString(), const QVariant& layerParams = QVariant()) = 0;
  virtual const MapLayerList& layers() const = 0;
  virtual void addLayer(IMapLayer* layer) = 0;
  virtual void removeLayer(const QString& name) = 0;

protected:
  virtual QBitmap* createBlankSquare(int level = 0, int x = 0, int y = 0) = 0;
};

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
  virtual QBitmap* getBlankSquare(int level = 0, int x = 0, int y = 0);
  virtual const MapLayerList& layers() const;
  virtual void addLayer(IMapLayer *layer);
  virtual void removeLayer(const QString& name);

private:  
  void checkDirectory(const QString& path);
};

#endif // IMAP_H
