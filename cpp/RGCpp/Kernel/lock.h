#ifndef LOCK_H
#define LOCK_H

#include "kernel.h"
#include <mutex>

using namespace std;

class KERNELSHARED_EXPORT LockBase
{
  std::mutex _mutex;
public:
  LockBase();
  ~LockBase();
};

class KERNELSHARED_EXPORT ReadWriteLock : public LockBase
{
};

#endif // LOCK_H
