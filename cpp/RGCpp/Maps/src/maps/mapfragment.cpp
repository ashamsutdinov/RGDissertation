#include "./../../maps/mapfragment.h"

MapFragment::MapFragment(QBitmap *leftTop, QBitmap *rightTop, QBitmap *leftBottom, QBitmap *rightBottom)
{
  _filledFragments = 0;
  if (leftTop)
  {
    _fragments[0] = leftTop;
    _filledFragments++;
  }
  if (rightTop)
  {
    _fragments[1] = rightTop;
    _filledFragments++;
  }
  if (leftBottom)
  {
    _fragments[2] = leftBottom;
    _filledFragments++;
  }
  if (rightBottom)
  {
    _fragments[3] = rightBottom;
    _filledFragments++;
  }
  int w1 = leftTop->width();
  int h1 = leftTop->height();
  int w = w1;
  int h = h1;
  if (rightTop)
  {
    w += rightTop->width();
  }
  if (leftBottom && rightBottom)
  {
    h += leftBottom->height();
  }
  _combinedBitmap = new QBitmap(w,h);
  QPainter painter(_combinedBitmap);
  if (leftTop)
  {
    QImage iLT = leftTop->toImage();
    painter.drawImage(0,0,iLT);
  }
  if (rightTop)
  {
    QImage iRT = rightTop->toImage();
    painter.drawImage(w1,0,iRT);
  }
  if (leftBottom && rightBottom)
  {
    QImage iLB = leftBottom->toImage();
    painter.drawImage(0,h1,iLB);
    QImage iRB = rightBottom->toImage();
    painter.drawImage(w1,h1,iRB);
  }
  painter.save();
}

int MapFragment::filledFragments() const
{
  return _filledFragments;
}

const QBitmap* MapFragment::leftTop() const
{
  return _fragments[0];
}

const QBitmap* MapFragment::rightTop() const
{
  return _fragments[1];
}

const QBitmap* MapFragment::leftBottom() const
{
  return _fragments[2];
}

const QBitmap* MapFragment::rightBottom() const
{
  return _fragments[3];
}

const QBitmap* MapFragment::combined() const
{
  return _combinedBitmap;
}

void MapFragment::dispose()
{
  delete _combinedBitmap;
  for (auto b : _fragments)
  {
    if (b)
    {
      delete b;
    }
  }
}
