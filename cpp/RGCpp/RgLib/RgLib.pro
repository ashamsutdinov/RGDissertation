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
    rg.cpp \
    point.cpp \
    irgmap.cpp \
    irgdirecttransformmap.cpp \
    irgreversetransformmap.cpp \
    rgapplication.cpp

HEADERS +=\
    rglib_global.h \
    rg.h \
    projection.h \    
    point.h \
    irgmap.h \
    irgdirecttransformmap.h \
    irgreversetransformmap.h \
    rgapplication.h

LIBS += -L../build/lib \
    -lKernel \
    -lMaps

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

INCLUDEPATH += $$PWD/../Maps
DEPENDPATH += $$PWD/../Maps
