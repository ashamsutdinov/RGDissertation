#ifndef THREADPOOL_H
#define THREADPOOL_H

#include "./../defines.h"
#include "./../services/interfaces/ithreadpool.h"

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

#endif // THREADPOOL_H
