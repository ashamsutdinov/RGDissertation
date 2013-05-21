#include "./../../application/singleapplication.h"

SingleApplication::SingleApplication(int argc, char *argv[]) :
  QApplication(argc, argv)
{
  _shouldContinue = false;

  _socket = new QLocalSocket(); 
  _socket->connectToServer(LOCAL_SERVER_NAME);
  if(_socket->waitForConnected(100))
  {
    _socket->write("CMD:showUp");
    _socket->flush();
    QThread::msleep(100);
    _socket->close();
  }
  else
  {    
    _shouldContinue = true;
    _server = new LocalServer();
    _server->start();
    QObject::connect(_server, SIGNAL(showUp()), this, SLOT(slotShowUp()));
  }
}

SingleApplication::~SingleApplication()
{
  if(_shouldContinue)
  {
    _server->terminate();
  }
}

bool SingleApplication::shouldContinue()
{
  return _shouldContinue;
}

void SingleApplication::slotShowUp()
{
  emit showUp();
}
