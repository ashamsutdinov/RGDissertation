#ifndef ICONFIG_H
#define ICONFIG_H

#include "kernel_global.h"
#include "iservice.h"

class KERNELSHARED_EXPORT IConfig :
    public IService
{
  Q_OBJECT

public:
  explicit IConfig(QObject* parent);
  virtual ~IConfig();
};

class KERNELSHARED_EXPORT Config :
    public IConfig
{
  Q_OBJECT

public:
  explicit Config(QObject* parent);
  virtual ~Config();

protected:
  virtual void initializeInternal();
};

#endif // ICONFIG_H
