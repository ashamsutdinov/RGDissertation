#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T12:57:52
#
#-------------------------------------------------

QT       += core gui widgets network

include(../settings.pri)

TARGET = Kernel
TEMPLATE = lib

DEFINES += KERNEL_LIBRARY

SOURCES += \
    ioccontainer.cpp \
    iservice.cpp \
    ithreadpool.cpp \
    ifactory.cpp \
    iconfig.cpp \
    ilog.cpp \
    idatabase.cpp \
    idisposable.cpp \
    kernelapplication.cpp \
    localserver.cpp

HEADERS += \
    kernel.h\
    kernel_global.h \
    bitmask.h \
    ioccontainer.h \
    defines.h \
    singleton.h \
    iservice.h \
    ithreadpool.h \
    ifactory.h \
    iconfig.h \
    ilog.h \
    idatabase.h \
    idisposable.h \
    kernelapplication.h \
    localserver.h

RESOURCES += \
    Resources.qrc
