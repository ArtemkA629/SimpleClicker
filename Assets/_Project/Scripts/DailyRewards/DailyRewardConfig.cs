using System;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DailyRewards/DailyRewardConfig", fileName = "DailyRewardConfig")]
public class DailyRewardConfig : ScriptableObject
{
    [SerializeField] private DailyRewardData[] _rewards;
    [SerializeField] private DailyRewardItem _itemPrefab;
    
    public int TotalDays => _rewards.Length;
    
    public DailyRewardItem ItemPrefab => _itemPrefab;
    
    public bool TryGetRewardData(int day, out DailyRewardData rewardData)
    {
        if (day <= 0 || day > _rewards.Length)
        {
            Debug.LogError($"Day must be between 1 and {_rewards.Length}, got {day}");
            rewardData = null;
            return false;
        }
        
        rewardData = _rewards[day - 1];
        return true;
    }
}

[Serializable]
public class DailyRewardData
{
    [SerializeField] private RewardType _rewardType;
    [SerializeField] private Sprite _icon;
    [SerializeField] private MoneyRewardData _moneyReward;
    [SerializeField] private BuildingRewardData _buildingReward;
    
    public RewardType RewardType => _rewardType;
    public Sprite Icon => _icon;
    public string RewardDescription => _rewardType == RewardType.Money 
        ? _moneyReward.Amount.ToString() 
        : _buildingReward.Amount.ToString();
    
    public MoneyRewardData MoneyReward => _rewardType == RewardType.Money ? _moneyReward : null;
    public BuildingRewardData BuildingReward => _rewardType == RewardType.Building ? _buildingReward : null;
}

[Serializable]
public class MoneyRewardData
{
    [SerializeField] private string _amount;
    
    public BigInteger Amount => BigIntegerStatic.Parse(_amount);
}

[Serializable]
public class BuildingRewardData
{
    [SerializeField] private string _buildingId;
    [SerializeField] private int _amount;
    
    public string BuildingId => _buildingId;
    public int Amount => _amount;
}

public enum RewardType
{
    Money,
    Building
}
