#ifndef IOCONTAINER_H
#define IOCONTAINER_H

#include "kernel_global.h"
#include "iinterface.h"
#include "singleton.h"
#include <typeinfo>
#include <typeindex>
#include <QMap>

class KERNELSHARED_EXPORT IoCContainerImpl : public IService
{
private:
  QMap<size_t,IService*> _services;
public:
  virtual ~IoCContainerImpl()
  {
    for (auto e : _services)
    {
      delete e;
    }
  }

public:
  template<typename TInterface, typename TService> bool registerService()
  {
    LOCK();
    auto tpI = std::type_index(typeid(TInterface));
    auto tpIN = tpI.hash_code();
    auto count = _services.count(tpIN);
    if (!count)
    {
      auto instance = new TService();
      auto ptrI = dynamic_cast<TInterface*>(instance);
      auto ptrS = dynamic_cast<IService*>(ptrI);
      _services.insert(tpIN,ptrS);
      return true;
    }
    return false;
  }
  template<typename TInterface> TInterface* resolve()
  {
    LOCK();
    auto tpI = std::type_index(typeid(TInterface));
    auto found = _services[tpI.hash_code()];
    auto ptrI = dynamic_cast<TInterface*>(found);
    return ptrI;
  }
};

class IoCContainer : public Singleton<IoCContainerImpl>
{
};
IoCContainerImpl* Singleton<IoCContainerImpl>::_instance = NULL;

#endif // IOCONTAINER_H
