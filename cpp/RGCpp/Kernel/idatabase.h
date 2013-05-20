#ifndef IDATABASE_H
#define IDATABASE_H

#include "kernel_global.h"
#include "iservice.h"

class KERNELSHARED_EXPORT IDatabase :
    public IService
{
  Q_OBJECT

public:
  explicit IDatabase(QObject* parent);
  virtual ~IDatabase();
};

class KERNELSHARED_EXPORT Database :
    public IDatabase
{
  Q_OBJECT

public:
  explicit Database(QObject* parent);
  virtual ~Database();

protected:
  void initializeInternal();
};

#endif // IDATABASE_H
