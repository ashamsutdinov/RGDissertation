#ifndef KERNEL_H
#define KERNEL_H

#include "Kernel/defines.h"
#include "Kernel/application/singleapplication.h"
#include "Kernel/application/kernelapplication.h"
#include "Kernel/enums/bitmask.h"
#include "Kernel/interfaces/idisposable.h"
#include "Kernel/services/interfaces/iservice.h"
#include "Kernel/services/service.h"
#include "Kernel/services/services.h"
#include "Kernel/services/interfaces/iconfig.h"
#include "Kernel/services/config.h"
#include "Kernel/services/interfaces/idatabase.h"
#include "Kernel/services/database.h"
#include "Kernel/services/interfaces/ifactory.h"
#include "Kernel/services/factory.h"
#include "Kernel/services/interfaces/ilog.h"
#include "Kernel/services/log.h"
#include "Kernel/services/interfaces/ithreadpool.h"
#include "Kernel/services/threadpool.h"
#include "Kernel/utils/ioccontainer.h"
#include "Kernel/utils/localserver.h"
#include "Kernel/utils/singleton.h"

#endif // KERNEL_H
