#ifndef FACTORY_H
#define FACTORY_H

#include "./../defines.h"
#include "./interfaces/ifactory.h"

class KERNELSHARED_EXPORT Factory :
    public IFactory
{
  Q_OBJECT

public:
  explicit Factory(QObject* parent);
  virtual ~Factory();

protected:
  virtual void initializeInternal();
};


#endif // FACTORY_H
