#ifndef IMAP_H
#define IMAP_H

#include "maps_global.h"
#include "iservice.h"

class MAPSSHARED_EXPORT IMap :
        public IService
{
    Q_OBJECT

public:
    explicit IMap(QObject* parent);
    virtual ~IMap();
};

class MAPSSHARED_EXPORT MapBase :
        public IMap
{
public:
    explicit MapBase(QObject* parent);
    virtual ~MapBase();
};

#endif // IMAP_H
