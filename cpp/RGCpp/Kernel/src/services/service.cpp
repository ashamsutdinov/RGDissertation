#include "./../../services/service.h"

Service::Service(QObject* parent) :
  IService(parent)
{
  _initialized = false;
}

Service::~Service()
{
  qDebug() << tr("Service destroyed: ") + objectName();
}

bool Service::isInitialized() const
{
  return _initialized;
}

void Service::initialize()
{
  if (isInitialized())
    return;

  initializeInternal();

  _initialized = true;

  emit initialized(this);
}

