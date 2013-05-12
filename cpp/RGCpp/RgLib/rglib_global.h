#ifndef RGLIB_GLOBAL_H
#define RGLIB_GLOBAL_H

#include <QtCore/qglobal.h>

#if defined(RGLIB_LIBRARY)
#  define RGLIBSHARED_EXPORT Q_DECL_EXPORT
#else
#  define RGLIBSHARED_EXPORT Q_DECL_IMPORT
#endif

#endif // RGLIB_GLOBAL_H
