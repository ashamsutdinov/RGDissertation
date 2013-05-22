#ifndef DATABASE_H
#define DATABASE_H

#include "./../defines.h"
#include "./../services/interfaces/idatabase.h"

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

#endif // DATABASE_H
