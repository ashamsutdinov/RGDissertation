#ifndef IFACTORY_H
#define IFACTORY_H

#include "./../../defines.h"
#include "./../../services/service.h"

class KERNELSHARED_EXPORT  IFactory :
    public Service
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

#endif // IFACTORY_H
