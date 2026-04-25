using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Improvement/PowerClickLevelInfoConfig", fileName = "PowerClickLevelInfoConfig")]
public class PowerClickLevelInfoConfig : ScriptableObject, IImprovementLevelInfoConfig
{
    [SerializeField] public PowerClickLevelInfo[] _levelsInfo;

    public ImprovementLevelInfo[] LevelsInfo => _levelsInfo;
    
    public int GetPriceByLevel(int level)
    {
        return _levelsInfo.FirstOrDefault(x => x.Level == level).Price;
    }
    
    public ImprovementLevelInfo GetLevelInfo(int level)
    {
        return _levelsInfo.FirstOrDefault(x => x.Level == level);
    }
}

[Serializable]
public class PowerClickLevelInfo : ImprovementLevelInfo
{
    [field: SerializeField] public int PowerClickMultiplier { get; private set; }
}