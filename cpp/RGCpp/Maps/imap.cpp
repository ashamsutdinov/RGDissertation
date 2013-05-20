#include <QDir>
#include <QFile>
#include <QPainter>
#include "imap.h"
#include "defines.h"
#include "map_defaults.h"

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

IMap::IMap(QObject* parent) :
    IService(parent)
{
}

IMap::~IMap()
{
}

MapBase::MapBase(QObject* parent) :
    IMap(parent)
{
}

MapBase::~MapBase()
{
}

QBitmap* MapBase::getBlank(int level, int x, int y)
{
    LOCK();
    auto cfg = _.config();
    auto dPath = cfg->get(MAPS_PATH_KEY, DEFAULT_MAPS_PATH).toString();
    auto bPath = cfg->get(MAPS_BLANK_PATH_KEY, DEFAULT_BLANK_MAPS_PATH).toString();
    auto flPath = getPath(dPath, bPath, level, x, y);
    QFile fl(flPath);
    QBitmap* pImg;
    if (!fl.exists())
    {
        pImg = createBlank(level, x, y);
    }
    else
    {
        pImg = new QBitmap(flPath);
    }
    return pImg;
}

QString MapBase::getPath(const QString &category, const QString &directory, int level, int x, int y, const QString &params)
{
    QString dir = category + "/" + directory + "/";
    if (!params.isNull() && !params.isEmpty())
    {
        dir += params + "/";
    }
    dir += level + "/";
    checkDirectory(dir);
    QString filePath = dir + x + "." + y + ".bmp";
    return filePath;
}

void MapBase::checkDirectory(const QString& path)
{
    QDir directory(path);
    if (!directory.exists())
        directory.mkpath(".");
}
