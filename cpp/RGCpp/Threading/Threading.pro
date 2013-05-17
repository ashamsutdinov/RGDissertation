#-------------------------------------------------
#
# Project created by QtCreator 2013-05-17T21:11:35
#
#-------------------------------------------------

QT       -= gui

TARGET = Threading
TEMPLATE = lib

include(../settings.pri)

DEFINES += THREADING_LIBRARY

SOURCES += \
    threadinglib.cpp

HEADERS += threading.h\
        threading_global.h \
    threadinglib.h

