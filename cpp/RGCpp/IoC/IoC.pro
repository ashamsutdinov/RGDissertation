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

SOURCES += \
    ioccontainer.cpp \
    ioclib.cpp

HEADERS += ioc.h\
    ioc_global.h \
    ioccontainer.h \
    ioclib.h

