#ifndef CONFIG_H
#define CONFIG_H

#include "./../defines.h"
#include "./../services/interfaces/iconfig.h"

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
  virtual QVariant get(const QString& key, const QVariant& defaultValue = QVariant());
};


#endif // CONFIG_H
