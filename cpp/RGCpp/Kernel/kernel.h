#ifndef KERNEL_H
#define KERNEL_H

#include "./i/defines.h"
#include "./i/application/singleapplication.h"
#include "./i/application/kernelapplication.h"
#include "./i/enums/bitmask.h"
#include "./i/interfaces/idisposable.h"
#include "./i/services/interfaces/iservice.h"
#include "./i/services/service.h"
#include "./i/services/services.h"
#include "./i/services/interfaces/iconfig.h"
#include "./i/services/config.h"
#include "./i/services/interfaces/idatabase.h"
#include "./i/services/database.h"
#include "./i/services/interfaces/ifactory.h"
#include "./i/services/factory.h"
#include "./i/services/interfaces/ilog.h"
#include "./i/services/log.h"
#include "./i/services/interfaces/ithreadpool.h"
#include "./i/services/threadpool.h"
#include "./i/utils/ioccontainer.h"
#include "./i/utils/localserver.h"
#include "./i/utils/singleton.h"

#endif // KERNEL_H
