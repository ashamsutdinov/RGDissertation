#-------------------------------------------------
#
# Project created by QtCreator 2013-05-12T16:30:57
#
#-------------------------------------------------

QT       -= gui

include(../settings.pri)

TARGET = RgLib
TEMPLATE = lib

DEFINES += RGLIB_LIBRARY

SOURCES += \
    rg.cpp \
    point.cpp

HEADERS +=\
    rglib_global.h \
    rg.h \
    projection.h \    
    point.h

LIBS += -L../build/lib \
    -lIoC \
    -lKernel

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

INCLUDEPATH += $$PWD/../IoC
DEPENDPATH += $$PWD/../IoC
