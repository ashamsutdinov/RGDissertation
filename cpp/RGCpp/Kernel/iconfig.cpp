#include "iconfig.h"

IConfig::IConfig(QObject* parent) :
  IService(parent)
{
}

IConfig::~IConfig()
{
}

Config::Config(QObject* parent) :
  IConfig(parent)
{
}

Config::~Config()
{
}

void Config::initializeInternal()
{
}
