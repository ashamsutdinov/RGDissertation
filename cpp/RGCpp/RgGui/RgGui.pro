#-------------------------------------------------
#
# Project created by QtCreator 2013-05-13T00:47:23
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

include(../settings.pri)

TARGET = RgGui
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp

HEADERS  += mainwindow.h

FORMS    += mainwindow.ui

LIBS += -L../build/lib \
         -lRgLib

INCLUDEPATH += $$PWD/../RgLib
DEPENDPATH += $$PWD/../RgLib
