#include "kernelapplication.h"
#include "ioccontainer.h"
#include "iconfig.h"
#include "ilog.h"
#include "ifactory.h"
#include "ithreadpool.h"
#include "idatabase.h"

/**
 * @brief SingleApplication::SingleApplication
 *  Constructor. Checks and fires up LocalServer or closes the program
 *  if another instance already exists
 * @param argc
 * @param argv
 */
SingleApplication::SingleApplication(int argc, char *argv[]) :
  QApplication(argc, argv)
{
  _shouldContinue = false; // By default this is not the main process

  _socket = new QLocalSocket();

  // Attempt to connect to the LocalServer
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
    // The attempt was insuccessful, so we continue the program
    _shouldContinue = true;
    _server = new LocalServer();
    _server->start();
    QObject::connect(_server, SIGNAL(showUp()), this, SLOT(slotShowUp()));
  }
}

/**
 * @brief SingleApplication::~SingleApplication
 *  Destructor
 */
SingleApplication::~SingleApplication()
{
  if(_shouldContinue)
  {
    _server->terminate();
  }
}

/**
 * @brief SingleApplication::shouldContinue
 *  Weather the program should be terminated
 * @return bool
 */
bool SingleApplication::shouldContinue()
{
  return _shouldContinue;
}

/**
 * @brief SingleApplication::slotShowUp
 *  Executed when the showUp command is sent to LocalServer
 */
void SingleApplication::slotShowUp()
{
  emit showUp();
}

KernelApplication::KernelApplication(int argc, char* argv[]) :
    SingleApplication(argc, argv)
{
}

KernelApplication::~KernelApplication()
{
}

void KernelApplication::initialize()
{
    auto ioc = IoCContainer::instance();
    ioc->registerService<IConfig,Config>();
    ioc->registerService<ILog,Log>();
    ioc->registerService<IThreadPool,ThreadPool>();
    ioc->registerService<IFactory,Factory>();
    ioc->registerService<IDatabase,Database>();
    initializeInternal();
    ioc->initialize();
}

void KernelApplication::initializeInternal()
{
}
