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
    lock.cpp

HEADERS += \
    kernel.h\
    kernel_global.h \
    kernellib.h \
    bitmask.h \
    interfacesbuffer.h \
    defines.h \
    lock.h
