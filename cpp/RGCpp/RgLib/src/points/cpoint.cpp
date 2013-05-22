#include "./../../i/points/cpoint.h"
#include "./../../i/points/rgpoint.h"

CPoint::CPoint(coord c0, coord c1, coord c2)
{
    coord d[] = {c0,c1,c2};
    copy(d,CSPACE_DIM);
}

CPoint::CPoint(const CPoint &cpy) :
    Point(cpy)
{
}

CPoint::~CPoint()
{
}

RGPoint CPoint::rg(CProjection proj)
{
    return RGPoint(COORD_ZERO,COORD_ZERO);
}
