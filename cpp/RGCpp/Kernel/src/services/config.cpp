#include "./../../services/config.h"

Config::Config(QObject* parent) :
  IConfig(parent)
{
  _settings = new QSettings(this);
}

Config::~Config()
{
}

void Config::initializeInternal()
{
  QCoreApplication::setOrganizationName(SETTINGS_ORG_NAME);
  QCoreApplication::setOrganizationDomain(SETTINGS_ORG_DOMAIN);
  QCoreApplication::setApplicationName(SETTINGS_APP_NAME);
}

void Config::set(const QString& key, const QVariant& value)
{
  _settings->setValue(key, value);
  emit configurationChanged(this, key, value);
}

QVariant Config::get(const QString& key, const QVariant& defaultValue)
{
  auto value = _settings->value(key);
  if (value.isNull())
  {
    set(key, defaultValue);
    value = defaultValue;
  }
  return value;
}
