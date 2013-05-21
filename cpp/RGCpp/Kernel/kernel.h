#ifndef KERNEL_H
#define KERNEL_H

#include "./defines.h"
#include "./application/singleapplication.h"
#include "./application/kernelapplication.h"
#include "./enums/bitmask.h"
#include "./interfaces/idisposable.h"
#include "./services/interfaces/iservice.h"
#include "./services/service.h"
#include "./services/services.h"
#include "./services/interfaces/iconfig.h"
#include "./services/config.h"
#include "./services/interfaces/idatabase.h"
#include "./services/database.h"
#include "./services/interfaces/ifactory.h"
#include "./services/factory.h"
#include "./services/interfaces/ilog.h"
#include "./services/log.h"
#include "./services/interfaces/ithreadpool.h"
#include "./services/threadpool.h"
#include "./utils/ioccontainer.h"
#include "./utils/localserver.h"
#include "./utils/singleton.h"

#endif // KERNEL_H
