#-------------------------------------------------
#
# Project created by QtCreator 2013-05-12T16:30:57
#
#-------------------------------------------------

QT       -= gui

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
    bits.h \
    point.h

unix:!symbian {
    maemo5 {
        target.path = /opt/usr/lib
    } else {
        target.path = /usr/lib
    }
    INSTALLS += target
}

include(../settings.pri)
