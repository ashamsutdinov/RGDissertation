#include "./../../i/utils/ioccontainer.h"

IoCContainerImpl::IoCContainerImpl(QObject *parent) :
  Service(parent)
{
}

void IoCContainerImpl::initializeInternal()
{
  for (auto s : _services)
  {
    s->initialize();
  }
}

IService* IoCContainerImpl::resolve(const QString &name)
{
  LOCK();
  auto found = _services[name];
  return found;
}
