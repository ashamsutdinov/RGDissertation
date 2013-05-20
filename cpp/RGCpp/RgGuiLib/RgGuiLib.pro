#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T13:00:11
#
#-------------------------------------------------

QT       += core gui widgets

include(../settings.pri)

TARGET = RgGuiLib
TEMPLATE = lib

DEFINES += RGGUILIB_LIBRARY

SOURCES += rgguilib.cpp

HEADERS += \
    rgguilib.h\
    rgguilib_global.h \
    rggui.h


LIBS += -L../build/lib \
    -lKernel \
    -lRgLib

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

INCLUDEPATH += $$PWD/../RgLib
DEPENDPATH += $$PWD/../RgLib
