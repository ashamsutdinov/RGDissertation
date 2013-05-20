#ifndef IRGREVERSETRANSFORMMAP_H
#define IRGREVERSETRANSFORMMAP_H

#include "rglib_global.h"
#include "irgmap.h"

class RGLIBSHARED_EXPORT IRgReverseTransformMap :
        public RgMap
{
    Q_OBJECT

public:
    explicit IRgReverseTransformMap(QObject* parent);
    virtual ~IRgReverseTransformMap();
};

class RGLIBSHARED_EXPORT RgReverseTransformMap :
        public IRgReverseTransformMap
{
    Q_OBJECT

public:
    explicit RgReverseTransformMap(QObject* parent);
    virtual ~RgReverseTransformMap();

protected:
    virtual void initializeTransform();
};

#endif // IRGREVERSETRANSFORMMAP_H
