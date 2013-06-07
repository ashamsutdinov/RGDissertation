#include "./../i/mainwindow.h"

int main(int argc, char *argv[])
{
  RgApplication a(argc, argv);
  if (!a.shouldContinue())
    return 0;

  a.initialize();

  MainWindow w;
  w.show();
  
  return a.exec();
}
