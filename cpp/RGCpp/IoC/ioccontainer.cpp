#include "ioccontainer.h"

IoCContainer IoCContainer::_container;

IoCContainer::IoCContainer()
{
}

const IoCContainer *IoCContainer::instance() const
{
  return &_container;
}
