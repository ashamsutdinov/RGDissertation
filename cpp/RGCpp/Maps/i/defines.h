#ifndef MAPS_DEFAULTS_H
#define MAPS_DEFAULTS_H

#include <QtCore>
#include <QtGui>
#include <QtWidgets>
#include <QtNetwork>

#include <kernel.h>

#if defined(MAPS_LIBRARY)
#  define MAPSSHARED_EXPORT Q_DECL_EXPORT
#else
#  define MAPSSHARED_EXPORT Q_DECL_IMPORT
#endif

#define MAPS_PATH_KEY             "/maps/path"
#define MAPS_BLANK_PATH_KEY       "/maps/path/blank"
#define MAPS_LAYERS_PATH_KEY      "/maps/path/layers"
#define DEFAULT_MAPS_PATH         "./map"
#define DEFAULT_BLANK_MAPS_PATH   "/blank"
#define DEFAULT_LAYERS_MAPS_PATH  "/layers"
#define MAPS_WORLD_X1             -1
#define MAPS_WORLD_Y1             -1
#define MAPS_WORLD_W              2
#define MAPS_WORLD_H              2
#define MAPS_DIVISOR              10
#define MAPS_FILE_EXT             ".png"

typedef double coord;

class IMapLayer;
typedef QList<IMapLayer*> MapLayerList;

#endif // MAPS_DEFAULTS_H
