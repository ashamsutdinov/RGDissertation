#include "lock.h"

LockBase::LockBase()
{
  _mutex.lock();
}

LockBase::~LockBase()
{
  _mutex.unlock();
}
