#ifndef RGAPPLICATION_H
#define RGAPPLICATION_H

#include "kernel.h"
#include "rglib_global.h"

class RGLIBSHARED_EXPORT RgApplication :
        public KernelApplication
{
public:
    explicit RgApplication(int args, char* argv[]);
    virtual ~RgApplication();

protected:
    virtual void initializeInternal();
};

#endif // RGAPPLICATION_H
