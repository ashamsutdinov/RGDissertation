#ifndef CPOINT_H
#define CPOINT_H

#include "./../defines.h"
#include "./../enums/projection.h"
#include "./point.h"

class RGPoint;
class RGLIBSHARED_EXPORT CPoint :
        Point
{
public:
    CPoint(coord c0, coord c1, coord c2);
    CPoint(const CPoint& cpy);
    virtual ~CPoint();

public:
    virtual RGPoint rg(CProjection proj);
};

#endif // CPOINT_H
