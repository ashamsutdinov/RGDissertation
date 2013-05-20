#ifndef ISERVICE_H
#define ISERVICE_H

#include <QObject>
#include <QStringList>
#include "kernel_global.h"

class IConfig;
class ILog;
class IThreadPool;
class IFactory;
class IDatabase;
struct KERNELSHARED_EXPORT _Services
{
private:
  static IConfig*      _config;
  static ILog*         _log;
  static IThreadPool*  _threadPool;
  static IFactory*     _factory;
  static IDatabase*    _database;

public:
  const IConfig*      config() volatile;
  const ILog*         log() volatile;
  const IThreadPool*  threadPool() volatile;
  const IFactory*     factory() volatile;
  const IDatabase*    database() volatile;
};

class KERNELSHARED_EXPORT IService :
    public QObject
{
  Q_OBJECT

private:
  bool      _initialized;

protected:
  _Services _;

public:
  explicit IService(QObject* parent);
  virtual ~IService();

public:
  void initialize();
  bool isInitialized() const;

protected:
  virtual void initializeInternal() = 0;

signals:
  void initialized(IService* instance);
};

#endif // ISERVICE_H
