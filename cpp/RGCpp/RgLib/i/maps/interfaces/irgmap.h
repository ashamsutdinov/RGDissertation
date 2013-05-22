#ifndef IRGMAP_H
#define IRGMAP_H

#include "./../../defines.h"
#include "./../../enums/projection.h"

class RGLIBSHARED_EXPORT IRgMap :
        public ProjectedMap<CProjection, CSPACE_DIM>
{
    Q_OBJECT

public:
    explicit IRgMap(QObject* parent);
    virtual ~IRgMap();

signals:
    void projectionChanged(IMap* map, const CProjection& proj);
};

#endif // IRGMAP_H
