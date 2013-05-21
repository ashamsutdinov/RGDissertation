#ifndef SINGLEAPPLICATION_H
#define SINGLEAPPLICATION_H

#include "./../defines.h"
#include "./../utils/localserver.h"

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

#endif // SINGLEAPPLICATION_H
