using System;
using System.Numerics;

public static class BigIntegerExtensions
{
    private const long Scale = 10000;

    public static BigInteger Multiply(this BigInteger value, double multiplier)
    {
        BigInteger scaledMultiplier = (long)Math.Round(multiplier * Scale);
        return (value * scaledMultiplier) / Scale;
    }

    public static BigInteger Divide(this BigInteger value, double divisor)
    {
        if (divisor == 0) throw new DivideByZeroException();
        BigInteger scaledDivisor = (long)Math.Round(divisor * Scale);
        return (value * Scale) / scaledDivisor;
    }

    public static BigInteger AddPercent(this BigInteger value, double percent)
    {
        BigInteger scaledMultiplier = (long)Math.Round((1.0 + (percent / 100.0)) * Scale);
        return (value * scaledMultiplier) / Scale;
    }

    public static BigInteger SubtractPercent(this BigInteger value, double percent)
    {
        BigInteger scaledMultiplier = (long)Math.Round((1.0 - (percent / 100.0)) * Scale);
        return (value * scaledMultiplier) / Scale;
    }
}