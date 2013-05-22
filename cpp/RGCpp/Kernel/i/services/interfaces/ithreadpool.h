#ifndef ITHREADPOOL_H
#define ITHREADPOOL_H

#include "./../../defines.h"
#include "./../../services/service.h"

class KERNELSHARED_EXPORT IThreadPool :
    public Service
{
  Q_OBJECT

public:
  explicit IThreadPool(QObject* parent);
  virtual ~IThreadPool();
};

#endif // ITHREADPOOL_H
