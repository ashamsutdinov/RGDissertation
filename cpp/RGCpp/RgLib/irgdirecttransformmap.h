#ifndef IRGDIRECTTRANSFORMMAP_H
#define IRGDIRECTTRANSFORMMAP_H

#include "rglib_global.h"
#include "irgmap.h"

class RGLIBSHARED_EXPORT IRgDirectTransformMap :
        public RgMap
{
    Q_OBJECT

public:
    explicit IRgDirectTransformMap(QObject* parent);
    virtual ~IRgDirectTransformMap();
};

class RGLIBSHARED_EXPORT RgDirectTransformMap :
        public IRgDirectTransformMap
{
    Q_OBJECT

public:
    explicit RgDirectTransformMap(QObject* parent);
    virtual ~RgDirectTransformMap();

protected:
    virtual void initializeTransform();
};

#endif // IRGDIRECTTRANSFORMMAP_H
