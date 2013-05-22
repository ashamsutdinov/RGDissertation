#include "irgdirecttransformmap.h"

IRgDirectTransformMap::IRgDirectTransformMap(QObject* parent) :
  RgMap(parent)
{
}

IRgDirectTransformMap::~IRgDirectTransformMap()
{
}

RgDirectTransformMap::RgDirectTransformMap(QObject* parent) :
  IRgDirectTransformMap(parent)
{
}

RgDirectTransformMap::~RgDirectTransformMap()
{
}

void RgDirectTransformMap::initializeTransform()
{
}
