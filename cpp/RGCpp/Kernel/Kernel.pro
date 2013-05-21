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
    src/utils/ioccontainer.cpp \
    src/services/interfaces/iservice.cpp \
    src/services/interfaces/ithreadpool.cpp \
    src/services/interfaces/ifactory.cpp \
    src/services/interfaces/iconfig.cpp \
    src/services/interfaces/ilog.cpp \
    src/services/interfaces/idatabase.cpp \
    src/interfaces/idisposable.cpp \
    src/application/kernelapplication.cpp \
    src/utils/localserver.cpp \
    src/services/service.cpp \
    src/services/services.cpp \
    src/services/config.cpp \
    src/services/database.cpp \
    src/services/factory.cpp \
    src/services/log.cpp \
    src/services/threadpool.cpp \
    src/application/singleapplication.cpp

HEADERS += \
    kernel.h\
    enums/bitmask.h \
    utils/ioccontainer.h \
    utils/singleton.h \
    services/interfaces/iservice.h \
    services/interfaces/ithreadpool.h \
    services/interfaces/ifactory.h \
    services/interfaces/iconfig.h \
    services/interfaces/ilog.h \
    services/interfaces/idatabase.h \
    interfaces/idisposable.h \
    application/kernelapplication.h \
    utils/localserver.h \
    services/service.h \
    services/services.h \
    services/config.h \
    services/database.h \
    services/factory.h \
    services/log.h \
    services/threadpool.h \
    application/singleapplication.h \
    defines.h

RESOURCES += \
    Resources.qrc
