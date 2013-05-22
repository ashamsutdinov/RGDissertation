#ifndef IMAP_H
#define IMAP_H

#include "./../../defines.h"

class MAPSSHARED_EXPORT IMap :
    public Service,
    public IDisposable
{
  Q_OBJECT

public:
  explicit IMap(QObject* parent);
  virtual ~IMap();

public:  
  virtual QString getPath(const QString& directory, int level, int x, int y, const QString& layer = QString(), const QVariant& layerParams = QVariant()) = 0;
  virtual const MapLayerList& layers() const = 0;
  virtual void addLayer(IMapLayer* layer) = 0;
  virtual void removeLayer(const QString& name) = 0;

protected:
  virtual QBitmap* createBlankSquare(int level = 0, int x = 0, int y = 0) = 0;
};

#endif // IMAP_H
