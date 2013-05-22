#ifndef IDISPOSABLE_H
#define IDISPOSABLE_H

#include "./../defines.h"

class KERNELSHARED_EXPORT IDisposable
{
public:
  explicit IDisposable();
public:
  virtual void dispose() = 0;
};

#endif // IDISPOSABLE_H
