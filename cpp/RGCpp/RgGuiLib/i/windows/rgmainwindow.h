#ifndef RGMAINWINDOW_H
#define RGMAINWINDOW_H

#include "./../defines.h"

class RGGUILIBSHARED_EXPORT RgMainWindow :
    public QMainWindow
{
    Q_OBJECT

public:
  explicit RgMainWindow(QWidget *parent = 0);
  ~RgMainWindow();
};

#endif // RGMAINWINDOW_H
