#ifndef RGPOINT_H
#define RGPOINT_H

#include "./../defines.h"
#include "./point.h"

class CPoint;
class RGLIBSHARED_EXPORT RGPoint :
        Point
{
public:
    RGPoint(coord r, coord g);
    RGPoint(const RGPoint& cpy);
    virtual ~RGPoint();

public:
    virtual CPoint c() const;
};

#endif // RGPOINT_H
