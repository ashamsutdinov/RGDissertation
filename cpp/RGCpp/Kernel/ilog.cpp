#include "ilog.h"

ILog::ILog(QObject* parent) :
  IService(parent)
{
}

ILog::~ILog()
{
}


Log::Log(QObject* parent) :
  ILog(parent)
{
}

Log::~Log()
{
}

void Log::initializeInternal()
{
}
