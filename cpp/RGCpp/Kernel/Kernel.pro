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
    i/enums/bitmask.h \
    i/utils/ioccontainer.h \
    i/utils/singleton.h \
    i/services/interfaces/iservice.h \
    i/services/interfaces/ithreadpool.h \
    i/services/interfaces/ifactory.h \
    i/services/interfaces/iconfig.h \
    i/services/interfaces/ilog.h \
    i/services/interfaces/idatabase.h \
    i/interfaces/idisposable.h \
    i/application/kernelapplication.h \
    i/utils/localserver.h \
    i/services/service.h \
    i/services/services.h \
    i/services/config.h \
    i/services/database.h \
    i/services/factory.h \
    i/services/log.h \
    i/services/threadpool.h \
    i/application/singleapplication.h \
    i/defines.h

RESOURCES += \
    Resources.qrc
