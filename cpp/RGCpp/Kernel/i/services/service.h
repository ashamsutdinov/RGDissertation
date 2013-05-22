#ifndef SERVICE_H
#define SERVICE_H

#include "./../defines.h"
#include "./interfaces/iservice.h"
#include "./services.h"

class KERNELSHARED_EXPORT Service :
    public IService
{
  Q_OBJECT

private:
  bool      _initialized;

protected:
  Services  _;

public:
  explicit Service(QObject* parent);
  virtual ~Service();

public:
  virtual void initialize();
  virtual bool isInitialized() const;

protected:
  virtual void initializeInternal() = 0;
};

#endif // SERVICE_H
