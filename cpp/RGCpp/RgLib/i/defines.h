#ifndef RG_DEFINES_H
#define RG_DEFINES_H

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

#define RGSPACE_DIM     2
#define CSPACE_DIM      3
#define COORD_ZERO      0.0

#endif // RG_DEFINES_H
