#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "./../defines.h"

namespace Ui {
  class MainWindow;
}

class MainWindow :
        public RgMainWindow
{
  Q_OBJECT
  
public:
  explicit MainWindow(QWidget *parent = 0);
  ~MainWindow();
  
private:
  Ui::MainWindow *ui;
};

#endif // MAINWINDOW_H