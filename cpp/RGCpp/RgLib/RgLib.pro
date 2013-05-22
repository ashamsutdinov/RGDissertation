#-------------------------------------------------
#
# Project created by QtCreator 2013-05-12T16:30:57
#
#-------------------------------------------------

QT       += core gui widgets network

include(../settings.pri)

TARGET = RgLib
TEMPLATE = lib

DEFINES += RGLIB_LIBRARY

SOURCES += \
    src/points/point.cpp \
    src/application/rgapplication.cpp \
    src/maps/interfaces/irgmap.cpp \
    src/maps/rgmap.cpp

HEADERS +=\
    rg.h \
    i/enums/projection.h \
    i/points/point.h \
    i/application/rgapplication.h \
    i/defines.h \
    i/maps/interfaces/irgmap.h \
    i/maps/rgmap.h

LIBS += -L../build/lib \
    -lKernel \
    -lMaps

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

INCLUDEPATH += $$PWD/../Maps
DEPENDPATH += $$PWD/../Maps
