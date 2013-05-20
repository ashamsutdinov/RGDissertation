#include <QDebug>
#include "iservice.h"
#include "defines.h"
#include "iconfig.h"
#include "ilog.h"
#include "ifactory.h"
#include "ithreadpool.h"
#include "idatabase.h"
#include "ioccontainer.h"

IConfig*      _Services::_config = NULL;
ILog*         _Services::_log = NULL;
IFactory*     _Services::_factory = NULL;
IThreadPool*  _Services::_threadPool = NULL;
IDatabase*    _Services::_database = NULL;

const IConfig* _Services::config() volatile
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

const ILog* _Services::log() volatile
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

const IFactory* _Services::factory() volatile
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

const IThreadPool* _Services::threadPool() volatile
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

const IDatabase* _Services::database() volatile
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

IService::IService(QObject* parent) :
  QObject(parent)
{
  _initialized = false;
}

IService::~IService()
{
  qDebug() << tr("Service destroyed: ") << objectName();
}

bool IService::isInitialized() const
{
  return _initialized;
}

void IService::initialize()
{
  if (isInitialized())
    return;

  initializeInternal();

  emit initialized(this);
}
