#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T10:42:35
#
#-------------------------------------------------

QT       -= gui

include(../settings.pri)

TARGET = IoC
TEMPLATE = lib

DEFINES += IOC_LIBRARY

SOURCES += ioc.cpp \
    ioccontainer.cpp

HEADERS += \
    ioc.h\
    ioc_global.h \
    ioccontainer.h

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

LIBS += -L../build/lib \
    -lKernel
