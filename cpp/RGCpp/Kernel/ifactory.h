#ifndef IFACTORY_H
#define IFACTORY_H

#include "kernel_global.h"
#include "iservice.h"

class KERNELSHARED_EXPORT  IFactory :
    public IService
{
  Q_OBJECT

public:
  explicit IFactory(QObject* parent);
  virtual ~IFactory();

public:
    template<typename TObject> TObject* create()
    {
        auto obj = new TObject();
        emit objectCreated(instance);
        return obj;
    }
    template<typename TObject> TObject* create(QObject* parent)
    {
        auto obj = new TObject(parent);
        emit qObjectCreated(instance);
        return obj;
    }

signals:
    void objectCreated(void* instance);
    void qObjectCreated(QObject* instance);
};

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



#endif // IFACTORY_H
