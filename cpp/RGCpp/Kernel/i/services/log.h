#ifndef LOG_H
#define LOG_H

#include "./../defines.h"
#include "./interfaces/ilog.h"

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

#endif // LOG_H
