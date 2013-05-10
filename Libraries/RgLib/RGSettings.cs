using System;

namespace ReverseTransform
{
  public class RGSettings
  {
    public static double Alpha;

    public static double N;

    public static double Lambda;

    public static double LambdaMinus1;

    public static double LambdaMinus2;

    public static double NLambdaMinus2;

    public static double Lambda2;

    public static double OneDivN;

    public static void Build(double alpha, double n)
    {
      Alpha = alpha;
      N = n;
      Lambda = Math.Pow(N, Alpha - 1);
      LambdaMinus1 = Math.Pow(Lambda, -1);
      LambdaMinus2 = Math.Pow(Lambda, -2);
      NLambdaMinus2 = N * LambdaMinus2;
      Lambda2 = Lambda * Lambda;
      OneDivN = 1 / N;
    }
  }
}