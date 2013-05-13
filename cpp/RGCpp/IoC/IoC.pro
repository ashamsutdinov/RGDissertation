#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T10:42:35
#
#-------------------------------------------------

QT       -= gui

TARGET = IoC
TEMPLATE = lib

include(../settings.pri)

DEFINES += IOC_LIBRARY

SOURCES += ioc.cpp \
    iocontainer.cpp

HEADERS += ioc.h\
        ioc_global.h \
    iocontainer.h

unix:!symbian {
    maemo5 {
        target.path = /opt/usr/lib
    } else {
        target.path = /usr/lib
    }
    INSTALLS += target
}
