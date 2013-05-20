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
    point.cpp \
    irgmap.cpp \
    irgdirecttransformmap.cpp \
    irgreversetransformmap.cpp

HEADERS +=\
    rglib_global.h \
    rg.h \
    projection.h \    
    point.h \
    irgmap.h \
    irgdirecttransformmap.h \
    irgreversetransformmap.h

LIBS += -L../build/lib \
    -lKernel \
    -lMaps

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

INCLUDEPATH += $$PWD/../Maps
DEPENDPATH += $$PWD/../Maps
