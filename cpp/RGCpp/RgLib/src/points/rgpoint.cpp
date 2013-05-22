#include "./../../i/points/rgpoint.h"
#include "./../../i/points/cpoint.h"

RGPoint::RGPoint(coord r, coord g)
{
    coord d[] = {r,g};
    copy(d,RGSPACE_DIM);
}

RGPoint::RGPoint(const RGPoint &cpy) :
    Point(cpy)
{
}

RGPoint::~RGPoint()
{
}

CPoint RGPoint::c(CProjection proj)
{
    return CPoint(COORD_ZERO,COORD_ZERO,COORD_ZERO);
}
