using System;
using System.Numerics;
using UnityEngine;

public static class BigIntegerExtensions
{
    private const long Scale = 10000;

    public static BigInteger Multiply(this BigInteger value, double multiplier)
    {
        BigInteger scaledMultiplier = (long)Math.Round(multiplier * Scale);
        return (value * scaledMultiplier) / Scale;
    }

    public static float Divide(this BigInteger dividend, BigInteger divisor)
    {
        if (divisor == 0)
        {
            Debug.LogError("Attempted to divide by zero. Returning 0.");
            return 0f;
        }
        
        if (dividend == 0) 
            return 0f;

        long dividendBits = dividend.GetByteCount() * 8;
        long divisorBits = divisor.GetByteCount() * 8;
        long maxBits = Math.Max(dividendBits, divisorBits);
    
        if (maxBits > 53)
        {
            int shift = (int)(maxBits - 53);
            dividend >>= shift;
            divisor >>= shift;
        }

        double result = (double)dividend / (double)divisor;
        return (float)result;
    }
    
    private readonly static string[] keys = new[] { "K", "M", "B", "T", "Q" };

    public static string ToShortValue(this BigInteger count)
    {
        BigInteger absoluteCount = BigInteger.Abs(count);

        if (absoluteCount < 1000)
        {
            return count.ToString();
        }

        string digits = absoluteCount.ToString();
        int log = digits.Length - 1;
        int keyIndex = Math.Clamp(log / 3 - 1, 0, keys.Length - 1);
        string key = keys[keyIndex];
        int digitsToShow = log % 3 + 1;
        string mostSignificantDigits = digits.Substring(0, 3);
        double value = double.Parse(mostSignificantDigits) / Math.Pow(10, 3 - digitsToShow);
        string sign = count < 0 ? "-" : "";
        return $"{sign}{value}{key}";
    }
}