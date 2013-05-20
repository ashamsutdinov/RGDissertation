#ifndef MAPS_GLOBAL_H
#define MAPS_GLOBAL_H

#include <QtCore/qglobal.h>

#if defined(MAPS_LIBRARY)
#  define MAPSSHARED_EXPORT Q_DECL_EXPORT
#else
#  define MAPSSHARED_EXPORT Q_DECL_IMPORT
#endif

#endif // MAPS_GLOBAL_H
