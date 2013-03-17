using System;
using System.Configuration;
using System.Drawing;

namespace ReverseTransform
{
  public static class Config
  {
    public static double Alpha;

    public static double N;

    public static double Acc;

    public static int Count;

    public static readonly CPoint P1 = new CPoint(1, 0, 0);

    public static readonly Pen BlackPen = new Pen(Color.Black);

    public static readonly Pen RedPen = new Pen(Color.Red);

    public static readonly Pen WhitePen = new Pen(Color.White);

    public static readonly Color Red = Color.Red;

    public static readonly Color Blue = Color.Blue;

    public static readonly Color Green = Color.Green;

    public static readonly Color Yellow = Color.Yellow;

    public static readonly Color White = Color.White;

    public static readonly Color Black = Color.Black;

    static Config()
    {
      var conf = ConfigurationManager.AppSettings;
      Alpha = double.Parse(conf["Alpha"]);
      N = double.Parse(conf["N"]);
      Count = int.Parse(conf["Count"]);
      Acc = double.Parse(conf["Acc"]);
      CPoint.Lambda = Math.Pow(N, Alpha - 1);
      CPoint.LambdaMinus1 = 1 / CPoint.Lambda;
      CPoint.LambdaMinus2 = CPoint.LambdaMinus1 / CPoint.Lambda;
      CPoint.NLambdaMinus2 = N * CPoint.LambdaMinus2;
    }
  }
}