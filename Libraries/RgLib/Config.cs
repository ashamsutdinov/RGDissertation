using System.Configuration;
using System.Drawing;

namespace ReverseTransform
{
  public class Config : RGSettings
  {
    public static readonly double Acc;

    public static readonly int Count;

    public static readonly CPoint ReserverInterestedPoint = new CPoint(1, 0, 0);

    public static readonly CPoint DirectInterestedPoint = new CPoint(0, 0, 1);

    public static readonly Pen BlackPen = new Pen(Color.Black);

    public static readonly Pen RedPen = new Pen(Color.Red);

    public static readonly Pen WhitePen = new Pen(Color.White);

    public static readonly Color Red = Color.Red;

    public static readonly Color Blue = Color.Blue;

    public static readonly Color Green = Color.Green;

    public static readonly Color Yellow = Color.Yellow;

    public static readonly Color White = Color.White;

    public static readonly Color Black = Color.Black;

    public static readonly Brush BlackBrush = new SolidBrush(Color.Black);

    public static readonly Color BgColor = Color.IndianRed;

    static Config()
    {
      var conf = ConfigurationManager.AppSettings;
      double.TryParse(conf["Alpha"], out Alpha);
      double.TryParse(conf["N"], out N);
      var parsedCount = int.TryParse(conf["Count"], out Count);
      if (!parsedCount)
        Count = 100;
      var parsedAcc = double.TryParse(conf["Acc"], out Acc);
      if (!parsedAcc)
        Acc = 0.000001d;
      Build(Alpha, N);
    }
  }
}