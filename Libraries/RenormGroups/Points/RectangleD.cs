using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RenormGroups
{
  public class RectangleD :
    Has2DAccuracy,
    INotifyPropertyChanged
  {
    private IEnumerable<PointD> _allPoints;

    private void ProcessPropertyChanges()
    {
      PropertyChanged += (sender, args) =>
      {
        _allPoints = null;
      };
    }

    private void SetValue(string pname)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(pname));
      }
    }

    private double _x;
    public double X
    {
      get
      {
        return _x;
      }
      set
      {
        _x = value;
        SetValue("X");
      }
    }

    private double _y;
    public double Y
    {
      get
      {
        return _y;
      }
      set
      {
        _y = value;
        SetValue("Y");
      }
    }

    private double _width;
    public double Width
    {
      get
      {
        return _width;
      }
      set
      {
        _width = value;
        SetValue("Width");
      }
    }

    private double _height;
    public double Height
    {
      get
      {
        return _height;
      }
      set
      {
        _height = value;
        SetValue("Height");
      }
    }

    private double _accuracyX;
    public new double AccuracyX
    {
      get
      {
        return _accuracyX;
      }
      set
      {
        _accuracyX = value;
        SetValue("AccuracyX");
      }
    }

    private double _accuracyY;
    public new double AccuracyY
    {
      get
      {
        return _accuracyY;
      }
      set
      {
        _accuracyY = value;
        SetValue("AccuracyY");
      }
    }

    public RectangleD()
    {
      ProcessPropertyChanges();
    }

    public RectangleD(PointD pt1, PointD pt2)
    {
      X = Math.Min(pt1.X, pt2.X);
      Y = Math.Min(pt1.Y, pt2.Y);
      Width = Math.Abs(pt1.X - pt2.X);
      Height = Math.Abs(pt1.Y - pt2.Y);
      ProcessPropertyChanges();
    }

    public IEnumerable<PointD> AllPoints
    {
      get
      {
        if (_allPoints == null)
        {
          if (Math.Abs(AccuracyX - 0) < AccuracyEpsilon || Math.Abs(AccuracyY - 0) < AccuracyEpsilon)
          {
            AccuracyX = Width / DefaultIntervalDivizor;
            AccuracyY = Height / DefaultIntervalDivizor;
          }

          var list = new List<PointD>();

          var xFrom = X;
          var xTo = X + Width;
          var yFrom = Y;
          var yTo = Y + Height;

          for (var x = xFrom; x <= xTo; x += AccuracyX)
          {
            for (var y = yFrom; y <= yTo; y += AccuracyY)
            {
              list.Add(new PointD { X = x, Y = y });
            }
          }

          _allPoints = new ReadOnlyCollection<PointD>(list);
        }
        return _allPoints;
      }
    }

    #region Implementation of INotifyPropertyChanged

    protected virtual void RaisePropertyChanged(string propName)
    {
      var e = PropertyChanged;
      if (e != null)
        e(this, new PropertyChangedEventArgs(propName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
  }
}