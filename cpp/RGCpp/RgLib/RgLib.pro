#-------------------------------------------------
#
# Project created by QtCreator 2013-05-12T16:30:57
#
#-------------------------------------------------

QT       -= gui

TARGET = RgLib
TEMPLATE = lib

include(../settings.pri)

DEFINES += RGLIB_LIBRARY

SOURCES += \
    rg.cpp \
    point.cpp

HEADERS +=\
        rglib_global.h \
    rg.h \
    projection.h \
    bits.h \
    point.h

LIBS += -L../build/lib \
         -lIoC \
		 -lKernel


INCLUDEPATH += $$PWD/../IoC
DEPENDPATH += $$PWD/../IoC