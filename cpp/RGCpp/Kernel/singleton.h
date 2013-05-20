#ifndef SINGLETON_H
#define SINGLETON_H

#include <typeinfo>
#include <typeindex>
#include <QCoreApplication>
#include "kernel_global.h"
#include "defines.h"
#include "iservice.h"

template<typename TSingle> class Singleton
{
private:
  static TSingle* _instance;
public:
  static TSingle* instance()
  {
    if (!_instance)
    {
      LOCK();
      if (!_instance)
      {
        auto app = QCoreApplication::instance();
        _instance = new TSingle(app);
        auto tp = std::type_index(typeid(TSingle));
        auto tpN = tp.name();
        _instance->setObjectName(tpN);
      }
    }
    return _instance;
  }
};

#endif // SINGLETON_H
