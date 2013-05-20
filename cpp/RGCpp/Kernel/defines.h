#ifndef DEFINES_H
#define DEFINES_H


#ifndef DEFINES_Q_LOCKS
#define DEFINES_Q_LOCKS

#include <QMutex>
#include <QMutexLocker>
#include <QReadWriteLock>

#define _Q_LOCK()          QMutex __m; QMutexLocker __ml(&__m);
#define _Q_BEGIN_LOCK()    { QMutex __m; __m.lock();
#define _Q_END_LOCK()        __m.unlock(); }

#endif // DEFINES_Q_LOCKS


#ifndef DEFINES_LOCK_MACROS
#define DEFINES_LOCK_MACROS

#define LOCK()        _Q_LOCK()
#define BEGIN_LOCK()  _Q_BEGIN_LOCK()
#define END_LOCK()    _Q_END_LOCK()

#endif // DEFINES_LOCK_MACROS

#ifndef DEFINES_SETTINGS
#define DEFINES_SETTINGS

#define SETTINGS_ORG_NAME   "Aydario"
#define SETTINGS_ORG_DOMAIN "senior-software-developer.com"
#define SETTINGS_APP_NAME   "RgLab"

#endif


#ifndef LOCAL_SERVER_NAME
#define LOCAL_SERVER_NAME "localhost"
#endif


#endif // DEFINES_H
