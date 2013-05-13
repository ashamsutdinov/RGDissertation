#include "iocontainer.h"

IOContainer IOContainer::_container;

IOContainer::IOContainer()
{
}

const IOContainer *IOContainer::instance() const
{
  return &_container;
}
