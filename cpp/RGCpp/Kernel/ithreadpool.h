#ifndef ITHREADPOOL_H
#define ITHREADPOOL_H

#include "kernel_global.h"
#include "iservice.h"

class KERNELSHARED_EXPORT IThreadPool :
    public IService
{
  Q_OBJECT

public:
  explicit IThreadPool(QObject* parent);
  virtual ~IThreadPool();
};

class KERNELSHARED_EXPORT ThreadPool :
    public IThreadPool
{
  Q_OBJECT

public:
  explicit ThreadPool(QObject* parent);
  virtual ~ThreadPool();

protected:
  virtual void initializeInternal();
};

#endif // ITHREADPOOL_H
