#ifndef PROJECTEDMAP_H
#define PROJECTEDMAP_H

#include "./../defines.h"
#include "./map.h"

template<typename TProjection, int Dim> class ProjectedMap :
        public Map
{
private:
    TProjection _projection;

public:
    explicit ProjectedMap(QObject* parent) :
        Map(parent)
    {
    }
    virtual ~ProjectedMap()
    {
    }

public:
    TProjection projection() const
    {
        return _projection;
    }
    void setProjection(const TProjection& proj)
    {
        _projection = proj;
        setProjectionInternal();        
    }
    int dimension() const
    {
        return Dim;
    }

protected:
    virtual void setProjectionInternal() = 0;
};

#endif // PROJECTEDMAP_H
