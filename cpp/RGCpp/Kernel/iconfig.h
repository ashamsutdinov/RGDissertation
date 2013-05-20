#ifndef ICONFIG_H
#define ICONFIG_H

#include <QVariant>
#include <QString>
#include <QSettings>
#include "kernel_global.h"
#include "iservice.h"

class KERNELSHARED_EXPORT IConfig :
    public IService
{
  Q_OBJECT

public:
  explicit IConfig(QObject* parent);
  virtual ~IConfig();

public:
  virtual void set(const QString& key, const QVariant& value) = 0;
  virtual QVariant get(const QString& key, const QVariant& defaultValue = QVariant()) const = 0;

signals:
    void configurationChanged(IConfig* sender, const QString& key, const QVariant& value);
};

class KERNELSHARED_EXPORT Config :
    public IConfig
{
  Q_OBJECT

private:
    QSettings* _settings;

public:
  explicit Config(QObject* parent);
  virtual ~Config();

protected:
  virtual void initializeInternal();

public:
    virtual void set(const QString& key, const QVariant& value);
    virtual QVariant get(const QString& key, const QVariant& defaultValue = QVariant()) const;
};

#endif // ICONFIG_H
