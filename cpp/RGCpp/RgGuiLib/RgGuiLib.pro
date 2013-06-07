#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T13:00:11
#
#-------------------------------------------------

QT       += core gui widgets network

include(../settings.pri)

TARGET = RgGuiLib
TEMPLATE = lib

DEFINES += RGGUILIB_LIBRARY

SOURCES += \
    src/windows/rgmainwindow.cpp

HEADERS += \
    rggui.h \
    i/defines.h \
    i/windows/rgmainwindow.h


LIBS += -L../build/lib \
    -lKernel \
    -lMaps \
    -lRgLib

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

INCLUDEPATH += $$PWD/../Maps
DEPENDPATH += $$PWD/../Maps

INCLUDEPATH += $$PWD/../RgLib
DEPENDPATH += $$PWD/../RgLib
