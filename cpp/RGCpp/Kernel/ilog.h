#ifndef ILOG_H
#define ILOG_H

#include "kernel_global.h"
#include "iservice.h"

class KERNELSHARED_EXPORT ILog :
    public IService
{
  Q_OBJECT

public:
  explicit ILog(QObject* parent);
  virtual ~ILog();
};

class KERNELSHARED_EXPORT Log :
    public ILog
{
  Q_OBJECT

public:
  explicit Log(QObject* parent);
  virtual ~Log();

protected:
  virtual void initializeInternal();
};

#endif // ILOG_H
