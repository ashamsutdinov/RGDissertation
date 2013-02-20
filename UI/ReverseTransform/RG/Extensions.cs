using System;

namespace ReverseTransform
{
  public static class Extensions
  {
    public static decimal Sqrt(this decimal x, decimal epsilon = 0.0M)
    {
      if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

      decimal current = (decimal)Math.Sqrt((double)x), previous;
      do
      {
        previous = current;
        if (previous == 0.0M) return 0;
        current = (previous + x / previous) / 2;
      }
      while (Math.Abs(previous - current) > epsilon);
      return current;
    }

    public static decimal SqrtB(this decimal x, decimal? guess = null)
    {
      if (x == 0)
      {
        return 0;
      }
      var ourGuess = guess.GetValueOrDefault(x / 2m);
      var result = x / ourGuess;
      var average = (ourGuess + result) / 2m;

      return average == ourGuess ? average : Sqrt(x, average);
    }

    public static decimal SqrtD(this decimal x)
    {
      return (decimal)Math.Sqrt((double) x);
    }
  }
}