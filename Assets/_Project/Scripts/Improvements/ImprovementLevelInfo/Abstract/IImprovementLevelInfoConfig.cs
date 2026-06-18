using System;
using System.Numerics;
using UnityEngine;

public interface IImprovementLevelInfoConfig
{
    public ImprovementLevelInfo[] LevelsInfo { get; }

    public BigInteger GetPriceByLevel(int level);
    public ImprovementLevelInfo GetLevelInfo(int level);
}

[Serializable]
public class ImprovementLevelInfo
{
    [field: SerializeField] public int Level { get; private set; }

    [SerializeField] private string _price;
    
    public BigInteger Price => BigIntegerStatic.Parse(_price);
}