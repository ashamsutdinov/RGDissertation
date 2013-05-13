#ifndef IOCONTAINER_H
#define IOCONTAINER_H

#include "ioc.h"

class IOCSHARED_EXPORT IOContainer
{
private:
  static IOContainer _container;

private:
  IOContainer();

public:
  const IOContainer* instance() const;
};

#endif // IOCONTAINER_H
