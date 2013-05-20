#ifndef IOCONTAINER_H
#define IOCONTAINER_H

#include <typeinfo>
#include <typeindex>
#include <QObject>
#include <QMap>
#include "kernel_global.h"
#include "iservice.h"
#include "singleton.h"

class KERNELSHARED_EXPORT IoCContainerImpl :
    public IService
{
  Q_OBJECT

private:
  QMap<QString,IService*> _services;

public:
  explicit IoCContainerImpl(QObject* parent);

public:
  template<typename TInterface, typename TService> IService* registerService()
  {
    LOCK();
    auto tpI = std::type_index(typeid(TInterface));
    auto tpIN = tpI.name();
    auto count = _services.count(tpIN);
    if (!count)
    {
      auto instance = new TService(this);
      auto ptrI = dynamic_cast<TInterface*>(instance);
      auto ptrS = dynamic_cast<IService*>(ptrI);
      emit serviceRegistered(ptrS);
      connect(ptrS, SIGNAL(initialized(IService*)), this, SIGNAL(serviceInitialized(IService*)));
      connect(ptrS, SIGNAL(destroyed(QObject*)), this, SIGNAL(serviceDestroyed(QObject*)));
      ptrS->setObjectName(tpIN);
      _services.insert(tpIN, ptrS);
      return ptrS;
    }
    return resolve<TInterface>();
  }
  template<typename TInterface> TInterface* resolve()
  {
    LOCK();
    auto tpI = std::type_index(typeid(TInterface));
    auto tpIN = tpI.name();
    auto found = _services[tpIN];
    auto ptrI = dynamic_cast<TInterface*>(found);
    return ptrI;
  }
  IService* resolve(const QString& name);

signals:
  void serviceRegistered(IService* instance);
  void serviceInitialized(IService* instance);
  void serviceDestroyed(QObject* instance);

public:
  virtual void initializeInternal();
};

class IoCContainer :
    public Singleton<IoCContainerImpl>
{
};
IoCContainerImpl* Singleton<IoCContainerImpl>::_instance = NULL;

#endif // IOCONTAINER_H
