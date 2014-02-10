using System.Configuration;
using System.Drawing;

namespace RgLib
{
    public class Config : RgSettings
    {
        public static readonly double Acc = 0.000001;

        public static readonly int Count = 100;

        public static readonly RGPoint ReserverInterestedPointRg = new RGPoint { R = 0, G = 0 };

        public static readonly CPoint ReserverInterestedPoint = new CPoint(1, 0, 0);

        public static readonly CPoint DirectInterestedPoint = new CPoint(0, 0, 1);

        public static readonly Pen BlackPen = new Pen(Color.Black);

        public static readonly Pen RedPen = new Pen(Color.Red);

        public static readonly Pen WhitePen = new Pen(Color.White);

        public static readonly Color Fuchsia = Color.Fuchsia;

        public static readonly Pen FuchsiaPen = new Pen(Color.Fuchsia);

        public static readonly Color Red = Color.Red;

        public static readonly Color Blue = Color.Blue;

        public static readonly Color LightBlue = Color.LightBlue;

        public static readonly Color Green = Color.Green;

        public static readonly Color LightGreen = Color.LightGreen;

        public static readonly Color Pink = Color.Pink;

        public static readonly Color Violet = Color.DarkViolet;

        public static readonly Color Yellow = Color.Yellow;

        public static readonly Color White = Color.White;

        public static readonly Color Black = Color.Black;

        public static readonly Color GhostWhite = Color.GhostWhite;

        public static readonly Brush BlackBrush = new SolidBrush(Color.Black);

        public static readonly Color BgColor = Color.IndianRed;

        static Config()
        {
            if (!Built)
            {
                var conf = ConfigurationManager.AppSettings;
                double.TryParse(conf["Alpha"], out Alpha);
                double.TryParse(conf["N"], out N);
                int.TryParse(conf["Count"], out Count);
                double.TryParse(conf["Acc"], out Acc);
                Build(Alpha, N);
            }
        }
    }
}