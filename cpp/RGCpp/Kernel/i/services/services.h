#ifndef SERVICES_H
#define SERVICES_H

#include "./../defines.h"
#include "./interfaces/iservice.h"

class IConfig;
class ILog;
class IThreadPool;
class IFactory;
class IDatabase;

class KERNELSHARED_EXPORT Services
{
private:
  static IConfig*      _config;
  static ILog*         _log;
  static IThreadPool*  _threadPool;
  static IFactory*     _factory;
  static IDatabase*    _database;

public:
  IConfig*      config();
  ILog*         log();
  IThreadPool*  threadPool();
  IFactory*     factory();
  IDatabase*    database();
};

#endif // SERVICES_H
