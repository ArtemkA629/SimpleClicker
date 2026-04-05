using System;
using System.Linq;
using UnityEngine;

public class BuildingsPresenter
{
    private readonly BuildingsModel _model;
    private readonly MoneyController _moneyController;
    private readonly BuildingsConfig _buildingsConfig;

    public event Action<BuildingData> BuildingBought; 
    
    public BuildingsPresenter(BuildingsModel model, MoneyController moneyController, IConfigProvider configProvider)
    {
        _model = model;
        _moneyController = moneyController;
        _buildingsConfig = configProvider.Get<BuildingsConfig>();
    }

    public float BuildingsPriceMultiplier => _buildingsConfig.PriceMultiplier;
    
    public void TryBuyBuilding(string buildingName)
    {
        int totalPrice = GetBuildingTotalPrice(buildingName);
        
        if (_moneyController.Amount < totalPrice)
            return;
        
        _model.AddBuilding(buildingName);
        _moneyController.TrySubtractMoney(totalPrice);
        BuildingBought?.Invoke(GetBuildingData(buildingName));
    }

    public BuildingData GetBuildingData(string buildingName)
    {
        return _model.GetBuildingData(buildingName);
    }

    public BuildingInfo GetBuildingInfo(string buildingName)
    {
        return _buildingsConfig.BuildingsInfo.FirstOrDefault(i => i.Name == buildingName);
    }
    
    private int GetBuildingTotalPrice(string buildingName)
    {
        BuildingInfo buildingInfo = _buildingsConfig.GetBuildingInfo(buildingName);
        BuildingData buildingData = _model.GetBuildingData(buildingName);
        int currentBuildingCount = buildingData == null ? 0 : buildingData.Count;
        return (int)(buildingInfo.StartPrice * Mathf.Pow(_buildingsConfig.PriceMultiplier, currentBuildingCount));
    }
}