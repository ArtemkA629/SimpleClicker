using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RewardConfig", fileName = "RewardConfig")]
public class RewardConfig : ScriptableObject
{
    [SerializeField] private int _baseAddingMoney = 1000;
    [SerializeField] private int _moneyRewardPercent = 10;
    [SerializeField] private int _cooldownMinutes = 3;

    public int BaseAddingMoney => _baseAddingMoney;
    public int MoneyRewardPercent => _moneyRewardPercent;
    public int CooldownMinutes => _cooldownMinutes;
}
