#include "./../../i/application/kernelapplication.h"
#include "./../../i/utils/ioccontainer.h"
#include "./../../i/services/config.h"
#include "./../../i/services/log.h"
#include "./../../i/services/factory.h"
#include "./../../i/services/threadpool.h"
#include "./../../i/services/database.h"

KernelApplication::KernelApplication(int argc, char* argv[]) :
  SingleApplication(argc, argv)
{
}

KernelApplication::~KernelApplication()
{
}

void KernelApplication::initialize()
{
  auto ioc = IoCContainer::instance();
  ioc->registerService<IConfig,Config>();
  ioc->registerService<ILog,Log>();
  ioc->registerService<IThreadPool,ThreadPool>();
  ioc->registerService<IFactory,Factory>();
  ioc->registerService<IDatabase,Database>();
  initializeInternal();
  ioc->initialize();
}

void KernelApplication::initializeInternal()
{
}
