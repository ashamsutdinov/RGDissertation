#include "mainwindow.h"
#include <QApplication>

#include "kernel.h"
#include "rg.h"

int main(int argc, char *argv[])
{
  QApplication a(argc, argv);
  MainWindow w;
  w.show();

  auto ioc = IoCContainer::instance();
  ioc->registerService<IConfig,Config>();
  ioc->registerService<ILog,Log>();
  ioc->registerService<IThreadPool,ThreadPool>();
  ioc->registerService<IFactory,Factory>();  
  ioc->registerService<IDatabase,Database>();
  ioc->registerService<IRgDirectTransformMap,RgDirectTransformMap>();
  ioc->registerService<IRgReverseTransformMap,RgReverseTransformMap>();

  ioc->initialize();
  
  return a.exec();
}
