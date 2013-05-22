#ifndef ISERVICE_H
#define ISERVICE_H

#include "./../../defines.h"

class KERNELSHARED_EXPORT IService :
    public QObject
{
  Q_OBJECT

public:
  explicit IService(QObject* parent);
  virtual ~IService();

public:
  virtual void initialize() = 0;
  virtual bool isInitialized() const = 0;

signals:
  void initialized(IService* instance);
};

#endif // ISERVICE_H
