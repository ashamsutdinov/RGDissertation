#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T00:47:23
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = RgGui
TEMPLATE = app

include(../settings.pri)

SOURCES += main.cpp\
        mainwindow.cpp

HEADERS  += mainwindow.h

FORMS    += mainwindow.ui

LIBS += -L../build/lib \
         -lKernel \
         -lRgLib \
         -lRgGuiLib
         -lIoC

INCLUDEPATH += $$PWD/../Kernel
DEPENDPATH += $$PWD/../Kernel

INCLUDEPATH += $$PWD/../IoC
DEPENDPATH += $$PWD/../IoC


INCLUDEPATH += $$PWD/../RgLib
DEPENDPATH += $$PWD/../RgLib
