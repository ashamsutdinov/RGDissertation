#include "./../../../i/maps/interfaces/irgmap.h"

IRgMap::IRgMap(QObject* parent) :
    ProjectedMap<CProjection, CSPACE_DIM>(parent)
{
}

IRgMap::~IRgMap()
{
}
