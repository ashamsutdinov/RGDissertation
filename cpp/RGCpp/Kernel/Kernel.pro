#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T12:57:52
#
#-------------------------------------------------

QT       -= gui

include(../settings.pri)

TARGET = Kernel
TEMPLATE = lib

DEFINES += KERNEL_LIBRARY

SOURCES += \
    kernellib.cpp \
    interfacesbuffer.cpp \
    ioccontainer.cpp \
    iinterface.cpp

HEADERS += \
    kernel.h\
    kernel_global.h \
    kernellib.h \
    bitmask.h \
    interfacesbuffer.h \
    ioccontainer.h \
    defines.h \
    singleton.h \
    iinterface.h
