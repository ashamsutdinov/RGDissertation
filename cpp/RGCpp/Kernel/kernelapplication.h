#ifndef KERNELAPPLICATION_H
#define KERNELAPPLICATION_H

#include <QApplication>
#include "kernel_global.h"
#include "localserver.h"

/**
 * @brief The Application class handles trivial application initialization procedures
 */
class KERNELSHARED_EXPORT SingleApplication :
        public QApplication
{
  Q_OBJECT

public:
  explicit SingleApplication(int, char *[]);
  virtual ~SingleApplication();

public:
  bool shouldContinue();

signals:
  void showUp();

private slots:
  void slotShowUp();

private:
  QLocalSocket* _socket;
  LocalServer* _server;
  bool _shouldContinue;
};

class KERNELSHARED_EXPORT KernelApplication :
        public SingleApplication
{
public:
    explicit KernelApplication(int, char *[]);
    virtual ~KernelApplication();

public:
    void initialize();

protected:
    virtual void initializeInternal();
};

#endif // KERNELAPPLICATION_H
