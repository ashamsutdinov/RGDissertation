#include "./../../application/kernelapplication.h"
#include "./../../utils/ioccontainer.h"
#include "./../../services/config.h"
#include "./../../services/log.h"
#include "./../../services/factory.h"
#include "./../../services/threadpool.h"
#include "./../../services/database.h"

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
