#ifndef RGGUI_DEFINES_H
#define RGGUI_DEFINES_H

#include <typeinfo>
#include <typeindex>

#include <QtCore>
#include <QtGui>
#include <QtWidgets>
#include <QtNetwork>

#include <kernel.h>
#include <maps.h>
#include <rg.h>

#if defined(RGGUILIB_LIBRARY)
#  define RGGUILIBSHARED_EXPORT Q_DECL_EXPORT
#else
#  define RGGUILIBSHARED_EXPORT Q_DECL_IMPORT
#endif

#endif // RGGUI_DEFINES_H
