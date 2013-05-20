#ifndef IRGMAP_H
#define IRGMAP_H

#include "rglib_global.h"
#include "maps.h"

class RGLIBSHARED_EXPORT IRgMap :
        public MapBase
{
    Q_OBJECT

public:
    explicit IRgMap(QObject* parent);
    virtual ~IRgMap();

protected:
    virtual void initializeInternal();
    virtual void initializeTransform() = 0;
};

#endif // IRGMAP_H
