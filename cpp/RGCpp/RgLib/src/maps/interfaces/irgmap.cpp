#include "irgmap.h"

IRgMap::IRgMap(QObject* parent) :
  Map(parent)
{
}

IRgMap::~IRgMap()
{
}

RgMap::RgMap(QObject* parent) :
  IRgMap(parent)
{
}

RgMap::~RgMap()
{
}

CProjection RgMap::projection() const
{
  return _projection;
}

void RgMap::setProjection(const CProjection proj)
{
  _projection = proj;
  emit projectionChanged(this, proj);
}

void RgMap::initializeInternal()
{
  initializeTransform();
}

QBitmap* RgMap::createBlankSquare(int level, int x, int y)
{
  return NULL;
}

