#include "localserver.h"

#include <QFile>
#include <QStringList>

/**
 * @brief LocalServer::LocalServer
 *  Constructor
 */
LocalServer::LocalServer()
{

}

/**
 * @brief LocalServer::~LocalServer
 *  Destructor
 */
LocalServer::~LocalServer()
{
  _server->close();
  for(auto c : _clients)
  {
    c->close();
  }
}

/**
 * -----------------------
 * QThread requred methods
 * -----------------------
 */

/**
 * @brief run
 *  Initiate the thread.
 */
void LocalServer::run()
{
  _server = new QLocalServer();
  QObject::connect(_server, SIGNAL(newConnection()), this, SLOT(slotNewConnection()));
  QObject::connect(this, SIGNAL(privateDataReceived(QString)), this, SLOT(slotOnData(QString)));
  QString serverName = QString(LOCAL_SERVER_NAME);
  _server->listen(serverName);
  while(_server->isListening() == false)
  {
    _server->listen(serverName);
    msleep(100);
  }
  exec();
}

/**
 * @brief LocalServer::exec
 *  Keeps the thread alive. Waits for incomming connections
 */
void LocalServer::exec()
{
  while(_server->isListening())
  {
    msleep(100);
    _server->waitForNewConnection(100);
    for(auto c : _clients)
    {
      if(c->waitForReadyRead(100))
      {
        QByteArray data = c->readAll();
        emit privateDataReceived(data);
      }
    }
  }
}

/**
 * -------
 * SLOTS
 * -------
 */

/**
 * @brief LocalServer::slotNewConnection
 *  Executed when a new connection is available
 */
void LocalServer::slotNewConnection()
{
  _clients.push_front(_server->nextPendingConnection());
}

/**
 * @brief LocalServer::slotOnData
 *  Executed when data is received
 * @param data
 */
void LocalServer::slotOnData(QString data)
{
  if(data.contains("CMD:", Qt::CaseInsensitive))
  {
    onCMD(data);
  }
  else
  {
    emit dataReceived(data);
  }
}

/**
 * -------
 * Helper methods
 * -------
 */

void LocalServer::onCMD(QString data)
{
  //  Trim the leading part from the command
  data.replace(0, 4, "");

  QStringList commands;
  commands << "showUp";

  switch(commands.indexOf(data))
  {
    case 0:
      emit showUp();
  }
}
