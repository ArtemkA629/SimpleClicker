using System;
using UnityEngine;

public class DailyRewardPresenter
{
    private readonly DailyRewardConfig _config;
    private readonly DailyRewardChecker _checker;
    private readonly MoneyController _moneyController;
    private readonly BuildingsPresenter _buildingsPresenter;
    private readonly ISaveSystem _saveSystem;
    
    private DailyRewardSaveData _saveData;
    
    public event Action<int> DayRewardClaimed;
    
    public DailyRewardPresenter(DailyRewardChecker checker, MoneyController moneyController, 
        BuildingsPresenter buildingsPresenter, IConfigProvider configProvider, ISaveSystem saveSystem)
    {
        _checker = checker;
        _moneyController = moneyController;
        _buildingsPresenter = buildingsPresenter;
        _config = configProvider.Get<DailyRewardConfig>();
        _saveSystem = saveSystem;
    }
    
    public void Initialize(DailyRewardSaveData saveData)
    {
        _saveData = saveData;
    }
    
    public void ClaimReward(int day, DailyRewardItem item)
    {
        if (day != _saveData.CurrentDay || _checker.CanClaimReward(day) == false) 
            return;
        
        if (_config.TryGetRewardData(day, out var rewardData) == false)
            return;

        GiveReward(rewardData);
        item.SetClaimed();

        if (day <= _config.TotalDays)
        {
            _saveData.MarkDayClaimed(day);
        }
        else
        {
            _saveData.ResetProgress();
        }
        
        Save();
        DayRewardClaimed?.Invoke(day);
    }
    
    private void GiveReward(DailyRewardData rewardData)
    {
        switch (rewardData.RewardType)
        {
            case RewardType.Money:
                MoneyRewardData moneyReward = rewardData.MoneyReward;
                _moneyController.AddMoney(moneyReward.Amount);
                break;
            case RewardType.Building:
                BuildingRewardData buildingReward = rewardData.BuildingReward;
                _buildingsPresenter.AddBuildingForce(buildingReward.BuildingId);
                break;
            default:
                Debug.LogError($"Unknown reward type: {rewardData.RewardType}");
                break;
        }
    }
    
    private void Save()
    {
        _saveSystem.Save(SavingConstants.DailyRewardId, _saveData);
    }
}
