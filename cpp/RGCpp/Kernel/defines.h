#ifndef DEFINES_H
#define DEFINES_H


#ifndef DEFINES_Q_LOCKS
#define DEFINES_Q_LOCKS

#include <QMutex>
#include <QMutexLocker>
#include <QReadWriteLock>

#define Q_LOCK()          QMutex __m; QMutexLocker __ml(&__m);
#define Q_BEGIN_LOCK()    { QMutex __m; __m.lock();
#define Q_END_LOCK()        __m.unlock(); }

#endif // DEFINES_Q_LOCKS


#ifndef DEFINES_LOCK_MACROS
#define DEFINES_LOCK_MACROS

#define LOCK()        Q_LOCK()
#define BEGIN_LOCK()  Q_BEGIN_LOCK()
#define END_LOCK()    Q_END_LOCK()

#endif // DEFINES_LOCK_MACROS


#ifndef DEFINES_DYN_ARRAY_MULTIPLIER
#define DEFINES_DYN_ARRAY_MULTIPLIER

#define DYN_ARRAY_INITIAL_SIZE  1
#define DYN_ARRAY_MULTIPLIER    2

#endif // DEFINES_DYN_ARRAY_MULTIPLIER


#endif // DEFINES_H
