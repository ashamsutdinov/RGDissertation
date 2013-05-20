#include "irgmap.h"

IRgMap::IRgMap(QObject* parent) :
    MapBase(parent)
{
}

IRgMap::~IRgMap()
{
}

void IRgMap::initializeInternal()
{
    initializeTransform();
}
