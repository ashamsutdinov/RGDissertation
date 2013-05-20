#ifndef IMAP_H
#define IMAP_H

#include <QBitmap>
#include "maps_global.h"
#include "iservice.h"

class MAPSSHARED_EXPORT IMap :
        public IService
{
    Q_OBJECT

public:
    explicit IMap(QObject* parent);
    virtual ~IMap();

public:
    virtual QBitmap* createBlank() = 0;
};

class MAPSSHARED_EXPORT MapBase :
        public IMap
{
public:
    explicit MapBase(QObject* parent);
    virtual ~MapBase();
};

#endif // IMAP_H
