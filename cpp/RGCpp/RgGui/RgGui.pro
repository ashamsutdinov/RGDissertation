#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T00:47:23
#
#-------------------------------------------------

QT       += core gui widgets

include(../settings.pri)

TARGET = RgGui
TEMPLATE = app

SOURCES += \
    main.cpp\
    mainwindow.cpp

HEADERS  += \
    mainwindow.h

FORMS    += \
    mainwindow.ui

LIBS += -L../build/lib \
    -lKernel \
    -lRgLib \
    -lRgGuiLib

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

INCLUDEPATH += $$PWD/../RgLib
DEPENDPATH += $$PWD/../RgLib

INCLUDEPATH += $$PWD/../RgGuiLib
DEPENDPATH += $$PWD/../RgGuiLib
