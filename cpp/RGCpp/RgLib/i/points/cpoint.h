#ifndef CPOINT_H
#define CPOINT_H

#include "./../defines.h"
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
    virtual RGPoint rg() const;
};

#endif // CPOINT_H
