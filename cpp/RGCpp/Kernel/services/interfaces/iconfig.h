#ifndef ICONFIG_H
#define ICONFIG_H

#include "./../../defines.h"
#include "./../../services/service.h"

class KERNELSHARED_EXPORT IConfig :
    public Service
{
  Q_OBJECT

public:
  explicit IConfig(QObject* parent);
  virtual ~IConfig();

public:
  virtual void set(const QString& key, const QVariant& value) = 0;
  virtual QVariant get(const QString& key, const QVariant& defaultValue = QVariant()) = 0;

signals:
  void configurationChanged(IConfig* sender, const QString& key, const QVariant& value);
};

#endif // ICONFIG_H
