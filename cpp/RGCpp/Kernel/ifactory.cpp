#include "ifactory.h"

IFactory::IFactory(QObject* parent) :
  IService(parent)
{
}

IFactory::~IFactory()
{
}

Factory::Factory(QObject* parent) :
  IFactory(parent)
{
}

Factory::~Factory()
{
}

void Factory::initializeInternal()
{
}
