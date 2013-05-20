#ifndef IDISPOSABLE_H
#define IDISPOSABLE_H

#include "kernel_global.h"

class KERNELSHARED_EXPORT IDisposable
{
public:
  IDisposable();
public:
  virtual void dispose() = 0;
};

#endif // IDISPOSABLE_H
