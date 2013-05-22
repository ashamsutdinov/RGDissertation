#include "./../../i/maps/rgmap.h"

RgMap::RgMap(QObject* parent) :
    IRgMap(parent)
{
}

RgMap::~RgMap()
{
}

void RgMap::initializeInternal()
{
    initializeRgInternal();
}

void RgMap::changeProjectionInternal()
{
    emit projectionChanged(this, projection());
    changeRgProjection();
}
