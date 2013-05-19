#ifndef ISERVICE_H
#define ISERVICE_H

#include "kernel_global.h"
#include <QObject>
#include <QStringList>

class KERNELSHARED_EXPORT IService :
    public QObject
{
  Q_OBJECT

private:
  QStringList _dependencies;
  bool  _initialized;

public:
  IService(QObject* parent);
  virtual ~IService();

public:
  const QStringList& dependencies() const;
  void addDependency(const QString& dep);
  void initialize();
  bool initialized() const;
};

#endif // ISERVICE_H
