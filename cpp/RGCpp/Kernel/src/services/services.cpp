#include "./../../i/services/services.h"
#include "./../../i/services/config.h"
#include "./../../i/services/log.h"
#include "./../../i/services/factory.h"
#include "./../../i/services/threadpool.h"
#include "./../../i/services/database.h"
#include "./../../i/utils/ioccontainer.h"

IConfig*      Services::_config = NULL;
ILog*         Services::_log = NULL;
IFactory*     Services::_factory = NULL;
IThreadPool*  Services::_threadPool = NULL;
IDatabase*    Services::_database = NULL;

IConfig* Services::config()
{
  if (!_config)
  {
    LOCK();
    if (!_config)
    {
      auto ioc = IoCContainer::instance();
      _config = ioc->resolve<IConfig>();
    }
  }
  return _config;
}

ILog* Services::log()
{
  if (!_log)
  {
    LOCK();
    if (!_log)
    {
      auto ioc = IoCContainer::instance();
      _log = ioc->resolve<ILog>();
    }
  }
  return _log;
}

IFactory* Services::factory()
{
  if (!_factory)
  {
    LOCK();
    if (!_factory)
    {
      auto ioc = IoCContainer::instance();
      _factory = ioc->resolve<IFactory>();
    }
  }
  return _factory;
}

IThreadPool* Services::threadPool()
{
  if (!_threadPool)
  {
    LOCK();
    if (!_threadPool)
    {
      auto ioc = IoCContainer::instance();
      _threadPool = ioc->resolve<IThreadPool>();
    }
  }
  return _threadPool;
}

IDatabase* Services::database()
{
  if (!_database)
  {
    LOCK();
    if (!_database)
    {
      auto ioc = IoCContainer::instance();
      _database = ioc->resolve<IDatabase>();
    }
  }
  return _database;
}