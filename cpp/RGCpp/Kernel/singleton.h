#ifndef SINGLETON_H
#define SINGLETON_H

#include "kernel_global.h"
#include "interfacesbuffer.h"
#include "defines.h"

class IInterface;
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
        _instance = new TSingle();
        static InterfacesBuffer buffer;
        auto ptr = dynamic_cast<IInterface*>(_instance);
        buffer.registerInterface(ptr);
      }
    }
    return _instance;
  }
};

#endif // SINGLETON_H
