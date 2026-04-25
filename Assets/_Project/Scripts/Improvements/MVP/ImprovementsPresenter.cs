using System;
using UnityEngine;

public class ImprovementsPresenter
{
    private ImprovementsModel _model;
    private ImprovementsConfig _config;
    private MoneyController _moneyController;

    public event Action ImprovementBought;
    
    public ImprovementsPresenter(ImprovementsModel model, MoneyController moneyController, IConfigProvider configProvider)
    {
        _model = model;
        _moneyController = moneyController;
        _config = configProvider.Get<ImprovementsConfig>();
    }
    
    public void TryBuyImprovement(string improvementName)
    {
        ImprovementConfigInfo info = _config.GetInfoByName(improvementName);
        ImprovementSaveData data = _model.GetImprovementData(improvementName);
        var levelInfoConfig = (IImprovementLevelInfoConfig)info.LevelInfoConfig;

        if (levelInfoConfig.LevelsInfo.Length == data.Level)
        {
            Debug.LogWarning($"Max level of {improvementName} is already reached.");
            return;
        }
        
        int price = levelInfoConfig.GetPriceByLevel(data.Level + 1);
        
        if (_moneyController.Amount < price)
            return;
        
        _model.AddLevelToImprovement(improvementName);
        _moneyController.TrySubtractMoney(price);
        ImprovementBought?.Invoke();
    }
    
    public int GetImprovementPrice(string improvementName, int improvementLevel)
    {
        ImprovementConfigInfo info = _config.GetInfoByName(improvementName);
        IImprovementLevelInfoConfig levelInfoConfig = (IImprovementLevelInfoConfig)info.LevelInfoConfig;
        improvementLevel = levelInfoConfig.LevelsInfo.Length < improvementLevel ? improvementLevel - 1 : improvementLevel;
        return levelInfoConfig.GetPriceByLevel(improvementLevel);
    }

    public bool CanBuyImprovement(int improvementPrice)
    {
        return _moneyController.Amount >= improvementPrice;
    }

    public bool IsLevelMax(string improvementName, int improvementLevel)
    {
        ImprovementConfigInfo info = _config.GetInfoByName(improvementName);
        IImprovementLevelInfoConfig levelInfoConfig = (IImprovementLevelInfoConfig)info.LevelInfoConfig;
        return levelInfoConfig.LevelsInfo.Length == improvementLevel;
    }
}