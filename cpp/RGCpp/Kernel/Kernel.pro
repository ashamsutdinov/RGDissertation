#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T12:57:52
#
#-------------------------------------------------

QT       -= gui

TARGET = Kernel
TEMPLATE = lib

include(../settings.pri)

DEFINES += KERNEL_LIBRARY

SOURCES += kernel.cpp \
    kernellib.cpp

HEADERS += kernel.h\
        kernel_global.h \
    kernellib.h

