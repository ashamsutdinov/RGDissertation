TEMPLATE = subdirs

CONFIG += ordered

SUBDIRS += \
    RgLib \
    RgUi

RgUi.depends = RgLib

VERSION = 0.0.1
