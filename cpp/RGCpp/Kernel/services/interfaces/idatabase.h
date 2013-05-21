#ifndef IDATABASE_H
#define IDATABASE_H

#include "./../../defines.h"
#include "./../../services/service.h"

class KERNELSHARED_EXPORT IDatabase :
    public Service
{
  Q_OBJECT

public:
  explicit IDatabase(QObject* parent);
  virtual ~IDatabase();
};

#endif // IDATABASE_H
