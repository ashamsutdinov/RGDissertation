#ifndef IOCONTAINER_H
#define IOCONTAINER_H

#include "ioc.h"

class IOCSHARED_EXPORT IoCContainer
{
private:
  static IoCContainer _container;

private:
  IoCContainer();

public:
  const IoCContainer* instance() const;
};

#endif // IOCONTAINER_H
