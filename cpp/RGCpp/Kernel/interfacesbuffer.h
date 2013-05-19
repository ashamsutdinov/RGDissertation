#ifndef INTERFACESBUFFER_H
#define INTERFACESBUFFER_H

#include "kernel.h"

class KERNELSHARED_EXPORT InterfacesBuffer
{
private:
  pinterface* _buffer;
  int _bufSize;
  int _interfacesCnt;

public:
  InterfacesBuffer();
  ~InterfacesBuffer();

public:
  void registerInterface(pinterface pi) volatile;
};

#endif // INTERFACESBUFFER_H
