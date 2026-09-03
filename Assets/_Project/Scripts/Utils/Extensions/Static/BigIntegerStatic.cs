using System.Numerics;
using UnityEngine;

public static class BigIntegerStatic
{
    public static BigInteger Parse(string value)
    {
        if (BigInteger.TryParse(value, out var result) == false)
        {
            Debug.LogError($"Failed to parse '{value}' as BigInteger.");
            return 0;
        }

        return result;
    }

    public static BigInteger Max(BigInteger a, BigInteger b)
    {
        return a > b ? a : b;
    }

    public static BigInteger Max(BigInteger a, BigInteger b, BigInteger c)
    {
        return Max(Max(a, b), c);
    }

    public static BigInteger Max(params BigInteger[] values)
    {
        if (values == null || values.Length == 0)
            return 0;

        BigInteger max = values[0];
        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] > max)
                max = values[i];
        }
        return max;
    }
}