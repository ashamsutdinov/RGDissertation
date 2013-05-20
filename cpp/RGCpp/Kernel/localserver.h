#ifndef LOCALSERVER_H
#define LOCALSERVER_H

#include <QThread>
#include <QVector>
#include <QLocalServer>
#include <QLocalSocket>
#include "kernel_global.h"
#include "defines.h"

class LocalServer :
        public QThread
{
  Q_OBJECT

public:
  explicit LocalServer();
  virtual ~LocalServer();

public:
  void shut();

protected:
  void run();
  void exec();

signals:
  void dataReceived(QString data);
  void privateDataReceived(QString data);
  void showUp();

private slots:
  void slotNewConnection();
  void slotOnData(QString data);

private:
  QLocalServer* _server;
  QVector<QLocalSocket*> _clients;
  void onCMD(QString data);
};

#endif // LOCALSERVER_H
