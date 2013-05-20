#include "imap.h"

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

