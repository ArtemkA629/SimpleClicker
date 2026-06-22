using System;
using System.Linq;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Rebirth/RebirthConfig", fileName = "RebirthConfig")]
public class RebirthConfig : ScriptableObject
{
    [SerializeField] private RebirthStage[] _rebirthStages;

    public bool TryGetRequiredMoneyForLevel(int level, out BigInteger price)
    {
        if (level <= 0)
        {
            Debug.LogError("Rebirth level must be greater than 0");
            return false;
        }
        
        var rebirthInfo = _rebirthStages.FirstOrDefault(s => s.Level == level);

        if (rebirthInfo == null)
        {
            Debug.LogError($"Can't find rebirth info for level {level}");
            return false;
        }
        
        price = rebirthInfo.Price;
        return true;
    }

    public int GetGemsRewardByLevel(int level)
    {
        var rebirthInfo = _rebirthStages.FirstOrDefault(s => s.Level == level);
        return rebirthInfo?.GemsReward ?? 0;
    }
    
    public RebirthDatabase GetDefaultDatabase()
    {
        var database = new RebirthDatabase();
        database.RebirthData = new RebirthSaveData(0);
        return database;
    }

    public bool IsMaxLevel(int level)
    {
        if (level < 0)
        {
            Debug.LogError("Rebirth level must be greater than 0 or equal it");
            return false;
        }
        
        return _rebirthStages.Any(s => s.Level == level + 1) == false;
    }
}

[Serializable]
public class RebirthStage
{
    [SerializeField] public int _level;
    [SerializeField] public string _price;
    [SerializeField] public int _gemsReward;
    
    public int Level => _level;
    public BigInteger Price => BigIntegerStatic.Parse(_price);
    public int GemsReward => _gemsReward;
}
