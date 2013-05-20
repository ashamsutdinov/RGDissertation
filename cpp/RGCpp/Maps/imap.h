#ifndef IMAP_H
#define IMAP_H

#include <QBitmap>
#include <QString>
#include "maps_global.h"
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

class MAPSSHARED_EXPORT IMap :
        public IService
{
    Q_OBJECT

public:
    explicit IMap(QObject* parent);
    virtual ~IMap();

public:
    virtual QBitmap* getBlank(int level = 0, int x = 0, int y = 0) = 0;

protected:
    virtual QBitmap* createBlank(int level = 0, int x = 0, int y = 0) = 0;
};

class MAPSSHARED_EXPORT MapBase :
        public IMap
{
public:
    explicit MapBase(QObject* parent);
    virtual ~MapBase();

private:
    QString getPath(const QString& category, const QString& directory, int level, int x, int y, const QString& params = QString());
    void checkDirectory(const QString& path);

public:
    virtual QBitmap* getBlank(int level = 0, int x = 0, int y = 0);
};

#endif // IMAP_H
