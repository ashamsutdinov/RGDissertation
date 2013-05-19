#include "interfacesbuffer.h"
#include "defines.h"

InterfacesBuffer::InterfacesBuffer()
{  
  _interfacesCnt = 0;
}

InterfacesBuffer::~InterfacesBuffer()
{
  flush();
}

void InterfacesBuffer::flush()
{
  LOCK();
  for(auto i : _buffer)
  {
    if (i)
    {
      delete i;
    }
  }
  _buffer.clear();
}

void InterfacesBuffer::registerInterface(IInterface*& pi)
{
  LOCK();
  _buffer.append(pi);
}

