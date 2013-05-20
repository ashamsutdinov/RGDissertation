#ifndef IRGMAP_H
#define IRGMAP_H

#include "rglib_global.h"
#include "projection.h"
#include "maps.h"

class RGLIBSHARED_EXPORT IRgMap :
        public MapBase
{
    Q_OBJECT

public:
    explicit IRgMap(QObject* parent);
    virtual ~IRgMap();

public:
    virtual CProjection projection() const = 0;
    virtual void setProjection(const CProjection proj) = 0;

protected:
    virtual void initializeTransform() = 0;

signals:
    void projectionChanged(IRgMap* instance, CProjection projection);
};

class RGLIBSHARED_EXPORT RgMap :
        public IRgMap
{
    Q_OBJECT

private:
    CProjection _projection;

public:
    explicit RgMap( QObject* parent);
    virtual ~RgMap();

public:
    virtual CProjection projection() const;
    virtual void setProjection(const CProjection proj);

protected:
    virtual QBitmap* createBlank(int level = 0, int x = 0, int y = 0);
    virtual void initializeInternal();
};

#endif // IRGMAP_H
