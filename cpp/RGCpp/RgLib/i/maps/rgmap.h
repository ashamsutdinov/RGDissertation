#ifndef RGMAP_H
#define RGMAP_H

#include "./../defines.h"
#include "./interfaces/irgmap.h"

class RGLIBSHARED_EXPORT RgMap :
        IRgMap
{
    Q_OBJECT

public:
    explicit RgMap(QObject* parent);
    virtual ~RgMap();

protected:
    virtual void initializeInternal();
    virtual void initializeRgInternal() = 0;
    virtual void changeProjectionInternal();
    virtual void changeRgProjection() = 0;
};

#endif // RGMAP_H
