#ifndef RGLIB_GLOBAL_H
#define RGLIB_GLOBAL_H

#include <QtCore>
#include <QtGui>
#include <QtWidgets>
#include <QtNetwork>

#include <kernel.h>
#include <maps.h>

#if defined(RGLIB_LIBRARY)
#  define RGLIBSHARED_EXPORT Q_DECL_EXPORT
#else
#  define RGLIBSHARED_EXPORT Q_DECL_IMPORT
#endif

#endif // RGLIB_GLOBAL_H
