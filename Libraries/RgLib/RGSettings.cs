using System;
using System.Configuration;

namespace RgLib
{
    public class RgSettings
    {
        public static double Alpha;

        public static double N;

        public static double NMinus1;

        public static double Lambda;

        public static double LambdaMinus1;

        public static double LambdaMinus2;

        public static double NLambdaMinus2;

        public static double Lambda2;

        public static double OneDivN;

        public static bool Built;

        public static double X1;

        public static double Y1;

        public static double W;

        public static double H;

        public static double Const;

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
            NMinus1 = Math.Pow(N, -1);
            Built = true;
        }

        static RgSettings()
        {
            X1 = -1;
            Y1 = -1;
            W = 2;
            H = 2;
            try
            {
                var conf = ConfigurationManager.AppSettings;
                var rect = "";
                rect = conf["InitialRect"];
                var parts = rect.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                double.TryParse(parts[0], out X1);
                double.TryParse(parts[1], out Y1);
                double.TryParse(parts[2], out W);
                double.TryParse(parts[3], out H);
                Const = double.Parse(conf["Const"]);
            }
            catch
            {

            }
        }
    }
}