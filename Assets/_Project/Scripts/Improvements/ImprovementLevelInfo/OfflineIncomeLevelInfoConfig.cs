using System;
using System.Linq;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Improvement/OfflineIncomeLevelInfoConfig", fileName = "OfflineIncomeLevelInfoConfig")]
public class OfflineIncomeLevelInfoConfig : ScriptableObject, IImprovementLevelInfoConfig
{
    [SerializeField] private OfflineIncomeLevelInfo[] _levelsInfo;

    public ImprovementLevelInfo[] LevelsInfo => _levelsInfo;
    
    public BigInteger GetPriceByLevel(int level)
    {
        return _levelsInfo.FirstOrDefault(x => x.Level == level).Price;
    }

    public ImprovementLevelInfo GetLevelInfo(int level)
    {
        return _levelsInfo.FirstOrDefault(x => x.Level == level);
    }
}

[Serializable]
public class OfflineIncomeLevelInfo : ImprovementLevelInfo
{
    [field: SerializeField] public int TotalIncomePercentPerDay { get; private set; }
}