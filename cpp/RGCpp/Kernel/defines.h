#ifndef DEFINES_H
#define DEFINES_H

#include "kernel.h"

typedef void* pinterface;

#define _Lock()      ReadWriteLock lock;
#define _BeginLock() { ReadWriteLock lock;
#define _EndLock()   }

#endif // DEFINES_H
