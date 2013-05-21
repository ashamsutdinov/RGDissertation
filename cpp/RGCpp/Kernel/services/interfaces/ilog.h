#ifndef ILOG_H
#define ILOG_H

#include "./../../defines.h"
#include "./../../services/service.h"

class KERNELSHARED_EXPORT ILog :
    public Service
{
  Q_OBJECT

public:
  explicit ILog(QObject* parent);
  virtual ~ILog();
};

#endif // ILOG_H
