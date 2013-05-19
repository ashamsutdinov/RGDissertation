#ifndef IINTERFACE_H
#define IINTERFACE_H

#include "kernel_global.h"

class KERNELSHARED_EXPORT IInterface
{
public:
  IInterface();
  virtual ~IInterface();
};

class KERNELSHARED_EXPORT IService : public IInterface
{
public:
  IService();
  virtual ~IService();
};

#endif // IINTERFACE_H
