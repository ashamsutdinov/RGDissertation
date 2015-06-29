using System.ComponentModel;

namespace RenormGroups.Processing
{
  public abstract class NotifyPropertyChanged : INotifyPropertyChanged
  {
    #region Implementation of INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    #endregion
  }
}