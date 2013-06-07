#ifndef KERNEL_DEFINES_H
#define KERNEL_DEFINES_H

#include <typeinfo>
#include <typeindex>

#include <QtCore>
#include <QtGui>
#include <QtWidgets>
#include <QtNetwork>

#if defined(KERNEL_LIBRARY)
#  define KERNELSHARED_EXPORT Q_DECL_EXPORT
#else
#  define KERNELSHARED_EXPORT Q_DECL_IMPORT
#endif

#define LOCK()          QMutex __m; QMutexLocker __ml(&__m);
#define BEGIN_LOCK()    { QMutex __m; __m.lock();
#define END_LOCK()        __m.unlock(); }

#define SETTINGS_ORG_NAME   "Aydario"
#define SETTINGS_ORG_DOMAIN "senior-software-developer.com"
#define SETTINGS_APP_NAME   "RgLab"

#define LOCAL_SERVER_NAME "localhost"

#endif // KERNEL_DEFINES_H
