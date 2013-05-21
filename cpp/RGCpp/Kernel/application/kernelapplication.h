#ifndef KERNELAPPLICATION_H
#define KERNELAPPLICATION_H

#include "./../defines.h"
#include "./../application/singleapplication.h"

class KERNELSHARED_EXPORT KernelApplication :
    public SingleApplication
{
  Q_OBJECT

public:
  explicit KernelApplication(int, char *[]);
  virtual ~KernelApplication();

public:
  void initialize();

protected:
  virtual void initializeInternal();
};

#endif // KERNELAPPLICATION_H
