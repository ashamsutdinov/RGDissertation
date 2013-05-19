#include "iservice.h"
#include "defines.h"

IService::IService(QObject* parent) :
  QObject(parent)
{
  _initialized = false;
}

IService::~IService()
{
}
