#include "mainwindow.h"
#include <QApplication>

#include "kernel.h"

class I : public IService
{

};

class II : public I
{

};

int main(int argc, char *argv[])
{
  QApplication a(argc, argv);
  MainWindow w;
  w.show();

  auto ioc = IoCContainer::instance();
  ioc->registerService<I,II>();
  auto s = ioc->resolve<I>();
  
  return a.exec();
}
