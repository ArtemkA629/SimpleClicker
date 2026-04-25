using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Improvement/ImprovementsConfig", fileName = "ImprovementsConfig")]
public class ImprovementsConfig : ScriptableObject
{
    [field: SerializeField] public ImprovementConfigInfo[] ImprovementsInfo { get; private set; }
    [field: SerializeField] public ImprovementItem ItemPrefab { get; private set; }

    public ImprovementConfigInfo GetInfoByName(string name)
    {
        return ImprovementsInfo.First(x => x.TypeConfig.Name == name);
    }
}

[Serializable]
public class ImprovementConfigInfo
{
    [field: SerializeField] public ImprovementTypeConfig TypeConfig { get; private set; }
    [field: SerializeField] public ScriptableObject LevelInfoConfig { get; private set; }
}