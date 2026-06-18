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
}