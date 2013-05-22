#-------------------------------------------------
#
# Project created by QtCreator 2013-05-20T12:36:22
#
#-------------------------------------------------

QT       += core gui widgets network

include(../settings.pri)

TARGET = Maps
TEMPLATE = lib

DEFINES += MAPS_LIBRARY

SOURCES += \
    src/maps/interfaces/imap.cpp \
    src/maps/mapfragment.cpp \
    src/maps/interfaces/imaplayer.cpp \
    src/maps/maplayer.cpp \
    src/maps/blankmaplayer.cpp \
    src/maps/map.cpp

HEADERS += \
  maps.h\
  i/maps/interfaces/imap.h \
  i/defines.h \
  i/maps/mapfragment.h \
  i/maps/interfaces/imaplayer.h \
  i/maps/maplayer.h \
  i/maps/blankmaplayer.h \
  i/maps/map.h

LIBS += -L../build/lib \
    -lKernel

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

