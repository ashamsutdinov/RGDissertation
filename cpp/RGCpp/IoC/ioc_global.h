#ifndef IOC_GLOBAL_H
#define IOC_GLOBAL_H

#include <QtCore/qglobal.h>

#if defined(IOC_LIBRARY)
#  define IOCSHARED_EXPORT Q_DECL_EXPORT
#else
#  define IOCSHARED_EXPORT Q_DECL_IMPORT
#endif

#endif // IOC_GLOBAL_H
