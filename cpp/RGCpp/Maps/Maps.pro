#-------------------------------------------------
#
# Project created by QtCreator 2013-05-20T12:36:22
#
#-------------------------------------------------

QT       += core gui

include(../settings.pri)

TARGET = Maps
TEMPLATE = lib

DEFINES += MAPS_LIBRARY

SOURCES += \
    imap.cpp

HEADERS += \
  maps.h\
  maps_global.h \
  imap.h

LIBS += -L../build/lib \
    -lKernel

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

