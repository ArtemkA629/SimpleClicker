using System;
using UnityEngine;

public interface IImprovementLevelInfoConfig
{
    public ImprovementLevelInfo[] LevelsInfo { get; }

    public int GetPriceByLevel(int level);
    public ImprovementLevelInfo GetLevelInfo(int level);
}

[Serializable]
public class ImprovementLevelInfo
{
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
}