#ifndef INTERFACESBUFFER_H
#define INTERFACESBUFFER_H

#include "kernel_global.h"
#include "iinterface.h"
#include <QList>

class KERNELSHARED_EXPORT InterfacesBuffer
{
private:
  int                _interfacesCnt;
  QList<IInterface*> _buffer;

public:
  InterfacesBuffer();
  ~InterfacesBuffer();

public:
  void registerInterface(IInterface*& pi);

private:
  void flush();
};

#endif // INTERFACESBUFFER_H
